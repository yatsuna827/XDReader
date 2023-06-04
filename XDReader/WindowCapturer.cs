using System.Drawing;

namespace XDReader
{
    class WindowCapturer
    {
        public static Bitmap Capture(int top, int left, int height, int width, int screenRate = 200)
        {
            var w = width * screenRate / 100;
            var h = height * screenRate / 100;
            var x = left * screenRate / 100;
            var y = top * screenRate / 100;

            using(var bmp = NativeMethods.CaptureScreen(x, y, h, w))
            {
                return new Bitmap(bmp, new Size(width, height));
            }
        }
    }
}
