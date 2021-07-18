using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Apo.Core;
using Apo.Core.ImageModifiers;
using Apo.Core.ImageModifiersCv;
using Emgu.CV;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    /// Interaction logic for SegmentationWatershedDialog.xaml
    /// </summary>
    public partial class SegmentationWatershedDialog : Window
    {
        public SegmentationWatershedDialog(ApoImage image)
        {
            _imgOrig = image;
            ImgWip = image.Clone();
            InitializeComponent();
            Calculate();
        }

        private void Calculate()
        {
            ImgWip = _imgOrig.Clone();
            var wat = new SegmentationWatershed();
            ImgWip.Modify(wat);
            Img1.Source = wat.Threshold.ToBitmap().ToImageSource();
            Img2.Source = wat.Opening.ToBitmap().ToImageSource();
            Img3.Source = wat.SureBg.ToBitmap().ToImageSource();
            Img4.Source = wat.SureFg.ToBitmap().ToImageSource();
            Img5.Source = wat.Unknown.ToBitmap().ToImageSource();
            Img6.Source = wat.Markers.ToBitmap().ToImageSource();
            Img7.Source = wat.Markers2.ToBitmap().ToImageSource();
            Img8.Source = wat.ImageGray.ToBitmap().ToImageSource();
            ImgRes.Source = ImgWip.ToImageSource();
        }


        public ApoImage ImgWip;
        private readonly ApoImage _imgOrig;

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void TbResult_OnChecked(object sender, RoutedEventArgs e)
        {
            Sv.Visibility = Visibility.Collapsed;
        }

        private void TbSteps_OnChecked(object sender, RoutedEventArgs e)
        {
            Sv.Visibility = Visibility.Visible;
        }
    }
}
