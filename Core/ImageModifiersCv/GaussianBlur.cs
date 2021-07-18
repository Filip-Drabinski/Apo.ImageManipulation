using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Apo.Core.ImageModifiersCv
{
    class GaussianBlur:IImageModifierCv
    {

        public int Width;
        public int Height;
        public double SigmaX;
        public double SigmaY;
        public BorderType BorderType;

        public GaussianBlur(int width, int height, double sigmaX, double sigmaY, BorderType borderType)
        {
            Width = width;
            Height = height;
            SigmaX = sigmaX;
            SigmaY = sigmaY;
            BorderType = borderType;
        }

        public void Work(ref Image<Gray, byte> image)
        {
            CvInvoke.GaussianBlur(image, image, new Size(Width, Height), SigmaX, SigmaY, BorderType);
        }
    }
}