using System.Drawing;
using System.Windows.Forms;

namespace XDReader
{
    public partial class CaptureWindowForm : Form
    {
        public CaptureWindowForm(string parentFormName)
        {
            InitializeComponent();

            this.Text = $"キャプチャフレーム({parentFormName})";
            this.TransparencyKey = Color.Red;
            this.pictureBox1.BackColor = Color.Red;
            button1.Visible = button2.Visible = false;
            this.TopMost = true;
        }

        public static int DisplayScale { get; set; } = 100;

        public Bitmap CaptureScreen()
            => WindowCapturer.Capture(Top + 34, Left + 11, pictureBox1.Size.Height, pictureBox1.Size.Width, DisplayScale);

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (pictureBox1.Image != null) return;

            pictureBox1.Image = CaptureScreen();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            if (pictureBox1.Image == null) return;

            var img = pictureBox1.Image;
            pictureBox1.Image = null;
            img.Dispose();
        }

        // フォームの破棄をキャンセルして不可視化する.
        private void CaptureWindowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        public Size CaptureAreaSize { get => pictureBox1.Size + new Size(2, 2); }
    }
}
