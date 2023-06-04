using System;
using System.Drawing;

using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace XDReader
{
    class BlinkDetector : IDisposable
    {
        private readonly Mat eye;
        private readonly double thresh = .80;

        public BlinkDetector(string src, double thresh = 0.8)
        {
            eye = new Mat(src);
            this.thresh = thresh;
        }

        public bool Detect(Bitmap capturedPicture)
        {
            using (var mat = BitmapConverter.ToMat(capturedPicture).CvtColor(ColorConversionCodes.BGRA2BGR))
            using (var res = new Mat())
            {
                Cv2.MatchTemplate(mat, eye, res, TemplateMatchModes.CCoeffNormed);
                Cv2.MinMaxLoc(res, out _, out double maxval);

                return maxval >= thresh;
            }
        }

        public void Dispose() => eye.Dispose();
    }
}
