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
    ///     Interaction logic for DirectionalEdgeDetectionDialog.xaml
    /// </summary>
    public partial class DirectionalEdgeDetectionDialog : Window
    {
        public ApoImage ImgWip;
        private Image<Gray, byte> _imgWip;
        private Mat _mask = CreateMaskSE();

        public DirectionalEdgeDetectionDialog(ApoImage img)
        {
            ImgWip = img.Clone();
            _imgWip = img.ToOpenCv();
            InitializeComponent();
            Calculate();
        }

        private void Calculate()
        {
            _imgWip = ImgWip.ToOpenCv();
            Img.Source = null;
            if (_all)
            {
                var dn = new Image<Gray, byte>(_imgWip.Width, _imgWip.Height);
                CvInvoke.Filter2D(_imgWip, dn, CreateMaskN(), new Point(-1, -1), 0, BorderType.Replicate);
                var de = new Image<Gray, byte>(_imgWip.Width, _imgWip.Height);
                CvInvoke.Filter2D(_imgWip, de, CreateMaskE(), new Point(-1, -1), 0, BorderType.Replicate);
                var ds = new Image<Gray, byte>(_imgWip.Width, _imgWip.Height);
                CvInvoke.Filter2D(_imgWip, ds, CreateMaskS(), new Point(-1, -1), 0, BorderType.Replicate);
                var dw = new Image<Gray, byte>(_imgWip.Width, _imgWip.Height);
                CvInvoke.Filter2D(_imgWip, dw, CreateMaskW(), new Point(-1, -1), 0, BorderType.Replicate);
                CvInvoke.BitwiseOr(dn, de, _imgWip);
                CvInvoke.BitwiseOr(_imgWip, ds, _imgWip);
                CvInvoke.BitwiseOr(_imgWip, dw, _imgWip);
            }
            else
            {
                CvInvoke.Filter2D(_imgWip, _imgWip, _mask, new Point(-1, -1), 0, BorderType.Replicate);
            }
            Img.Source = _imgWip.ToBitmap().ToImageSource();
        }



        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonN_OnClick(object sender, RoutedEventArgs e)
        {
            _all = false;
            _mask = CreateMaskN();
            Calculate();
        }

        private void ButtonNW_OnClick(object sender, RoutedEventArgs e)
        {
            _all = false;
            _mask = CreateMaskNW();
            Calculate();
        }
        private void ButtonNE_OnClick(object sender, RoutedEventArgs e)
        {
            _all = false;
            _mask = CreateMaskNE();
            Calculate();
        }

        private void ButtonE_OnClick(object sender, RoutedEventArgs e)
        {
            _all = false;
            _mask = CreateMaskE();
            Calculate();
        }
        private void ButtonS_OnClick(object sender, RoutedEventArgs e)
        {
            _all = false;
            _mask = CreateMaskS();
            Calculate();
        }
        private void ButtonSW_OnClick(object sender, RoutedEventArgs e)
        {
            _all = false;
            _mask = CreateMaskSW();
            Calculate();
        }
        private void ButtonSE_OnClick(object sender, RoutedEventArgs e)
        {
            _all = false;
            _mask = CreateMaskSE();
            Calculate();
        }
        private void ButtonW_OnClick(object sender, RoutedEventArgs e)
        {
            _all = false;
            _mask = CreateMaskW();
            Calculate();
        }
        private static Mat CreateMaskN()
        {
            var res = new Mat(3, 3, DepthType.Cv64F, 1);
            res.SetTo(new double[]
            {
                1,1,1,
                1,-2,1,
                -1,-1,-1
            });
            return res;
        }
        private static Mat CreateMaskSE()
        {
            var res = new Mat(3, 3, DepthType.Cv64F, 1);
            res.SetTo(new double[]
            {
                -1,-1, 0,
                -1, 0, 1,
                 0, 1, 1
            });
            return res;
        }
        private static Mat CreateMaskSW()
        {
            var res = new Mat(3, 3, DepthType.Cv64F, 1);
            res.SetTo(new double[]
            {
                 0,-1,-1,
                 1, 0,-1,
                 1, 1, 0
            });
            return res;
        }
        private static Mat CreateMaskNW()
        {
            var res = new Mat(3, 3, DepthType.Cv64F, 1);
            res.SetTo(new double[]
            {
                 1, 1, 0,
                 1, 0,-1,
                 0,-1,-1
            });
            return res;
        }
        private static Mat CreateMaskNE()
        {
            var res = new Mat(3, 3, DepthType.Cv64F, 1);
            res.SetTo(new double[]
            {
                 0, 1, 1,
                -1, 0, 1,
                -1,-1, 0
            });
            return res;
        }
        private static Mat CreateMaskE()
        {
            var res = new Mat(3, 3, DepthType.Cv64F, 1);
            res.SetTo(new double[] { 
                -1, 1, 1,
                -1, -2, 1,
                -1, 1, 1 });
            return res;
        }
        private static Mat CreateMaskS()
        {
            var res = new Mat(3, 3, DepthType.Cv64F, 1); res.SetTo(new double[] {
                -1, -1, -1,
                1, -2, 1,
                1, 1, 1 });
            return res;
        }
        private static Mat CreateMaskW()
        {
            var res = new Mat(3, 3, DepthType.Cv64F, 1); res.SetTo(new double[] {
                1, 1, -1,
                1, -2, -1,
                1, 1, -1 });
            return res;
        }

        private bool _all = false;
        private void ButtonAll_OnClick(object sender, RoutedEventArgs e)
        {
            _all = true;
            Calculate();
        }
    }
}