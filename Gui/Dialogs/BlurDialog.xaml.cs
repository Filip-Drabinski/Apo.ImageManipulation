using System.Windows;
using Apo.Core;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for BlurDialog.xaml
    /// </summary>
    public partial class BlurDialog : Window
    {
        public ApoImage ImgWip;
        private Image<Gray, byte> _imgWip;
        private BorderType _bt = BorderType.Replicate;

        public BlurDialog(ApoImage img)
        {
            InitializeComponent();
            ImgWip = img.Clone();
            _imgWip = img.ToOpenCv();
        }

        public BlurDialog()
        {
            InitializeComponent();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            for (var x = 0; x < ImgWip.Width; x++)
            for (var y = 0; y < ImgWip.Height; y++)
                ImgWip.SetPixel(0,y,x, _imgWip.Data[y, x, 0]);
        }

        private void ButtonCalculate_OnClick(object sender, RoutedEventArgs e)
        {
            Img.Source = null;
            _imgWip = ImgWip.ToOpenCv();
            CvInvoke.Blur(_imgWip, _imgWip, new Size(Model.Width, Model.Height), new Point(-1, -1), _bt);
            Img.Source = _imgWip.ToBitmap().ToImageSource();
            Model.IsOkEnabled = true;
        }

        private void RadioReplicate_OnClick(object sender, RoutedEventArgs e)
        {
            _bt = BorderType.Replicate;
        }

        private void RadioReflect_OnClick(object sender, RoutedEventArgs e)
        {
            _bt = BorderType.Reflect;
        }

        private void RadioReflect101_OnClick(object sender, RoutedEventArgs e)
        {
            _bt = BorderType.Reflect101;
        }

        private void RadioIsolated_OnClick(object sender, RoutedEventArgs e)
        {
            _bt = BorderType.Isolated;
        }
    }
}