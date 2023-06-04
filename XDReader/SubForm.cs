using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using PokemonGCRNGLibrary;

namespace COInitialSeedReader
{
    public partial class SubForm : Form
    {
        public SubForm(MainForm parent)
        {
            InitializeComponent();
            mainForm = parent;
        }

        private MainForm mainForm;
        public bool isWorking;
        public bool isDone;
        public float FrameRate = 60.0f;
        public float BlinkFrameRate = 30.0f;
        public int BlinkCount = 10;

        public void RenderPic(Bitmap pic)
        {
            pictureBox1.Image = pic;
        }
        public Task RunnningBattleNow(bool lightMode = false)
        {
            CaptureFrameForm.Instance.Visible = true;
            button2.Text = "キャプチャ枠を隠す";
            button2.Enabled = false;

            return Task.Run(() =>
            {
                int pre = -1;
                int count = 0;
                isWorking = true;
                var nextframe = (double)Environment.TickCount;
                while (true)
                {
                    if (Environment.TickCount >= nextframe)
                    {
                        nextframe += 1000 / FrameRate;
                        var (code, pic) = NameDetector.Detect(CaptureFrameForm.Instance.CaptureFrame());
                        Invoke(new Action(() => RenderPic(pic)));

                        if (code != -1 && code != pre)
                        {
                            mainForm.AddInput(code);
                            count++;
                        }
                        if (count == (lightMode ? 8 : 7)) break;

                        pre = code;
                    }

                    if (!isWorking) break;
                }
                Invoke(new Action(() => Stop()));
                isDone = true;
                Invoke(new Action(() => SwitchCaptureFrameVisible()));
            });
        }
        public Task RunnningBlink()
        {
            CaptureFrameForm.Instance.Visible = true;
            button2.Text = "キャプチャ枠を隠す";
            button2.Enabled = false;

            return Task.Run(() =>
            {
                bool isBlinked = false;
                int blinkCount = 0;
                int frameCount = 0;
                isWorking = true;
                var nextframe = (double)Environment.TickCount;
                while (true)
                {
                    if (Environment.TickCount >= nextframe)
                    {
                        frameCount++;
                        nextframe += 1000 / FrameRate;

                        var (isBlinking, pic) = BlinkDetector.Detect(CaptureFrameForm.Instance.CaptureFrame());
                        Invoke(new Action(() => RenderPic(pic)));

                        if (isBlinked && !isBlinking)
                        {
                            blinkCount++;
                            if (blinkCount > 1) Invoke(new Action(() => { mainForm.AddBlinkBlank((int)(frameCount * (BlinkFrameRate / FrameRate))); }));
                            if (blinkCount == BlinkCount + 1)
                            {
                                Console.Beep(1000, 250);
                                Invoke(new Action(() => mainForm.SearchSeed_Blink()));
                                isWorking = false;
                            }
                            frameCount = 0;
                        }

                        isBlinked = isBlinking;
                    }

                    if (!isWorking) break;
                }
                Invoke(new Action(() => Stop()));
                if(!CaptureFrameForm.Instance.IsDisposed) Invoke(new Action(() => SwitchCaptureFrameVisible()));
                isDone = true;
            });
        }
        
        public void Stop()
        {
            isWorking = false;
            button2.Enabled = true;
            mainForm.Canceled();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if(!isWorking) Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SwitchCaptureFrameVisible();
        }

        private void SwitchCaptureFrameVisible()
        {
            button2.Text = (CaptureFrameForm.Instance.Visible ^= true) ? "キャプチャ枠を隠す" : "キャプチャ枠を表示";
        }

    }
}
