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
    /// Interaction logic for AdaptiveThresholdingDialog.xaml
    /// </summary>
    public partial class AdaptiveThresholdingDialog : Window
    {
        public AdaptiveThresholdingDialog(ApoImage image)
        {
            _imgOrig = image;
            ImgWip = image.Clone();
            _adaptiveThresholding = new SegmentationAdaptiveThresholding(AdaptiveThresholdType.MeanC, ThresholdType.Binary, 11, 0);
            InitializeComponent();
            Calculate();
        }


        public ApoImage ImgWip;
        private readonly ApoImage _imgOrig;
        private readonly SegmentationAdaptiveThresholding _adaptiveThresholding;
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private bool TbiBlockSize_OnValueChanged(int arg)
        {
            if (arg % 2 == 1 && arg > 1)
            {
                _adaptiveThresholding.BlockSize = arg;
                Calculate();
                return true;
            } 
            return false;

        }
        private bool TbiConstant_OnValueChanged(int arg)
        {
            _adaptiveThresholding.Param1 = arg;
            Calculate();
            return true;
        }

        private void TbMean_OnChecked(object sender, RoutedEventArgs e)
        {
            _adaptiveThresholding.AdaptiveType = AdaptiveThresholdType.MeanC;
            Calculate();
        }
        private void TbGaussian_OnChecked(object sender, RoutedEventArgs e)
        {
            _adaptiveThresholding.AdaptiveType = AdaptiveThresholdType.GaussianC;
            Calculate();
        }
        private void TbBinary_OnChecked(object sender, RoutedEventArgs e)
        {
            _adaptiveThresholding.ThresholdType = ThresholdType.Binary;
            Calculate();
        }
        private void TbBinaryInverted_OnChecked(object sender, RoutedEventArgs e)
        {
            _adaptiveThresholding.ThresholdType = ThresholdType.BinaryInv;
            Calculate();
        }

        private void Calculate()
        {
            if(Img==null )return;
            ImgWip = _imgOrig.Clone();
            ImgWip.Modify(_adaptiveThresholding);
            Img.Source = ImgWip.ToImageSource();
        }
    }
}
