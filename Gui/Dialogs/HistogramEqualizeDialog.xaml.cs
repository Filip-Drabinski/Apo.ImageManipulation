using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Apo.Core;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for HistogramEqualizeDialog.xaml
    /// </summary>
    public partial class HistogramEqualizeDialog : Window
    {
        public readonly ApoImage ImageWip;
        private readonly ApoImage _imageOrig;

        public HistogramEqualizeDialog()
        {
            InitializeComponent();
        }

        public HistogramEqualizeDialog(ApoImage img)
        {
            _imageOrig = img;
            ImageWip = img.Duplicate(0);
            InitializeComponent();
            HistUc.Model.ImageSourceBefore = _imageOrig.ToImageSource();
            var histOrig = new ApoHistogram(_imageOrig);
            HistUc.BarChartBefore.SetData(histOrig[0].Data, Color.FromRgb(0, 0, 0));
            CalculateImage();
            var histWip = new ApoHistogram(ImageWip);
            HistUc.BarChartAfter.SetData(histWip[0].Data, Color.FromRgb(0, 0, 0));
            HistUc.Model.ImageSourceAfter = ImageWip.ToImageSource();
        }

        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private int[] CumulativeSum(int[] lut)
        {
            var res = new List<int>();
            res.Add(lut[0]);
            for (var it = 1; it < lut.Length; it++)
            {
                var tmp = res[^1] + lut[it];
                res.Add(tmp);
            }


            return res.ToArray();
        }

        private void CalculateImage()
        {
            /*
            new HistogramEqualizer().Work(out var data, ImageWip);
            ImageWip.Pixels = data;
            */
            var histWip = new ApoHistogram(_imageOrig);
            var cs = CumulativeSum(histWip[0].Data);
            var csNo0 = cs.Except(new List<int> {0}).ToArray();
            var csMax = csNo0[0];
            var csMin = csNo0[0];
            foreach (var val in csNo0)
            {
                csMax = val > csMax ? val : csMax;
                csMin = val < csMin ? val : csMin;
            }

            cs = cs.Select(val => val != 0 ? (int) ((double) (val - csMin) * 255 / (csMax - csMin)) : 0).ToArray();
            HistUc.BarChartAfter.SetData(cs, Color.FromRgb(0, 0, 0));
            for (var x = 0; x < ImageWip.Width; x++)
            for (var y = 0; y < ImageWip.Height; y++)
                ImageWip.SetPixel(0,y,x, (byte)cs[_imageOrig.Pixels[y, x, 0]]);
        }
    }
}