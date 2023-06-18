using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace XDReader
{
    abstract class BlinkDetector
    {
        public int Count(Bitmap capturedPicture)
        {
            var data = capturedPicture.LockBits(
                new Rectangle(0, 0, capturedPicture.Width, capturedPicture.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            var buf = new byte[capturedPicture.Width * capturedPicture.Height * 4];
            Marshal.Copy(data.Scan0, buf, 0, buf.Length);

            var cnt = 0;
            for (int i = 0; i < buf.Length; i += 4)
            {
                var (b, g, r) = (buf[i], buf[i + 1], buf[i + 2]);

                if (CheckDot(r, g, b))
                    cnt++;
            }

            capturedPicture.UnlockBits(data);

            return cnt;
        }
        public Bitmap ExtractEye(Bitmap bitmap)
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

                if (!CheckDot(r, g, b))
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

        protected abstract bool CheckDot(byte r, byte g, byte b);  
    }

    class DusclopsBlinkDetector : BlinkDetector
    {
        protected override bool CheckDot(byte r, byte g, byte b)
            => !(r <= g || r <= b || (0.7 * r < g));
    }
    class EspeonBlinkDetector : BlinkDetector
    {
        protected override bool CheckDot(byte r, byte g, byte b)
            => !(b <= g || b <= r || (0.6 * b < g));
    }

    class ShellderBlinkDetector : BlinkDetector
    {
        protected override bool CheckDot(byte r, byte g, byte b)
        {
            var max = Math.Max(Math.Max(r, g), b);
            var min = Math.Min(Math.Min(r, g), b);

            return (max - min < 15) && min > 150;
        }
    }
}
