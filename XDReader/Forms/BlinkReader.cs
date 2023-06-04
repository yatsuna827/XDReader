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
using static System.Windows.Forms.LinkLabel;
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

            var detector = new BlinkDetector("./Source/Blink/Eye.png");
            return Task.Run(() =>
            {
                var isBlinked = true;
                var blinkCount = 0;
                var prevTick = 0L;
                var nextFrame = DateTime.Now.Ticks;
                var resList = new List<int>();

                var frameCounter = 0;

                while (!token.IsCancellationRequested)
                {
                    var currentTick = DateTime.Now.Ticks;
                    if (currentTick >= nextFrame)
                    {
                        frameCounter++;
                        nextFrame += FPS;

                        var bmp = captureWindowForm.CaptureScreen();
                        var isBlinking = !detector.Detect(bmp);
                        if(isBlinking && !isBlinked)
                        {
                            Invoke((MethodInvoker)(() =>
                            {
                                Console.Beep();
                                if (blinkCount > 0) resList.Add(frameCounter);
                                blinkResults.Add(new BlinkResult(blinkCount++, frameCounter));
                            }));
                            prevTick = currentTick;
                            frameCounter = 0;
                        }
                        isBlinked = isBlinking;
                    }
                }

                return resList.ToArray();
            }, token);
        }

        private Task CaptureTestAsync(CancellationToken token)
        {
            var detector = new BlinkDetector("./Source/Blink/Eye.png");
            return Task.Run(() =>
            {
                var isBlinked = true;
                var nextFrame = DateTime.Now.Ticks;
                while (!token.IsCancellationRequested)
                {
                    if (DateTime.Now.Ticks >= nextFrame)
                    {
                        nextFrame += FPS;

                        var bmp = captureWindowForm.CaptureScreen();
                        var isBlinking = !detector.Detect(bmp);
                        Invoke((MethodInvoker)(() =>
                        {
                            blinkCaptureTestForm.SetEye(isBlinking);
                            blinkCaptureTestForm.UpdateImage(bmp);
                        }));

                        isBlinked = isBlinking;
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
                File.WriteAllText($"{DateTime.Now:yyyyMMddhhmmss}.csv", string.Join(Environment.NewLine, observedData.Select((b, i) => $"{i},{b}")));
            }
        }
    }

    public class BlinkResult
    {
        public int Index { get; }
        public string Blank { get; }

        public BlinkResult(int i, int blank) => Blank = (Index = i) == 0 ? "---" : blank.ToString();
    }
}
