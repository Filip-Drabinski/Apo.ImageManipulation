using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Apo.Core;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for HistogramStretchDialog.xaml
    /// </summary>
    public partial class HistogramStretchDialog : Window
    {
        public readonly ApoImage ImageWip;
        private readonly ApoImage _imageOrig;
        private readonly double _origMin;
        private readonly double _origMax;
        private readonly bool _created;

        public HistogramStretchDialog()
        {
            InitializeComponent();
        }

        public HistogramStretchDialog(ApoImage img)
        {
            _imageOrig = img;
            ImageWip = img.Duplicate(0);
            _origMax = _imageOrig.Pixels[0,0, 0];
            _origMin = _imageOrig.Pixels[0,0, 0];
            for (var x = 0; x < ImageWip.Width; x++)
            for (var y = 0; y < ImageWip.Height; y++)
            {
                _origMax = _imageOrig.Pixels[y,x,0] > _origMax ? _imageOrig.Pixels[y,x,0] : _origMax;
                _origMin = _imageOrig.Pixels[y,x,0] < _origMin ? _imageOrig.Pixels[y,x,0] : _origMin;
            }

            InitializeComponent();
            Model.SliderMaxVal = (int) _origMax;
            Model.SliderMinVal = (int) _origMin;
            HistUc.Model.ImageSourceBefore = _imageOrig.ToImageSource();
            HistUc.Model.ImageSourceAfter = ImageWip.ToImageSource();
            var histOrig = new ApoHistogram(_imageOrig);
            var hist = new ApoHistogram(_imageOrig);
            HistUc.BarChartBefore.SetData(histOrig[0].Data, Color.FromRgb(0, 0, 0));
            HistUc.BarChartAfter.SetData(hist[0].Data, Color.FromRgb(0, 0, 0));
            _created = true;
        }

        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void SliderMin_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderMin.Value = Math.Round(SliderMin.Value);
            CalculateAfter();
        }

        private void SliderMax_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderMax.Value = Math.Round(SliderMax.Value);
            CalculateAfter();
        }

        private void CalculateAfter()
        {
            if (!_created) return;
            var minNew = SliderMin.Value;
            var maxNew = SliderMax.Value;
            for (var x = 0; x < ImageWip.Width; x++)
            for (var y = 0; y < ImageWip.Height; y++)
            {
                var px = _imageOrig.Pixels[y,x,0];
                var rangeOld = _origMax - _origMin;
                var rangeNew = maxNew - minNew;
                var pxDistFromMinOld = px - _origMin;
                var pxPercOfOldRange = pxDistFromMinOld / rangeOld;
                var pxInRangeNew = pxPercOfOldRange * rangeNew;
                var pxNew = minNew + pxInRangeNew;
                ImageWip.SetPixel(0,y,x, (byte)Math.Round(pxNew));
            }

            var hist = new ApoHistogram(ImageWip);
            HistUc.Model.ImageSourceAfter = ImageWip.ToImageSource();
            HistUc.BarChartAfter.SetData(hist[0].Data, Color.FromRgb(0, 0, 0));
        }
    }
}