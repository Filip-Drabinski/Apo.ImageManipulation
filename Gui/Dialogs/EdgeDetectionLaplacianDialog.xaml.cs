using System;
using System.Windows;
using Apo.Core;
using Apo.Core.ImageModifiersCv;
using Apo.Gui.Models;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for EdgeDetectionLaplacianDialog.xaml
    /// </summary>
    public partial class EdgeDetectionLaplacianDialog : Window
    {
        public ApoImage ImgWip;
        public ApoImage ImgOrig;
        LaplacianModifier _laplacian = new LaplacianModifier();
        private BorderType _bt;

        public EdgeDetectionLaplacianDialog()
        {
            InitializeComponent();
        }
        public EdgeDetectionLaplacianDialog(ApoImage img)
        {
            ImgWip = img.Clone();
            ImgOrig = img.Clone();
            InitializeComponent();
            Calculate();
        }

        private void Calculate()
        {
            if (Img == null) return;
            ImgWip = ImgOrig.Clone();
            ImgWip.Modify(_laplacian);
            Img.Source = ImgWip.ToImageSource();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void RadioReplicate_OnClick(object sender, RoutedEventArgs e)
        {
            _laplacian.BorderTypeVal = BorderType.Replicate;
            Calculate();
        }

        private void RadioReflect_OnClick(object sender, RoutedEventArgs e)
        {
            _laplacian.BorderTypeVal = BorderType.Reflect;
            Calculate();
        }

        private void RadioReflect101_OnClick(object sender, RoutedEventArgs e)
        {
            _laplacian.BorderTypeVal = BorderType.Reflect101;
            Calculate();
        }

        private void RadioIsolated_OnClick(object sender, RoutedEventArgs e)
        {
            _laplacian.BorderTypeVal = BorderType.Isolated;
            Calculate();
        }

        private bool TextBoxIntKSize_OnValueChanged(int arg)
        {
            if (arg < 1 || arg > 31 || arg % 2 != 1) return false;
            _laplacian.KSize = arg;
            Calculate();
            return true;
        }


        private bool TextBoxDoubleScale_OnValueChanged(double arg)
        {
            _laplacian.Scale = arg;
            Calculate();
            return true;
        }

        private bool TextBoxDoubleDelta_OnValueChanged(double arg)
        {
            _laplacian.Delta = arg;
            Calculate();
            return true;
        }
    }
}