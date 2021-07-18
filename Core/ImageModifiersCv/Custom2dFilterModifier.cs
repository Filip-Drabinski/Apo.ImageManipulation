using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Apo.Core.ImageModifiersCv
{
    public class Custom2dFilterModifier : IImageModifierCv
    {
        private Mat _kernel;
        public Custom2dFilterModifier(int size = 3)
        {
            _kernel = new Mat(size, size, DepthType.Cv64F, 1);
            Values = new double[size*size];
        }
        public void Work(ref Image<Gray, byte> image)
        {
            _kernel.SetTo(Values);
            CvInvoke.Filter2D(image,image,_kernel,new Point(-1,-1),0,BorderTypeVal);
        }

        public BorderType BorderTypeVal { get; set; } = BorderType.Replicate;
        public double[] Values { get;}
    }
}