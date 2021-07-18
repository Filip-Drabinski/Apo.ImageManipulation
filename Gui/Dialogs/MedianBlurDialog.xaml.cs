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
using Emgu.CV;
using Emgu.CV.Structure;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    /// Interaction logic for MedianBlurDialog.xaml
    /// </summary>
    public partial class MedianBlurDialog : Window
    {
        public ApoImage ImgWip;
        public ApoImage ImgOrig;
        MedianBlurModifier _blur = new MedianBlurModifier();
        public MedianBlurDialog(ApoImage img)
        {
            ImgWip = img.Clone();
            ImgOrig = img.Clone();
            InitializeComponent();
            Calculate();
        }
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Calculate();
        }

        private void Calculate()
        {
            if(Img == null) return;
            Img.Source = null;
            ImgWip = ImgOrig.Clone();
            ImgWip.Modify(_blur);
            Img.Source = ImgWip.ToImageSource();
        }
        private bool TextBoxInt_OnValueChanged(int arg)
        {
            if (arg < 1 || arg % 2 != 1) return false;
            _blur.KSize = arg;
            Calculate();
            return true;
        }
    }
}
