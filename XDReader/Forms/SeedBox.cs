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
    public partial class SeedBox : TextBox
    {
        public SeedBox()
        {
            InitializeComponent();

            this.Size = new Size(100, 22);
            //this.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Text = "0";
            this.MaxLength = 8;
            this.Enter += (sender, e) => SelectAll();
            this.Validating += (sender, e) =>
            {
                var txt = this.Text.ToUpper();
                txt = txt.Replace("0x", string.Empty); // 0xを付けたままコピペされてたら消す.
                txt = txt.RegaxReplace("[^0-9A-F]", string.Empty); // 16進数に使われない文字が入っていたら消す.

                if (txt == "") { this.Undo(); return; }

                Seed = Convert.ToUInt32(txt, 16);
                Text = ZeroPadding ? $"{Seed:X8}" : txt;
            };
        }

        private bool _zeroPadding;
        public bool ZeroPadding
        {
            get => _zeroPadding;
            set
            {
                _zeroPadding = value;
                FormattingText();
            }
        }
        public uint Seed { get; private set; }
        public override string Text
        {
            get => base.Text;
            set
            {
                if (value == this.Name) return;
                var txt = value.ToUpper();
                txt = txt.Replace("0x", string.Empty); // 0xを付けたままコピペされてたら消す.
                txt = txt.RegaxReplace("[^0-9A-F]", string.Empty); // 16進数に使われない文字が入っていたら消す.
                txt = txt.Substring(0, Math.Min(txt.Length, 8)); // 8文字を超過していたら消す.

                if (txt == "") return;
                Seed = Convert.ToUInt32(txt, 16);
                base.Text = ZeroPadding ? $"{Seed:X8}" : txt;
            }
        }

        private void FormattingText()
        {
            if (this.Text == "") return;

            Text = ZeroPadding ? $"{Seed:X8}" : $"{Seed:X}";
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
