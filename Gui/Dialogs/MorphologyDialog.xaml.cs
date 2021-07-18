using System.Windows;
using Apo.Core;
using Apo.Core.ImageModifiersCv;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Apo.Gui.Dialogs
{
    public partial class MorphologyDialog : Window
    {
        public ApoImage ImgWip;
        public ApoImage ImgOrig;
        private MorphologyModifier _morphology;
        public MorphologyDialog(ApoImage img)
        {
            ImgWip = img.Clone();
            ImgOrig = img.Clone();
            _morphology = new MorphologyModifier();
            InitializeComponent();
            Calculate();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }


        private void LbiReplicate_OnSelected(object sender, RoutedEventArgs e)
        {
            _morphology.BorderTypeVal = BorderType.Replicate;
            Calculate();
        }

        private void LbiConstant_OnSelected(object sender, RoutedEventArgs e)
        {
            _morphology.BorderTypeVal = BorderType.Constant;
            Calculate();
        }

        private void LbiReflect_OnSelected(object sender, RoutedEventArgs e)
        {
            _morphology.BorderTypeVal = BorderType.Reflect;
            Calculate();
        }
        private void LbiErode_OnSelected(object sender, RoutedEventArgs e)
        {
            _morphology.Operation = MorphOp.Erode;
            Calculate();
        }
        private void LbiDilate_OnSelected(object sender, RoutedEventArgs e)
        {
            _morphology.Operation = MorphOp.Dilate;
            Calculate();
        }
        private void LbiOpen_OnSelected(object sender, RoutedEventArgs e)
        {
            _morphology.Operation = MorphOp.Open;
            Calculate();
        }
        private void LbiClose_OnSelected(object sender, RoutedEventArgs e)
        {
            _morphology.Operation = MorphOp.Close;
            Calculate();
        }

        private ElementShape _shape = ElementShape.Cross;
        private void LbiCross_OnSelected(object sender, RoutedEventArgs e)
        {
            _morphology.ShapeVal = ElementShape.Cross;
            Calculate();
        }
        private void LbiEllipse_OnSelected(object sender, RoutedEventArgs e)
        {
            _morphology.ShapeVal = ElementShape.Ellipse;
            Calculate();
        }

        private void Calculate()
        {
            ImgWip = ImgOrig.Clone();
            ImgWip.Modify(_morphology);
            Img.Source = ImgWip.ToImageSource();
        }

        private bool TbiConstant_OnValidateValue(int arg)
        {
            _morphology.BorderConstantValue = arg;
            Calculate();
            return true;
        }
        private bool TbiSize_OnValidateValue(int arg)
        {
            if (arg < 1 || arg % 2 != 1) return false;
            _morphology.SizeVal = arg;
            Calculate();
            return true;

        }

        private bool TbiIterations_OnValueChanged(int arg)
        {
            if (arg < 1) return false;
            _morphology.Iterations = arg;
            Calculate();
            return true;
        }
    }
}
