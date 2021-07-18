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
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    /// Interaction logic for ThresholdingSegmentationDialog.xaml
    /// </summary>
    public partial class ThresholdingSegmentationDialog : Window
    {
        public ThresholdingSegmentationDialog(ApoImage image)
        {
            _imgOrig = image;
            ImgWip = image.Clone();
            InitializeComponent();
        }


        public ApoImage ImgWip;
        private readonly ApoImage _imgOrig;
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void SliderThreshold_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ImgWip = _imgOrig.Clone();
            SliderThreshold.Value = Math.Round(SliderThreshold.Value);
            ImgWip.Modify(new SegmentationThresholding(SliderThreshold.Value));
            Img.Source = ImgWip.ToImageSource();
        }
    }
}
