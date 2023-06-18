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
using System.IO;
using XDReader.Forms;
using System.Reflection;

namespace XDReader
{
    public partial class BlinkReader : Form
    {
        delegate IEnumerable<uint> BlinkSeedSearch();

        public BlinkReader()
        {
            InitializeComponent();

            var version = Assembly.GetExecutingAssembly().GetName().Version;

            this.Text += $" v{version.Major}.{version.Minor}.{version.Build}";

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

        private async void Button_blink_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                return;
            }

            Button_blink.Text = "停止";
            cancellationTokenSource = new CancellationTokenSource();
            CaptureTestMenuItem.Enabled = button1.Enabled = false;
            blinkCoolTimeBox.Enabled = groupBox1.Enabled = false;
            
            var (completed, blinks, lastTicks) = await CaptureBlinkAsync(cancellationTokenSource.Token, (int)blinkCountBox.Value);
            if (completed)
            {
                // 中断されてない
                // 検索処理を走らせる.
                //Console.Beep(1000, 250);
                Button_blink.Text = "検索中";
                Button_blink.Enabled = false;
                BlinkResultDGV.Rows.Clear();

                _lastTicks = lastTicks;

                var currentSeed = currentSeedBox.Seed;
                var targetSeed = targetSeedBox.Seed;
                var min = (uint)minFrameBox.Value;
                var max = (uint)maxFrameBox.Value;
                var error = (int)allowableErrorBox.Value;
                var cool = (int)blinkCoolTimeBox.Value;

                await SearchSeedAsync(currentSeed, targetSeed, () => SeedFinder.FindCurrentSeedByBlinkFaster(currentSeed, min, max, blinks,
                        coolTime: cool,
                        allowanceLimitOfError: error,
                        blankMagnification: 1)
                );
                Button_blink.Enabled = true;
            }
            cancellationTokenSource = null;
            CaptureTestMenuItem.Enabled = button1.Enabled = true;
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
            Button_blink.Enabled = button1.Enabled = false;
            CaptureTestMenuItem.Text = "キャプチャテスト停止";
            await CaptureTestAsync(cancellationTokenSource.Token);

            cancellationTokenSource = null;
            Button_blink.Enabled = button1.Enabled = true;
            CaptureTestMenuItem.Text = "キャプチャテスト開始";
        }

        private void SwitchCaptureFrameVisibleMenuItem_Click(object sender, EventArgs e)
            => SwitchCaptureFrameVisibleMenuItem.Text = (captureWindowForm.Visible ^= true) ? "キャプチャ枠非表示" : "キャプチャ枠表示";

        private void ShootMenuItem_Click(object sender, EventArgs e)
        {
            using (var img = captureWindowForm.CaptureScreen())
            {
                img.Save($"{DateTime.Now:yyyyMMddhhmmss}.png", ImageFormat.Png);
            }
        }

        private long _lastTicks;
        private Task<(bool Completed, int[] Blinks, long LastTicks)> CaptureBlinkAsync(CancellationToken token, int n)
        {
            blinkResults = new BindingList<BlinkResult>();
            blinkResultBindingSource.DataSource = blinkResults;

            var detector = new DusclopsBlinkDetector();
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

                return (blinkCount == n + 1, resList.ToArray(), prevTick);
            }, token);
        }

        // UIにフィードバックするためにInvokeを呼ぶ必要があるが、Invokeは重い。
        // 「Invokeする処理」自体を非同期で呼び出すことで処理速度を安定させる。
        private Task AddBlink(int blinkCount, int frameCounter)
        {
            return Task.Run(() => Invoke((MethodInvoker)(() => {
                //Console.Beep();
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
                        row.SetValues($"{seed.GetIndex(currentSeed)}", $"{seed:X8}", $"{targetSeed.GetIndex(seed)}", $"{seed.NextSeed(5000):X8}");
                        BlinkResultDGV.Rows.Add(row);
                    }));
                }
            });
        }

        private Task CaptureTestAsync(CancellationToken token)
        {
            var detector = new DusclopsBlinkDetector();
            return Task.Run(() =>
            {
                var isBlinked = true;
                var prevCount = 0;
                var nextFrame = DateTime.Now.Ticks;
                //var list = new List<int>();
                while (!token.IsCancellationRequested)
                {
                    var current = DateTime.Now.Ticks;
                    if (current >= nextFrame)
                    {
                        nextFrame += FPS;

                        var bmp = captureWindowForm.CaptureScreen();
                        var cnt = detector.Count(bmp);
                        //list.Add(cnt);
                        var isBlinking = cnt < numericUpDown1.Value;
                        Invoke((MethodInvoker)(() =>
                        {
                            blinkCaptureTestForm.SetData(cnt, isBlinking);
                            blinkCaptureTestForm.UpdateImage(detector.ExtractEye(bmp));
                        }));

                        isBlinked = isBlinking;
                        prevCount = cnt;
                    }
                }

                /*
                Invoke((MethodInvoker)(() =>
                {
                    File.WriteAllText($"./{DateTime.Now.Millisecond}.txt", string.Join(Environment.NewLine, list));
                }));
                */
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
            var target = e.ColumnIndex == 3 ? seed.NextSeed(5000) : targetSeedBox.Seed;
            var targetIndex = target.GetIndex(initSeed);

            var _cool = (int)blinkCoolTimeBox.Value;

            if (target.GetIndex(seed) > 1000000)
            {
                MessageBox.Show("必要な待機時間が長すぎます");
                return;
            }

            // 「現在地から目標seedまでの待機時間」を算出する
            // arr - 1が必要な待機時間(フレーム)
            // -1が必要なのは、先頭のseedは現在地である(=0基準である)ため

            var handler = new BlinkObjectEnumeratorHanlder(new BlinkObject(_cool, 10, delayAtMaturity: (int)delayAtMaturityBox.Value));

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

            if (blinks.Length == 0)
            {
                MessageBox.Show("時間すぎちゃった…。");
                return;
            }

            // 瞬き2回につき1F、クールタイム中が停滞によって延びると想定する
            for (int i = 1; i < blinks.Length; i += 2)
                blinks[i]++;

            if (_timer == null || _timer.IsDisposed)
                _timer = new BlinkTimer(blinks, 
                    coolTime: _cool,
                    //framesPerDelay: checkBox1.Checked ? (int)framesPerDelayBox.Value : -1,
                    breakingFrames: (int)breakingTimeBox.Value,
                    baseTick: _lastTicks
                );

            _timer.Show();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            CaptureWindowForm.DisplayScale = (int)numericUpDown2.Value;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                return;
            }

            button1.Text = "停止";
            cancellationTokenSource = new CancellationTokenSource();
            Button_blink.Enabled = CaptureTestMenuItem.Enabled = false;
            blinkCoolTimeBox.Enabled = groupBox1.Enabled = false;

            var (_, blinks, _) = await CaptureBlinkAsync(cancellationTokenSource.Token, int.MaxValue);

            File.WriteAllLines($"{currentSeedBox.Seed:X8}.txt", blinks.Select(_ => _.ToString()).ToArray());

            cancellationTokenSource = null;
            Button_blink.Enabled = CaptureTestMenuItem.Enabled = true;
            blinkCoolTimeBox.Enabled = groupBox1.Enabled = true;
            button1.Text = "観測する";
        }

        private Greevil _greevilForm;
        private void button2_Click(object sender, EventArgs e)
        {
            if (_greevilForm == null || _greevilForm.IsDisposed)
                _greevilForm = new Greevil();

            _greevilForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var initSeed = currentSeedBox.Seed;
            var _index = 2197;
            var seed = initSeed.NextSeed(2197);
            var target = seed.NextSeed(5000);
            var targetIndex = target.GetIndex(initSeed);

            var _cool = (int)blinkCoolTimeBox.Value;

            var handler = new BlinkObjectEnumeratorHanlder(new BlinkObject(_cool, 10, delayAtMaturity: (int)delayAtMaturityBox.Value));

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

            if (blinks.Length == 0)
            {
                MessageBox.Show("時間すぎちゃった…。");
                return;
            }

            // 瞬き2回につき1F、クールタイム中が停滞によって延びると想定する
            for (int i = 1; i < blinks.Length; i += 2)
                blinks[i]++;

            if (_timer == null || _timer.IsDisposed)
                _timer = new BlinkTimer(blinks,
                    coolTime: _cool,
                    //framesPerDelay: checkBox1.Checked ? (int)framesPerDelayBox.Value : -1,
                    breakingFrames: (int)breakingTimeBox.Value,
                    baseTick: _lastTicks
                );

            _timer.Show();
        }
    }

    public class BlinkResult
    {
        public int Index { get; }
        public string Blank { get; }

        public BlinkResult(int i, int blank) => Blank = (Index = i) == 0 ? "---" : blank.ToString();
    }
}
