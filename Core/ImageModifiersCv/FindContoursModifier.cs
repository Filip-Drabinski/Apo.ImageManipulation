using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Apo.Core.ImageModifiersCv
{
    public class FindContoursModifier:IImageModifierCv
    {
        public int NumberOfObjects;
        public bool DrawImageBorder = true;
        public ThresholdType ThresholdType = ThresholdType.Binary;
        public double Threshold = 127;
        private readonly RetrType _countourRetrievalMode = RetrType.List;
        public ChainApproxMethod ChainApprox = ChainApproxMethod.ChainApproxSimple;
        public FindContoursModifier(){}
        public FindContoursModifier(bool drawImageBorder, ThresholdType thresholdType, double threshold, RetrType countourRetrievalMode, ChainApproxMethod chainApproxMethod)
        {
            DrawImageBorder = drawImageBorder;
            ThresholdType = thresholdType;
            Threshold = threshold;
            _countourRetrievalMode = countourRetrievalMode;
            ChainApprox = chainApproxMethod;
        }
        public void Work(ref Image<Gray, byte> image)
        {
            var img = image.Convert<Bgr, byte>();
            Work(ref img);
            image = img.Convert<Gray, byte>();
        }
        public void Work(ref Image<Bgr, byte> image)
        {
            var thresh = new Mat();
            CvInvoke.CvtColor(image,thresh,ColorConversion.Bgr2Gray);
            var contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
            var hierarchy = new Mat();
            CvInvoke.Threshold(image, thresh, Threshold, 255, ThresholdType);
            CvInvoke.CvtColor(thresh, thresh, ColorConversion.Bgr2Gray);
            CvInvoke.FindContours(thresh,contours,hierarchy, _countourRetrievalMode, ChainApprox);
            //image = thresh.ToImage<Bgr, byte>();
            NumberOfObjects = contours.Size - 1;
            var contSize = DrawImageBorder ? contours.Size : contours.Size - 1;
            for (int i = 0; i < contSize; i++)
            {
                CvInvoke.DrawContours(image, contours, i, new MCvScalar(0, 0, 255), 3);
            }
        }
        public void Work(ref Image<Bgra, byte> image)
        {
            var img = image.Convert<Bgr, byte>();
            Work(ref img);
            image = img.Convert<Bgra, byte>();
        }
    }
}