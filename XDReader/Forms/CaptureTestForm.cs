using System.Drawing;
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

            return form;
        }

        private static readonly Size HOSEI = new Size(40, 88);
        public void ResizeFrame(Size size)
        {
            this.Size = size + HOSEI;
            this.pictureBox1.Size = size;
        }

        public void SetData(int dots, bool blinking)
            => (this.faceBox.Text, this.dotCountBox.Text) = (blinking ? "-。-" : "･。･", $"{dots}");

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
