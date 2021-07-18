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
using Apo.Core.ImageModifiersCv;
using Emgu.CV.CvEnum;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    /// Interaction logic for FindContoursDialog.xaml
    /// </summary>
    public partial class FindContoursDialog : Window
    {
        public ApoImage ImgWip;
        private readonly ApoImage _imgOrig;
        private readonly FindContoursModifier _findContours;
        private bool inverted = false;
        private bool otsu = false;

        public FindContoursDialog(ApoImage image)
        {
            _imgOrig = image;
            ImgWip = image.Clone();
            _findContours = new FindContoursModifier();
            InitializeComponent();
            Calculate();
        }

        private void Calculate()
        {
            if (Img == null) return;
            ImgWip = _imgOrig.Clone();
            _findContours.ThresholdType = inverted? ThresholdType.BinaryInv: ThresholdType.Binary;
            if (otsu) _findContours.ThresholdType |= ThresholdType.Otsu;
            ImgWip.Modify(_findContours);
            Img.Source = ImgWip.ToImageSource();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void TbBorders_OnChecked(object sender, RoutedEventArgs e)
        {
            _findContours.DrawImageBorder = true;
            Calculate();
        }

        private void TbBorders_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _findContours.DrawImageBorder = false;
            Calculate();
        }

        private void TbInvertThreshold_OnChecked(object sender, RoutedEventArgs e)
        {
            inverted = true;
            Calculate();
        }
        private void TbInvertThreshold_OnUnchecked(object sender, RoutedEventArgs e)
        {
            inverted = false;
            Calculate();
        }

        private void RbOtsu_OnChecked(object sender, RoutedEventArgs e)
        {
            otsu = true;
            Calculate();
        }

        private void RbThreshVal_OnChecked(object sender, RoutedEventArgs e)
        {
            otsu = false;
            Calculate();
        }

        private void SliderThresh_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderThresh.Value = Math.Round(SliderThresh.Value);
            _findContours.Threshold = SliderThresh.Value;
            Calculate();
        }

        private void RbNone_OnChecked(object sender, RoutedEventArgs e)
        {
            _findContours.ChainApprox = ChainApproxMethod.ChainApproxNone;
            Calculate();
        }

        private void RbSimple_OnChecked(object sender, RoutedEventArgs e)
        {
            _findContours.ChainApprox = ChainApproxMethod.ChainApproxSimple;
            Calculate();
        }

        private void RbTc89L1_OnChecked(object sender, RoutedEventArgs e)
        {
            _findContours.ChainApprox = ChainApproxMethod.ChainApproxTc89L1;
            Calculate();
        }

        private void RbTc89Kcos_OnChecked(object sender, RoutedEventArgs e)
        {
            _findContours.ChainApprox = ChainApproxMethod.ChainApproxTc89Kcos;
            Calculate();
        }

        private void RbLinkRuns_OnChecked(object sender, RoutedEventArgs e)
        {
            _findContours.ChainApprox = ChainApproxMethod.LinkRuns;
            Calculate();
        }
    }
}
