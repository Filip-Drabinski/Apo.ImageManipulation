using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Apo.Core;
using Apo.Core.ImageModifiers;
using Apo.Core.ImageModifiersCv;
using Apo.Gui.Dialogs;
using Apo.Gui.Models;
using Microsoft.Win32;

namespace Apo.Gui.Windows
{
    public partial class ImageWindow : Window
    {
        private int _duplicateCounter;
        private NotificationSystem _notificationSystem;
        private Guid _groupGuid;

        private ImageModel _model;
        private ApoImage _image;

        public ImageWindow()
        {
            InitializeComponent();
            InitializeDataContext();
            InitializeNotifications();
        }

        private void InitializeNotifications()
        {
            _notificationSystem = NotificationSystem.Instance;
            _groupGuid = _notificationSystem.CreateChannel(Notification);
        }


        private void ImageTypeChange(ImageType obj)
        {
            switch (obj)
            {
                case ImageType.Grayscale:
                    _image = _image.ToGrayscale();
                    break;
                case ImageType.Bgr:
                    _image = _image.ToBgr();
                    break;
                case ImageType.Bgra:
                    _image = _image.ToBgra();
                    break;
            }

            _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
            UpdateModel();
            UpdateLayout();
        }

        private void TwoStepCustom()
        {
            var dialog = new TwoStepCustomFilterDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                for (var x = 0; x < _image.Width; x++)
                for (var y = 0; y < _image.Height; y++)
                    _image.SetPixel(0, y, x, dialog.ImgWip.Pixels[y, x, 0]);
                _model.ImageSource = _image.ToImageSource();
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void Skeletonization()
        {
            var dialog = new SkeletonizationDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void Morphology()
        {
            var dialog = new MorphologyDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void TwoImages()
        {
            var dialog = new TwoImageDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void MedianBlur()
        {
            var dialog = new MedianBlurDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void EdgeDetectionCanny()
        {
            var dialog = new EdgeDetectionCannyDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void EdgeDetectionSobel()
        {
            var dialog = new EdgeDetectionSobelDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void EdgeDetectionLaplacian()
        {
            var dialog = new EdgeDetectionLaplacianDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void CustomFilter()
        {
            var dialog = new CustomFilterDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void LinearSharpen()
        {
            var dialog = new LinearSharpenDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void DirectionalEdgeDetection()
        {
            var dialog = new DirectionalEdgeDetectionDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }


        private void GaussianBlur()
        {
            var dialog = new GaussianBlurDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void Blur()
        {
            var dialog = new BlurDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void Negation()
        {
            var imd = new ImageModificationDialog();
            var imgCpy = _image.Clone();
            imgCpy.Modify(new ImageNegation());
            imd.Image.Source = imgCpy.ToImageSource();
            if (imd.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(imgCpy));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void Thresholding()
        {
            var thresholdingDialog = new ThresholdingDialog(_image);
            if (thresholdingDialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(thresholdingDialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void Posterize()
        {
            var pd = new PosterizeDialog(_image);
            if (pd.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(pd.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void HistogramEqualize()
        {
            var histogramEqualizeDialog = new HistogramEqualizeDialog(_image);
            if (histogramEqualizeDialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(histogramEqualizeDialog.ImageWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void HistogramStretch()
        {
            var histogramStretchDialog = new HistogramStretchDialog(_image);
            if (histogramStretchDialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(histogramStretchDialog.ImageWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void InitializeDataContext()
        {
            _model = new ImageModel();
            DataContext = _model;
            _model.Height = -1;
            _model.Width = -1;
            _model.IsSaved = true;
            _model.Name = "Empty";
        }

        private void OpenFile()
        {
            var ofd = new OpenFileDialog();
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;

            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                ofd.Filter = String.Format("{0}{1}{2} ({3})|{3}", ofd.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }

            ofd.Filter = String.Format("{0}{1}{2} ({3})|{3}", ofd.Filter, sep, "All Files", "*.*");

            ofd.DefaultExt = ".png"; // Default file extension 

            if (ofd.ShowDialog() == true) LoadImage(ofd.FileName);
        }

        private void OpenInNewWindow()
        {
            var ofd = new OpenFileDialog();
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;

            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                ofd.Filter = String.Format("{0}{1}{2} ({3})|{3}", ofd.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }

            ofd.Filter = String.Format("{0}{1}{2} ({3})|{3}", ofd.Filter, sep, "All Files", "*.*");

            ofd.DefaultExt = ".png"; // Default file extension 


            if (ofd.ShowDialog() == true)
            {
                var windowNew = new ImageWindow();
                windowNew.LoadImage(ofd.FileName);
                windowNew.Show();
            }
        }

        private void OpenDuplicate()
        {
            var windowNew = new ImageWindow();
            windowNew.LoadImage(_image.Duplicate(_duplicateCounter++));
            windowNew.Show();
        }

        private void LoadImage(string path)
        {
            LoadImage(ApoImage.FromFile(path));
        }

        private void LoadImage(ApoImage img)
        {
            _image = img;
            UpdateModel();
            _notificationSystem.Notify(_groupGuid, Message.ImageOpened);
        }

        private void Save()
        {
            Save(_image.Path);
        }

        private void Save(string path)
        {
            _image.SaveAs(path);
            MenuUc.IsImageSaved = true;
            _model.IsSaved = true;
            UpdateModel();
        }

        private void SaveAs()
        {
            var sfd = new SaveFileDialog();

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;

            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                sfd.Filter = String.Format("{0}{1}{2} ({3})|{3}", sfd.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }

            sfd.Filter = String.Format("{0}{1}{2} ({3})|{3}", sfd.Filter, sep, "All Files", "*.*");

            sfd.DefaultExt = ".png"; // Default file extension 


            if (sfd.ShowDialog() == true) Save(sfd.FileName);
        }

        private void CloseImage()
        {
            _image = null;
            _model.ImageSource = null;
            _model.FullPath = "";
            _model.Width = 0;
            _model.Height = 0;
            _model.Name = "Empty";
            MenuUc.IsImageLoaded = false;
            _notificationSystem.Notify(_groupGuid, Message.ImageClosed);
        }

        private void SetImageScaling(BitmapScalingMode mode)
        {
            RenderOptions.SetBitmapScalingMode(Img, mode);
        }

        private void OpenHistogramWindow()
        {
            new HistogramWindow(_groupGuid, () => _image).Show();
        }

        private void OpenIntensityProfileWindow()
        {
            new IntensityProfileWindow(_groupGuid, () => _image).Show();
        }
        List<ApoImage> _backups = new List<ApoImage>();
        private int _backupCounter = 0;
        private void UpdateModel(bool createBackup = true)
        {
            if (_image == null)
            {
                _model.Height = -1;
                _model.Width = -1;
                _model.IsSaved = false;
                _model.Name = "Empty";
                _model.ImageSource = null;
                MenuUc.IsImageLoaded = false;
            }
            else
            {
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                _model.Height = _image.Height;
                _model.Width = _image.Width;
                _model.IsSaved = _image.IsSaved;
                _model.Name = _image.Path.Split("\\").Last();
                _model.FullPath = _image.Path;
                _model.ImageSource = _image.ToImageSource();

                MenuUc.IsImageLoaded = true;
                MenuUc.ColorMode = _image.Type;
                _model.ImageType = _image.Type;
                MenuUc.IsImageSaved = _image.IsSaved;
                if (createBackup)
                {
                    while (_backups.Count> 0 &&_backupCounter != _backups.Count - 1)
                    {
                        _backups.RemoveAt(_backups.Count - 1);
                    }
                    _backups.Add(_image.Clone());
                    _backupCounter = _backups.Count - 1;
                }
                MenuUc.MiUndo.IsEnabled = _backupCounter != 0;
                MenuUc.MiRedo.IsEnabled = _backupCounter < _backups.Count - 1;
            }
        }

        private void Notification(Message message, params object[] args)
        {
            if (message == Message.CollectImages) (args.First() as List<ApoImage>)?.Add(_image);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _notificationSystem.RemoveChannel(_groupGuid);
            base.OnClosing(e);
        }

        private void Undo()
        {
            if (_backups.Count > 1 && _backupCounter > 0)
            {
                _backupCounter--;
                _image = _backups[_backupCounter];
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
            }
            UpdateModel(false);
        }

        private void Redo()
        {
            if (_backupCounter < _backups.Count -1)
            {
                _backupCounter++;
                _image = _backups[_backupCounter];
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
            }
            UpdateModel(false);
        }

        private void MenuUc_OnExit()
        {
            Close();
        }

        private void MenuUc_OnManualThresholdingSegmentation()
        {
            var dialog = new ThresholdingSegmentationDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void MenuUc_OnAdaptiveThresholdingSegmentation()
        {
            var dialog = new AdaptiveThresholdingDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void MenuUc_OnWaterShedSegmentation()
        {
            var dialog = new SegmentationWatershedDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }
        private void MenuUc_OnOtsuSegmentation()
        {
            var dialog = new OtsuSegmentationDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }

        private void MenuUc_OnFindContours()
        {
            var dialog = new FindContoursDialog(_image);
            if (dialog.ShowDialog() == true)
            {
                _image.Modify(new ImageCopyPixels(dialog.ImgWip));
                _notificationSystem.Notify(_groupGuid, Message.ImageValueChanged);
                UpdateModel();
            }
        }
    }
}