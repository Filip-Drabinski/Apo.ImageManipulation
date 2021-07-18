using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Apo.Core.ImageModifiersCv
{
    public class EdgeDetectionSobelModifier : IImageModifierCv
    {
        public void Work(ref Image<Gray, byte> image)
        {
            var res = new Mat(image.Width,image.Height,DepthType.Cv32F,1);
            CvInvoke.Sobel(image,res,DepthType.Cv32F,XOrder,YOrder,KSize,Scale,Delta,BorderTypeVal);
            image = res.ToImage<Gray, byte>();
        }
        public void Work(ref Image<Bgr, byte> image)
        {
            var res = new Mat(image.Width,image.Height,DepthType.Cv32F,1);
            CvInvoke.Sobel(image,res,DepthType.Cv32F,XOrder,YOrder,KSize,Scale,Delta,BorderTypeVal);
            image = res.ToImage<Bgr, byte>();
        }

        public BorderType BorderTypeVal { get; set; } = BorderType.Replicate;

        public double Delta { get; set; } = 1;

        public double Scale { get; set; } = 1;

        public int YOrder { get; set; } = 0;

        public int XOrder { get; set; } = 1;

        public int KSize { get; set; } = 5;
    }
}