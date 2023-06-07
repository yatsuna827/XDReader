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
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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

        // private const long FPS = (long)(10_000_000 / (29.97 * 2));
        private const double FPS = 1000 / 60.0;
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

        private int[] observedData;
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
            blinkCoolTimeBox.Enabled = false;
            
            observedData = await CaptureBlinkAsync(cancellationTokenSource.Token);

            cancellationTokenSource = null;
            CaptureTestMenuItem.Enabled = true;
            blinkCoolTimeBox.Enabled = true;
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

        private Task<int[]> CaptureBlinkAsync(CancellationToken token)
        {
            blinkResults = new BindingList<BlinkResult>();
            blinkResultBindingSource.DataSource = blinkResults;
            observedData = null;

            var detector = new BlinkDetector();
            return Task.Run(() =>
            {
                var isBlinked = true;
                var blinkCount = 0;
                var prevTick = 0L;
                var nextFrame = (double)Environment.TickCount;
                var resList = new List<int>();
                var thresh = (int)blinkThresholdBox.Value;

                var frameCounter = 0;

                while (!token.IsCancellationRequested)
                {
                    var currentTick = Environment.TickCount;
                    if (currentTick >= nextFrame)
                    {
                        frameCounter++;
                        nextFrame += FPS;

                        var bmp = captureWindowForm.CaptureScreen();
                        var cnt = detector.Count(bmp);

                        var isBlinking = cnt < blinkThresholdBox.Value;
                        if (isBlinking && !isBlinked)
                        {
                            Callback(blinkCount++, frameCounter);
                            if (blinkCount > 0) resList.Add(frameCounter);

                            prevTick = currentTick;
                            frameCounter = 0;
                        }
                        isBlinked = isBlinking;
                    }
                }

                return resList.ToArray();
            }, token);
        }

        private Task Callback(int blinkCount, int frameCounter)
        {
            return Task.Run(() => Invoke((MethodInvoker)(() => {
                Console.Beep();
                blinkResults.Add(new BlinkResult(blinkCount, frameCounter));
            })));
        }

        private Task CaptureTestAsync(CancellationToken token)
        {
            var detector = new BlinkDetector();
            return Task.Run(() =>
            {
                var nextFrame = (double)Environment.TickCount;
                while (!token.IsCancellationRequested)
                {
                    if (Environment.TickCount >= nextFrame)
                    {
                        nextFrame += FPS;

                        var bmp = captureWindowForm.CaptureScreen();
                        var cnt = detector.Count(bmp);

                        var isBlinking = cnt < blinkThresholdBox.Value;

                        Invoke((MethodInvoker)(() =>
                        {
                            blinkCaptureTestForm.SetData(cnt, isBlinking);
                            bmp.ExtractEye();
                            blinkCaptureTestForm.UpdateImage(bmp);
                        }));
                    }
                }
            }, token);
        }

        private async void BlinkReader_FormClosing(object sender, FormClosingEventArgs e)
        {
            await ForcedQuit();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            CaptureWindowForm.DisplayScale = (int)numericUpDown2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (observedData != null)
            {
                File.WriteAllText($"{DateTime.Now:yyyyMMddhhmmss}.txt", string.Join(Environment.NewLine, observedData.Select((b, i) => $"{i},{b}")));
            }
        }
    }

    public static class BitmapExt
    {
        public static void ExtractEye(this Bitmap bitmap)
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
        }
    }

    public class BlinkResult
    {
        public int Index { get; }
        public string Blank { get; }

        public BlinkResult(int i, int blank) => Blank = (Index = i) == 0 ? "---" : blank.ToString();
    }
}
