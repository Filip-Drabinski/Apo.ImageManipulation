using System.Windows;
using Apo.Core;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for ThresholdingDialog.xaml
    /// </summary>
    public partial class ThresholdingDialog : Window
    {
        private readonly ApoImage _imgOrig;
        public ApoImage ImgWip;

        public ThresholdingDialog()
        {
            InitializeComponent();
        }

        public ThresholdingDialog(ApoImage image)
        {
            _imgOrig = image;
            ImgWip = image.Clone();
            InitializeComponent();
            Calculate();
        }

        private void SliderMin_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Calculate();
        }

        private void SliderMax_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Calculate();
        }

        private void SliderBotVal_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Calculate();
        }

        private void SliderTopVal_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Calculate();
        }

        private void Calculate()
        {
            for (var x = 0; x < ImgWip.Width; x++)
            for (var y = 0; y < ImgWip.Height; y++)
                if (_imgOrig.Pixels[y,x,0] <= Model.SliderMinVal) ImgWip.SetPixel(0,y,x, (byte)Model.BottomVal);
                else if (_imgOrig.Pixels[y,x,0] >= Model.SliderMaxVal)
                    ImgWip.SetPixel(0,y,x, (byte)Model.TopVal);
                else
                    ImgWip.SetPixel(0,y,x, _imgOrig.Pixels[y,x,0]);

            Model.Bitmap = ImgWip.ToImageSource();
        }

        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}