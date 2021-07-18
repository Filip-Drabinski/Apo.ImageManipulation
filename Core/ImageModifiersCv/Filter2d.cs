using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Apo.Core.ImageModifiersCv
{
    class Filter2d : IImageModifierCv
    {
        public Mat Kernel;
        public System.Drawing.Point Anchor = new System.Drawing.Point(-1, -1);
        public double Delta = 0;
        public BorderType BorderType = BorderType.Replicate;

        public Filter2d()
        {
            Kernel = Mat.Zeros(3, 3, DepthType.Cv64F, 1);
        }
        public Filter2d(Mat kernel)
        {
            Kernel = kernel;
        }
        public void Work(ref Image<Gray, byte> image)
        {
            CvInvoke.Filter2D(image, image, Kernel, Anchor, Delta, BorderType);
        }
    }
}