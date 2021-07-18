using System;
using System.Drawing;
using System.Windows;
using Apo.Core;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Point = System.Drawing.Point;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for LinearSharpenDialog.xaml
    /// </summary>
    public partial class LinearSharpenDialog : Window
    {
        public ApoImage ImgWip;
        private Image<Gray, byte> _imgWip;
        public LinearSharpenDialog(ApoImage img)
        {
            ImgWip = img.Clone();
            _imgWip = img.ToOpenCv();
            InitializeComponent();
            Calculate();
        }

        private void Calculate()
        {
            var kernel = new Mat(3,3,DepthType.Cv64F,1);
            kernel.SetTo<double>(new double[]{
                0, -1, 0,
                -1, 5, -1,
                0, -1, 0});
            CvInvoke.Filter2D(_imgWip,_imgWip,kernel,new Point(-1,-1),0,BorderType.Replicate);
            Img.Source = _imgWip.ToBitmap().ToImageSource();
        }

        public LinearSharpenDialog()
        {
            InitializeComponent();
        }


        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            for (var x = 0; x < ImgWip.Width; x++)
            for (var y = 0; y < ImgWip.Height; y++)
                ImgWip.SetPixel(0,y,x,_imgWip.Data[y, x, 0]);;
        }

        private void ButtonBase1_OnClick(object sender, RoutedEventArgs e)
        {Calculate();
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            _imgWip = ImgWip.ToOpenCv();
            var kernel = new Mat(3, 3, DepthType.Cv64F, 1);
            kernel.SetTo<double>(new double[]
            {

                -1, -1, -1,
                -1, 9, -1,
                -1, -1, -1,
            });
            CvInvoke.Filter2D(_imgWip, _imgWip, kernel, new Point(-1, -1), 0, BorderType.Replicate);
            Img.Source = _imgWip.ToBitmap().ToImageSource();
        }

        private void ButtonBase3_OnClick(object sender, RoutedEventArgs e)
        {
            _imgWip = ImgWip.ToOpenCv();
            var kernel = new Mat(3, 3, DepthType.Cv64F, 1);
            kernel.SetTo<double>(new double[]
            {
                1, -2, 1,
                -2, 5, -2,
                1, -2, 1
            });
            CvInvoke.Filter2D(_imgWip, _imgWip, kernel, new Point(-1, -1), 0, BorderType.Replicate);
            Img.Source = _imgWip.ToBitmap().ToImageSource();
        }
    }
}