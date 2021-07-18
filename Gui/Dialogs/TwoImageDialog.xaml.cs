using System;
using System.Collections.Generic;
using System.Linq;
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
using Emgu.CV;
using Emgu.CV.Structure;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    /// Interaction logic for TwoImageDialog.xaml
    /// </summary>
    public partial class TwoImageDialog : Window
    {
        public ApoImage ImgWip;
        private ApoImage _imgOrig;
        private Image<Gray, byte> _imgWip;
        private ApoImage _otherImage;
        private List<ApoImage> _images;
        public TwoImageDialog(ApoImage img)
        {
            ImgWip = img.Clone();
            _imgOrig = img.Clone();
            _imgWip = img.ToOpenCv();
            _images = new List<ApoImage>();
            InitializeComponent();
            ImgInpA.Source = _imgOrig.ToImageSource();
            ImgInpB.Source = null;
            GetOtherImages();
        }

        private void GetOtherImages()
        {
            _images.Clear();
            ImgInpB.Source = null;
            NotificationSystem.Instance.NotifyAll(Message.CollectImages,_images);
            List<string> names = new List<string>();
            foreach (var img in _images)
            {
                names.Add(img.Path);
            }
            CbImages.ItemsSource = names;
            CbImages.SelectedIndex = 0;
            ImgInpB.Source = _images.First().ToImageSource();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            for (var x = 0; x < ImgWip.Width; x++)
            for (var y = 0; y < ImgWip.Height; y++)
                ImgWip.SetPixel(0,y,x,_imgWip.Data[y, x, 0]);;
        }

        private void ButtonCalculate_OnClick(object sender, RoutedEventArgs e)
        {
            ImgResult.Source = null;
            _imgWip = ImgWip.ToOpenCv();

            Image<Gray, byte> selecImg = _otherImage.ToOpenCv();
            Image<Gray, byte> img2 = new Image<Gray, byte>(_imgWip.Width, _imgWip.Height);
            if (RbPadding.IsChecked == true)
            {
                for (int x = 0; x < img2.Width; x++)
                {
                    for (int y = 0; y < img2.Height; y++)
                    {
                        if (x < selecImg.Width && y < selecImg.Height)
                        {
                            img2.Data[y, x, 0] = selecImg.Data[y, x, 0];
                        }
                        else
                        {
                            img2.Data[y, x, 0] = 0;
                        }
                    }
                }
                
            }
            else
            {
                CvInvoke.Resize(selecImg, img2,new Size(_imgWip.Width,_imgWip.Height));
            }

            switch (CbOperation.SelectedIndex)
            {
                case 0:
                    CvInvoke.Add(_imgWip, img2, _imgWip);
                    break;
                case 1:
                    CvInvoke.AddWeighted(_imgWip,double.Parse(TbAlpha.Text),img2, double.Parse(TbBeta.Text), double.Parse(TbGamma.Text), _imgWip);
                    break;
                case 2:
                    CvInvoke.BitwiseNot(_imgWip,_imgWip);
                    break;
                case 3:
                    CvInvoke.BitwiseAnd(_imgWip,img2,_imgWip);
                    break;
                case 4:
                    CvInvoke.BitwiseOr(_imgWip, img2, _imgWip);
                    break;
                case 5:
                    CvInvoke.BitwiseXor(_imgWip, img2, _imgWip);
                    break;
                default:
                    return;
            }
            ImgResult.Source = _imgWip.ToBitmap().ToImageSource();
        }

        private void CbImages_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CbImages.SelectedIndex == -1) return;
            _otherImage = _images[CbImages.SelectedIndex];
            ImgInpB.Source = _otherImage.ToImageSource();
        }

        private void CbOperation_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbOperation.SelectedIndex == 1)
            {
                SpBlend.Visibility = Visibility.Visible;
            }
            else
            {
                SpBlend.Visibility = Visibility.Collapsed;
            }
        }
    }
}
