using System.Windows;
using Apo.Core;
using Apo.Core.ImageModifiersCv;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Size = System.Drawing.Size;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for GaussianBlurDialog.xaml
    /// </summary>
    public partial class GaussianBlurDialog : Window
    {
        public ApoImage ImgWip;
        public ApoImage ImgOrig;
        GaussianBlurModifier _gaussianBlur = new GaussianBlurModifier();
        private BorderType _bt = BorderType.Replicate;

        public GaussianBlurDialog(ApoImage img)
        {
            ImgWip = img.Clone();
            ImgOrig = img.Clone();
            InitializeComponent();
            Calculate();
        }

        public GaussianBlurDialog()
        {
            InitializeComponent();
        }


        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void RadioReplicate_OnClick(object sender, RoutedEventArgs e)
        {
            _bt = BorderType.Replicate;
            Calculate();
        }

        private void RadioReflect_OnClick(object sender, RoutedEventArgs e)
        {
            _bt = BorderType.Reflect;
            Calculate();
        }

        private void RadioReflect101_OnClick(object sender, RoutedEventArgs e)
        {
            _bt = BorderType.Reflect101;
            Calculate();
        }

        private void RadioIsolated_OnClick(object sender, RoutedEventArgs e)
        {
            _bt = BorderType.Isolated;
            Calculate();
        }

        private void Calculate()
        {
            if(Img==null) return;
            Img.Source = null;
            ImgWip = ImgOrig.Clone();
            ImgWip.Modify(_gaussianBlur);
            Img.Source = ImgWip.ToImageSource();
            Model.IsOkEnabled = true;
        }

        private bool TextBoxIntWidth_OnValueChanged(int arg)
        {
            if (arg < 1 || arg % 2 != 1) return false;
            _gaussianBlur.Width = arg;
            Calculate();
            return true;
        }

        private bool TextBoxIntHeight_OnValueChanged(int arg)
        {
            if (arg < 1 || arg % 2 != 1) return false;
             _gaussianBlur.Height = arg;
             Calculate();
            return true;
        }

        private bool TextBoxDoubleSigmaX_OnValueChanged(double arg)
        {
            _gaussianBlur.SigmaX = arg;
            Calculate();
            return true;
        }

        private bool TextBoxDoubleSigmaY_OnValueChanged(double arg)
        {
            _gaussianBlur.SigmaY = arg;
            Calculate();
            return true;
        }
    }
}