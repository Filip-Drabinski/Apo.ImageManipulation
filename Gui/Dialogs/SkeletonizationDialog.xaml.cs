using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    /// Interaction logic for SkeletonizationDialog.xaml
    /// </summary>
    public partial class SkeletonizationDialog : Window
    {
        public readonly ApoImage ImgWip;
        public SkeletonizationDialog(ApoImage img)
        {
            ImgWip = img.Clone();
            InitializeComponent();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }


        private void BtnCalculate_OnClick(object sender, RoutedEventArgs e)
        {
            BtnOk.IsEnabled = false;
            ImgWip.Modify(new ImageSkeletonization());
            Img.Source = ImgWip.ToImageSource();
            BtnOk.IsEnabled = true;
        }
    }
}
