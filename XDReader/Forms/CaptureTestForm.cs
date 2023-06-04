using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XDReader
{
    public partial class CaptureTestForm : Form
    {
        public CaptureTestForm()
        {
            InitializeComponent();
        }

        public static CaptureTestForm CreateBlinkCaptureTestForm()
        {
            var form = new CaptureTestForm
            {
                Text = "瞬きテスト"
            };
            form.playerNameBox.Visible = false;
            form.battleTeamBox.Visible = false;

            return form;
        }

        public static CaptureTestForm CreateBattleNowCaptureTestForm()
        {
            var form = new CaptureTestForm
            {
                Text = "とにかくバトルテスト"
            };
            form.faceBox.Visible = false;
            return form;
        }


        private static readonly Size HOSEI = new Size(40, 88);
        public void ResizeFrame(Size size)
        {
            this.Size = size + HOSEI;
            this.pictureBox1.Size = size;
        }
        public void SetEye(bool blinking) => this.faceBox.Text = blinking ? "-。-" : "･。･";
        public void SetBattleNow(string p, string t)
        {
            playerNameBox.Text = p;
            battleTeamBox.Text = t;
        }
        public void UpdateImage(Bitmap bmp)
        {
            var img = pictureBox1.Image;
            pictureBox1.Image = bmp;
            img?.Dispose();
        }

        private void BlinkCaptureTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }
    }
}
