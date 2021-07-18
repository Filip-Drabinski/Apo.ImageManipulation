using System.Windows;
using Apo.Core;
using Apo.Core.ImageModifiersCv;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for EdgeDetectionCannyDialog.xaml
    /// </summary>
    public partial class EdgeDetectionCannyDialog : Window
    {
        public EdgeDetectionCannyDialog(ApoImage image)
        {
            ImgWip = image.Clone();
            ImgOrig = image.Clone();
            InitializeComponent();
            Calculate();
        }

        private void Calculate()
        {
            if(Img==null)return;
            ImgWip = ImgOrig.Clone();
            ImgWip.Modify(_canny);
            Img.Source = ImgWip.ToImageSource();
        }

        public ApoImage ImgWip;
        public ApoImage ImgOrig;
        CannyModifier _canny = new CannyModifier();

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private bool TextBoxIntTh1_OnValueChanged(int arg)
        {
            if (arg < 0 || arg > 255) return false;
            _canny.Threshold1 = arg;
            Calculate();
            return true;
        }
        private bool TextBoxIntTh2_OnValueChanged(int arg)
        {
            if (arg < 0 || arg > 255) return false;
            _canny.Threshold2 = arg;
            Calculate();
            return true;
        }


        private void ToggleButtonGradient_OnChecked(object sender, RoutedEventArgs e)
        {
            _canny.L2Gradient = true;
            Calculate();
        }

        private void ToggleButtonGradient_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _canny.L2Gradient = false;
            Calculate();
        }

        private bool TextBoxIntApertureSize_OnValueChanged(int arg)
        {
            if (arg < 3 || arg > 7 || arg % 2 != 1) return false;
            _canny.ApertureSize = arg;
            Calculate();
            return true;
        }
    }
}