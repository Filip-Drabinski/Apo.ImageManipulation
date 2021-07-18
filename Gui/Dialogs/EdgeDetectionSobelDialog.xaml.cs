using System.Windows;
using Apo.Core;
using Apo.Core.ImageModifiersCv;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for EdgeDetectionSobelDialog.xaml
    /// </summary>
    public partial class EdgeDetectionSobelDialog : Window
    {
        public ApoImage ImgWip;
        public ApoImage ImgOrig;
        EdgeDetectionSobelModifier _sobel = new EdgeDetectionSobelModifier();

        public EdgeDetectionSobelDialog()
        {
            InitializeComponent();
        }
        public EdgeDetectionSobelDialog(ApoImage img)
        {
            ImgWip = img.Clone();
            ImgOrig = img.Clone();
            InitializeComponent();
            Calculate();
        }

        private void Calculate()
        {
            if (Img == null) return;
            ImgWip = ImgOrig.Clone();
            ImgWip.Modify(_sobel);
            Img.Source = ImgWip.ToImageSource();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void RadioReplicate_OnClick(object sender, RoutedEventArgs e)
        {
            _sobel.BorderTypeVal = BorderType.Replicate;
            Calculate();
        }

        private void RadioReflect_OnClick(object sender, RoutedEventArgs e)
        {
            _sobel.BorderTypeVal  = BorderType.Reflect;
            Calculate();
        }

        private void RadioReflect101_OnClick(object sender, RoutedEventArgs e)
        {
            _sobel.BorderTypeVal = BorderType.Reflect101;
            Calculate();
        }

        private void RadioIsolated_OnClick(object sender, RoutedEventArgs e)
        {
            _sobel.BorderTypeVal = BorderType.Isolated;
            Calculate();
        }


        private void RadioX_OnClick(object sender, RoutedEventArgs e)
        {
            _sobel.XOrder = 1;
            _sobel.YOrder = 0;
            Calculate();
        }

        private void RadioY_OnClick(object sender, RoutedEventArgs e)
        {
            _sobel.XOrder = 1;
            _sobel.YOrder = 0;
            Calculate();
        }

        private bool TextBoxIntKSize_OnValueChanged(int arg)
        {
            if (arg < 1 || arg > 31 || arg % 2 != 1) return false;
            _sobel.KSize = arg;
            Calculate();
            return true;
        }

        private bool TextBoxDoubleScale_OnValueChanged(double arg)
        {
            _sobel.Scale = arg;
            Calculate();
            return true;
        }

        private bool TextBoxDoubleDelta_OnValueChanged(double arg)
        {
            _sobel.Delta = arg;
            Calculate();
            return true;
        }
    }
}