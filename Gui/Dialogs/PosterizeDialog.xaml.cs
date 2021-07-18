using System;
using System.Windows;
using Apo.Core;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for PosterizeDialog.xaml
    /// </summary>
    public partial class PosterizeDialog : Window
    {
        private readonly ApoImage _imgOrig;
        public ApoImage ImgWip;

        public PosterizeDialog()
        {
            InitializeComponent();
        }

        public PosterizeDialog(ApoImage image)
        {
            _imgOrig = image;
            ImgWip = image.Clone();
            InitializeComponent();
            SliderNum.Value = 256;
            Calculate();
        }

        private void Calculate()
        {
            var numOfColors = (int) SliderNum.Value;
            var step = 255.0d / numOfColors;
            var colorsNew = new byte[256];
            var cnIter = 0;
            for (var i = 1; i < numOfColors + 1; i++)
            {
                var nextStep = Math.Round(i * step);
                var color = (byte)Math.Round((i - 1) * step);
                if (i == numOfColors) color = 255;
                for (; cnIter < nextStep && cnIter < colorsNew.Length; cnIter++) colorsNew[cnIter] = color;
            }

            for (var x = 0; x < _imgOrig.Width; x++)
            for (var y = 0; y < _imgOrig.Height; y++)
                ImgWip.SetPixel(0,y,x, colorsNew[_imgOrig.Pixels[y, x, 0]]);
            /*for (int i = 1; i < numOfColors+1; i++)
            {
            }*/
            Img.Source = ImgWip.ToImageSource();
        }

        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderNum.Value = Math.Round(SliderNum.Value);
            SliderNum.IsEnabled = false;
            Calculate();
            SliderNum.IsEnabled = true;
        }

        private void ButtonCalculate_OnClick(object sender, RoutedEventArgs e)
        {
            Calculate();
        }
    }
}