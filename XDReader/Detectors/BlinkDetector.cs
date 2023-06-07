using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace XDReader
{
    class BlinkDetector
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

                if (!(r <= g || r <= b || (0.7 * r < g)))
                    cnt++;
            }

            capturedPicture.UnlockBits(data);

            return cnt;
        }
    }
}
