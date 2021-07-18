using System;
using System.Windows;
using Apo.Core;
using Apo.Core.ImageModifiersCv;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for CustomFilterDialog.xaml
    /// </summary>
    public partial class CustomFilterDialog : Window
    {
        public ApoImage ImgWip;
        public ApoImage ImgOrig;
        Custom2dFilterModifier _filter = new Custom2dFilterModifier();
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public CustomFilterDialog(ApoImage img)
        {
            ImgWip = img.Clone();
            ImgOrig = img.Clone();
            InitializeComponent();
            Calculate();
        }

        private void Calculate()
        {
            if(Img == null)return;
            ImgWip = ImgOrig.Clone();
            Img.Source = null;
            ImgWip.Modify(_filter);
            Img.Source = ImgWip.ToImageSource();
        }

        private bool TextBoxDouble0XOO_OnValueChanged(double arg)
        {
            _filter.Values[0] = arg;
            Calculate();
            return true;
        }
        private bool TextBoxDouble0OXO_OnValueChanged(double arg)
        {
            _filter.Values[1] = arg;
            Calculate();
            return true;
        }
        private bool TextBoxDouble0OOX_OnValueChanged(double arg)
        {
            _filter.Values[2] = arg;
            Calculate();
            return true;
        }
        private bool TextBoxDouble1XOO_OnValueChanged(double arg)
        {
            _filter.Values[3] = arg;
            Calculate();
            return true;
        }
        private bool TextBoxDouble1OXO_OnValueChanged(double arg)
        {
            _filter.Values[4] = arg;
            Calculate();
            return true;
        }
        private bool TextBoxDouble1OOX_OnValueChanged(double arg)
        {
            _filter.Values[5] = arg;
            Calculate();
            return true;
        }
        private bool TextBoxDouble2XOO_OnValueChanged(double arg)
        {
            _filter.Values[6] = arg;
            Calculate();
            return true;
        }
        private bool TextBoxDouble2OXO_OnValueChanged(double arg)
        {
            _filter.Values[7] = arg;
            Calculate();
            return true;
        }
        private bool TextBoxDouble2OOX_OnValueChanged(double arg)
        {
            _filter.Values[8] = arg;
            Calculate();
            return true;
        }
    }
}