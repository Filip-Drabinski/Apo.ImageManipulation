using Emgu.CV;
using Emgu.CV.Structure;

namespace Apo.Core.ImageModifiersCv
{
    public class CannyModifier : IImageModifierCv
    {
        public void Work(ref Image<Gray, byte> image)
        {
            CvInvoke.Canny(image,image,Threshold1,Threshold2,ApertureSize,L2Gradient);
        }

        public bool L2Gradient { get; set; } = false;

        public int ApertureSize { get; set; } = 3;

        public double Threshold2 { get; set; } = 200;

        public double Threshold1 { get; set; } = 100;
    }
}