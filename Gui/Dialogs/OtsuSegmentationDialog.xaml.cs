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
using Emgu.CV.CvEnum;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    /// Interaction logic for OtsuSegmentationDialog.xaml
    /// </summary>
    public partial class OtsuSegmentationDialog : Window
    {
        public OtsuSegmentationDialog(ApoImage image)
        {
            _imgOrig = image;
            ImgWip = image.Clone();
            _segmentationOtsu = new SegmentationOtsu(true,5,5,0,0,BorderType.Replicate);
            InitializeComponent();
            Calculate();
        }



        public ApoImage ImgWip;
        private readonly ApoImage _imgOrig;
        private readonly SegmentationOtsu _segmentationOtsu;
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private bool TbiWidth_OnValueChanged(int arg)
        {
            if (arg >= 1 && arg % 2 == 1)
            {
                _segmentationOtsu.Width = arg;
                Calculate();
                return true;
            }

            return false;
        }

        private bool TbiHeight_OnValueChanged(int arg)
        {
            if (arg >= 1 && arg % 2 == 1)
            {
                _segmentationOtsu.Height = arg;
                Calculate();
                return true;
            }

            return false;
        }

        private bool TbiSigmaX_OnValueChanged(double arg)
        {
            _segmentationOtsu.SigmaX = arg;
            Calculate();
            return true;
        }

        private bool TbiSigmaY_OnValueChanged(double arg)
        {
            _segmentationOtsu.SigmaY = arg;
            Calculate();
            return true;
        }

        private void RadioReplicate_OnClick(object sender, RoutedEventArgs e)
        {
            _segmentationOtsu.BorderType = BorderType.Replicate;
            Calculate();
        }

        private void RadioIsolated_OnClick(object sender, RoutedEventArgs e)
        {
            _segmentationOtsu.BorderType = BorderType.Isolated;
            Calculate();
        }

        private void RadioReflect_OnClick(object sender, RoutedEventArgs e)
        {
            _segmentationOtsu.BorderType = BorderType.Reflect;
            Calculate();
        }

        private void RadioReflect101_OnClick(object sender, RoutedEventArgs e)
        {
            _segmentationOtsu.BorderType = BorderType.Reflect101;
            Calculate();
        }
        private void Calculate()
        {
            if (Img == null) return;
            ImgWip = _imgOrig.Clone();
            ImgWip.Modify(_segmentationOtsu);
            Img.Source = ImgWip.ToImageSource();
        }

        private void CbBlur_OnChecked(object sender, RoutedEventArgs e)
        {
            _segmentationOtsu.UseBlur = true;
            Calculate();
        }

        private void CbBlur_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _segmentationOtsu.UseBlur = false;
            Calculate();
        }
    }
}
