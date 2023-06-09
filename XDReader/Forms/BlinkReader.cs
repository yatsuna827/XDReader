using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PokemonXDRNGLibrary;
using PokemonPRNG.LCG32.GCLCG;
using PokemonXDRNGLibrary.AdvanceSource;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace XDReader
{
    public partial class BlinkReader : Form
    {
        delegate IEnumerable<uint> BlinkSeedSearch();

        public BlinkReader()
        {
            InitializeComponent();

            captureWindowForm.SizeChanged +=
                (sender, e) => blinkCaptureTestForm.ResizeFrame(captureWindowForm.CaptureAreaSize);
            captureWindowForm.FormClosing +=
                (sender, e) => SwitchCaptureFrameVisibleMenuItem.Text = "キャプチャ枠表示";

            blinkCaptureTestForm.ResizeFrame(captureWindowForm.CaptureAreaSize);
            blinkCaptureTestForm.FormClosing += (sender,e) => cancellationTokenSource?.Cancel();
        }

        private const long FPS = (long)(10_000_000 / (29.97 * 2));
        private CancellationTokenSource cancellationTokenSource;
        private BindingList<BlinkResult> blinkResults;
        private readonly CaptureWindowForm captureWindowForm = new CaptureWindowForm("瞬き");
        private readonly CaptureTestForm blinkCaptureTestForm = CaptureTestForm.CreateBlinkCaptureTestForm();
        public Task ForcedQuit()
        {
            return Task.Run(() =>
            {
                cancellationTokenSource?.Cancel();
                while (cancellationTokenSource != null) { }
            });
        }

        public void SetCurrentSeed(uint seed) => currentSeedBox.Text = $"{seed:X8}";

        private async void Button_blink_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                return;
            }

            Button_blink.Text = "停止";
            cancellationTokenSource = new CancellationTokenSource();
            CaptureTestMenuItem.Enabled = false;
            blinkCoolTimeBox.Enabled = groupBox1.Enabled = false;
            
            var (completed, blinks) = await CaptureBlinkAsync(cancellationTokenSource.Token, (int)blinkCountBox.Value);
            if (completed)
            {
                // 中断されてない
                // 検索処理を走らせる.
                Console.Beep(1000, 250);
                Button_blink.Text = "検索中";
                Button_blink.Enabled = false;
                BlinkResultDGV.Rows.Clear();

                var currentSeed = currentSeedBox.Seed;
                var targetSeed = targetSeedBox.Seed;
                var min = (uint)minFrameBox.Value;
                var max = (uint)maxFrameBox.Value;
                var error = (int)allowableErrorBox.Value;
                var magnification = (int)blankMagnificationBox.Value / 100.0;
                var cool = (int)blinkCoolTimeBox.Value;

                await SearchSeedAsync(currentSeed, targetSeed, () => SeedFinder.FindCurrentSeedByBlinkFaster(currentSeed, min, max, blinks,
                        coolTime: cool,
                        allowanceLimitOfError: error,
                        blankMagnification: magnification)
                );
                Button_blink.Enabled = true;
            }
            cancellationTokenSource = null;
            CaptureTestMenuItem.Enabled = true;
            blinkCoolTimeBox.Enabled = groupBox1.Enabled = true;
            Button_blink.Text = "開始";
        }

        private async void CaptureTestMenuItem_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null)
            {
                blinkCaptureTestForm.Visible = false;
                cancellationTokenSource.Cancel();
                return;
            }
            cancellationTokenSource = new CancellationTokenSource();
            blinkCaptureTestForm.Visible = true;
            Button_blink.Enabled = false;
            CaptureTestMenuItem.Text = "キャプチャテスト停止";
            await CaptureTestAsync(cancellationTokenSource.Token);

            cancellationTokenSource = null;
            Button_blink.Enabled = true;
            CaptureTestMenuItem.Text = "キャプチャテスト開始";
        }

        private void SwitchCaptureFrameVisibleMenuItem_Click(object sender, EventArgs e)
            => SwitchCaptureFrameVisibleMenuItem.Text = (captureWindowForm.Visible ^= true) ? "キャプチャ枠非表示" : "キャプチャ枠表示";

        private void ShootMenuItem_Click(object sender, EventArgs e)
        {
            using (var img = captureWindowForm.CaptureScreen())
            {
                img.Save($"{DateTime.Now:yyyyMMddhhmmss}.png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private long lastTick;
        private Task<(bool completed, int[] blinks)> CaptureBlinkAsync(CancellationToken token, int n)
        {
            blinkResults = new BindingList<BlinkResult>();
            blinkResultBindingSource.DataSource = blinkResults;

            var detector = new BlinkDetector();
            return Task.Run(() =>
            {
                var isBlinked = true;
                var prevCount = 0;
                var blinkCount = 0;
                var prevTick = 0L;
                var nextFrame = DateTime.Now.Ticks;
                var resList = new List<int>();

                var thresh = (int)numericUpDown1.Value;

                while (!token.IsCancellationRequested)
                {
                    var currentTick = DateTime.Now.Ticks;
                    if (currentTick >= nextFrame)
                    {
                        nextFrame += FPS;

                        var bmp = captureWindowForm.CaptureScreen();
                        var cnt = detector.Count(bmp);
                        var isBlinking = cnt < thresh;
                        if (isBlinking && !isBlinked)
                        {
                            var frames = (currentTick - prevTick).TickToFrame(29.97 * 2);
                            if (blinkCount > 0) resList.Add(frames);
                            AddBlink(blinkCount++, frames);
                            prevTick = currentTick;
                        }
                        isBlinked = isBlinking;
                        prevCount = cnt;

                        if (blinkCount > n) break;
                    }
                }

                lastTick = prevTick;
                return (blinkCount == n + 1, resList.ToArray());
            }, token);
        }

        // UIにフィードバックするためにInvokeを呼ぶ必要があるが、Invokeは重い。
        // 「Invokeする処理」自体を非同期で呼び出すことで処理速度を安定させる。
        private Task AddBlink(int blinkCount, int frameCounter)
        {
            return Task.Run(() => Invoke((MethodInvoker)(() => {
                Console.Beep();
                blinkResults.Add(new BlinkResult(blinkCount, frameCounter));
            })));
        }

        private Task SearchSeedAsync(uint currentSeed, uint targetSeed, BlinkSeedSearch blinkSeedSearch)
        {
            return Task.Run(() =>
            {
                foreach (var seed in blinkSeedSearch())
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        var row = new DataGridViewRow();
                        row.CreateCells(BlinkResultDGV);
                        row.SetValues($"{seed.GetIndex(currentSeed)}", $"{seed:X8}", $"{targetSeed.GetIndex(seed)}");
                        BlinkResultDGV.Rows.Add(row);
                    }));
                }
            });
        }

        private Task CaptureTestAsync(CancellationToken token)
        {
            var detector = new BlinkDetector();
            return Task.Run(() =>
            {
                var isBlinked = true;
                var prevCount = 0;
                var nextFrame = DateTime.Now.Ticks;
                var list = new List<int>();
                while (!token.IsCancellationRequested)
                {
                    var current = DateTime.Now.Ticks;
                    if (current >= nextFrame)
                    {
                        nextFrame += FPS;

                        var bmp = captureWindowForm.CaptureScreen();
                        var cnt = detector.Count(bmp);
                        list.Add(cnt);
                        var isBlinking = cnt < numericUpDown1.Value;
                        Invoke((MethodInvoker)(() =>
                        {
                            blinkCaptureTestForm.SetData(cnt, isBlinking);
                            blinkCaptureTestForm.UpdateImage(bmp.ExtractEye());
                        }));

                        isBlinked = isBlinking;
                        prevCount = cnt;
                    }
                }

                Invoke((MethodInvoker)(() =>
                {
                    File.WriteAllText($"./{DateTime.Now.Millisecond}.txt", string.Join(Environment.NewLine, list));
                }));
            }, token);
        }

        private async void BlinkReader_FormClosing(object sender, FormClosingEventArgs e)
        {
            await ForcedQuit();
        }

        private BlinkTimer _timer;
        private void BlinkResultDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var _index = int.Parse((string)BlinkResultDGV.Rows[e.RowIndex].Cells[0].Value);
            var seed = Convert.ToUInt32((string)BlinkResultDGV.Rows[e.RowIndex].Cells[1].Value, 16);

            var initSeed = currentSeedBox.Seed;
            var target = seed.NextSeed(114514);
            var targetIndex = target.GetIndex(initSeed);

            var _cool = (int)blinkCoolTimeBox.Value;

            // 「現在地から目標seedまでの待機時間」を算出する
            // arr - 1が必要な待機時間(フレーム)
            // -1が必要なのは、先頭のseedは現在地である(=0基準である)ため

            var handler = new BlinkObjectEnumeratorHanlder(new BlinkObject(_cool, 10));

            var arr = initSeed.EnumerateSeed(handler)
                .SkipWhile(_ => _.GetIndex(initSeed) < _index)
                .TakeWhile(_ => _.GetIndex(initSeed) <= targetIndex).ToArray();

            // 「現在地から目標seedまでの瞬き間隔の系列」
            var blinkSeries = initSeed.EnumerateActionSequence(handler)
                .SkipWhile(_ => _.Seed.GetIndex(initSeed) < _index)
                .TakeWhile(_ => _.Seed.GetIndex(initSeed) <= targetIndex);

            // 残り待機時間 - 瞬き間隔の系列の総和
            var rest = (arr.Length - 1) - blinkSeries.Skip(1).Sum(_ => _.Interval);

            // 目標seedがちょうど瞬きに重なるとは限らないため
            // 余りが出る場合は末尾に追加する必要がある
            var blinks = rest > 0 ?
                blinkSeries.Select(_ => _.Interval).Append(rest).ToArray() :
                blinkSeries.Select(_ => _.Interval).ToArray();

            if (_timer == null || _timer.IsDisposed)
                _timer = new BlinkTimer(blinks, breakingFrames: (int)breakingTimeBox.Value, baseTick: lastTick);

            _timer.Show();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            CaptureWindowForm.DisplayScale = (int)numericUpDown2.Value;
        }

    }

    public class BlinkResult
    {
        public int Index { get; }
        public string Blank { get; }

        public BlinkResult(int i, int blank) => Blank = (Index = i) == 0 ? "---" : blank.ToString();
    }


    public static class BitmapExt
    {
        public static Bitmap ExtractEye(this Bitmap bitmap)
        {
            var data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            var buf = new byte[bitmap.Width * bitmap.Height * 4];
            Marshal.Copy(data.Scan0, buf, 0, buf.Length);

            for (int i = 0; i < buf.Length; i += 4)
            {
                var (b, g, r) = (buf[i], buf[i + 1], buf[i + 2]);

                if (r <= g || r <= b || (0.7 * r < g))
                {
                    buf[i] = 0;
                    buf[i + 1] = 0;
                    buf[i + 2] = 0;
                }
            }

            Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            bitmap.UnlockBits(data);

            return bitmap;
        }
    }

}
