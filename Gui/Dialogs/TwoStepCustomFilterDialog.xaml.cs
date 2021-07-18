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
    /// Interaction logic for TwoStepCustomFilterDialog.xaml
    /// </summary>
    public partial class TwoStepCustomFilterDialog : Window
    {
        private readonly ApoImage ImgOrig;
        public ApoImage ImgWip;

        private Custom2dFilterModifier _filter2d5x5;
        private Custom2dFilterModifier _filter2dI;
        private Custom2dFilterModifier _filter2dII;
        public TwoStepCustomFilterDialog(ApoImage img)
        {
            ImgOrig = img.Clone();
            ImgWip = img.Clone();
            _filter2d5x5 = new Custom2dFilterModifier(5);
            _filter2dI = new Custom2dFilterModifier();
            _filter2dII = new Custom2dFilterModifier();
            InitializeComponent(); 
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void ButtonCalculate5x5_OnClick(object sender, RoutedEventArgs e)
        {
            ImgWip = ImgOrig.Clone();
            ImgWip.Modify(new ImageCopyPixels(ImgOrig));
            Img.Source = null;
            ImgWip.Modify(_filter2d5x5);
            Img.Source = ImgWip.ToImageSource();
        }

        private void ButtonCalculate2x3x3_OnClick(object sender, RoutedEventArgs e)
        {
            ImgWip = ImgOrig.Clone();
            Img.Source = null;
            ImgWip.Modify(_filter2dI);
            ImgWip.Modify(_filter2dII);
            Img.Source = ImgWip.ToImageSource();
        }

        private bool TextBoxDoubleA0XOO_OnValueChanged(double arg)
        {
            _filter2dI.Values[0] = arg;
            return true;
        }

        private bool TextBoxDoubleA0OXO_OnValueChanged(double arg)
        {
            _filter2dI.Values[1] = arg;
            return true;
        }

        private bool TextBoxDoubleA0OOX_OnValueChanged(double arg)
        {
            _filter2dI.Values[2] = arg;
            return true;
        }

        private bool TextBoxDoubleA1XOO_OnValueChanged(double arg)
        {
            _filter2dI.Values[3] = arg;
            return true;
        }

        private bool TextBoxDoubleA1OXO_OnValueChanged(double arg)
        {
            _filter2dI.Values[4] = arg;
            return true;
        }

        private bool TextBoxDoubleA1OOX_OnValueChanged(double arg)
        {
            _filter2dI.Values[5] = arg;
            return true;
        }

        private bool TextBoxDoubleA2XOO_OnValueChanged(double arg)
        {
            _filter2dI.Values[6] = arg;
            return true;
        }

        private bool TextBoxDoubleA2OXO_OnValueChanged(double arg)
        {
            _filter2dI.Values[7] = arg;
            return true;
        }

        private bool TextBoxDoubleA2OOX_OnValueChanged(double arg)
        {
            _filter2dI.Values[8] = arg;
            return true;
        }
        //=================================================================
        private bool TextBoxDoubleB0XOO_OnValueChanged(double arg)
        {
            _filter2dII.Values[0] = arg;
            return true;
        }

        private bool TextBoxDoubleB0OXO_OnValueChanged(double arg)
        {
            _filter2dII.Values[1] = arg;
            return true;
        }

        private bool TextBoxDoubleB0OOX_OnValueChanged(double arg)
        {
            _filter2dII.Values[2] = arg;
            return true;
        }

        private bool TextBoxDoubleB1XOO_OnValueChanged(double arg)
        {
            _filter2dII.Values[3] = arg;
            return true;
        }

        private bool TextBoxDoubleB1OXO_OnValueChanged(double arg)
        {
            _filter2dII.Values[4] = arg;
            return true;
        }

        private bool TextBoxDoubleB1OOX_OnValueChanged(double arg)
        {
            _filter2dII.Values[5] = arg;
            return true;
        }

        private bool TextBoxDoubleB2XOO_OnValueChanged(double arg)
        {
            _filter2dII.Values[6] = arg;
            return true;
        }

        private bool TextBoxDoubleB2OXO_OnValueChanged(double arg)
        {
            _filter2dII.Values[7] = arg;
            return true;
        }

        private bool TextBoxDoubleB2OOX_OnValueChanged(double arg)
        {
            _filter2dII.Values[8] = arg;
            return true;
        }
        //=================================================================
        private bool TextBoxDouble0XOOOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[0] = arg;
            return true;
        }

        private bool TextBoxDouble0OXOOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[1] = arg;
            return true;
        }

        private bool TextBoxDouble0OOXOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[2] = arg;
            return true;
        }

        private bool TextBoxDouble0OOOXO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[3] = arg;
            return true;
        }

        private bool TextBoxDouble0OOOOX_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[4] = arg;
            return true;
        }

        private bool TextBoxDouble1XOOOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[5] = arg;
            return true;
        }

        private bool TextBoxDouble1OXOOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[6] = arg;
            return true;
        }

        private bool TextBoxDouble1OOXOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[7] = arg;
            return true;
        }

        private bool TextBoxDouble1OOOXO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[8] = arg;
            return true;
        }

        private bool TextBoxDouble1OOOOX_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[9] = arg;
            return true;
        }

        private bool TextBoxDouble2XOOOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[10] = arg;
            return true;
        }

        private bool TextBoxDouble2OXOOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[11] = arg;
            return true;
        }

        private bool TextBoxDouble2OOXOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[12] = arg;
            return true;
        }

        private bool TextBoxDouble2OOOXO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[13] = arg;
            return true;
        }

        private bool TextBoxDouble2OOOOX_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[14] = arg;
            return true;
        }

        private bool TextBoxDouble3XOOOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[15] = arg;
            return true;
        }

        private bool TextBoxDouble3OXOOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[16] = arg;
            return true;
        }

        private bool TextBoxDouble3OOXOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[17] = arg;
            return true;
        }

        private bool TextBoxDouble3OOOXO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[18] = arg;
            return true;
        }
        private bool TextBoxDouble3OOOOX_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[19] = arg;
            return true;
        }

        private bool TextBoxDouble4XOOOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[20] = arg;
            return true;
        }
        private bool TextBoxDouble4OXOOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[21] = arg;
            return true;
        }

        private bool TextBoxDouble4OOXOO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[22] = arg;
            return true;
        }

        private bool TextBoxDouble4OOOXO_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[23] = arg;
            return true;
        }

        private bool TextBoxDouble4OOOOX_OnValueChanged(double arg)
        {
            _filter2d5x5.Values[24] = arg;
            return true;
        }


    }
}
