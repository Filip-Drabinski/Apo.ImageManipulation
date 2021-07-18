using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Apo.Core;
using Apo.Gui.Dialogs;
using Apo.Gui.Models;

namespace Apo.Gui.UserControls
{
    /// <summary>
    ///     Interaction logic for MenuUc.xaml
    /// </summary>
    public partial class MenuUc : UserControl
    {
        public event Action Open;
        public event Action OpenInNewWindow;
        public event Action Duplicate;
        public event Action Save;
        public event Action SaveAs;
        public event Action Close;
        public event Action Exit;
        public event Action Undo;
        public event Action Redo;
        public event Action HistogramShow;
        public event Action HistogramStretch;
        public event Action HistogramEqualize;
        public event Action IntensityProfileShow;
        public event Action<BitmapScalingMode> Scaling;
        public event Action Negation;
        public event Action Thresholding;
        public event Action Posterize;
        public event Action Blur;
        public event Action GaussianBlur;
        public event Action EdgeDetectionLaplacian;
        public event Action EdgeDetectionSobel;
        public event Action EdgeDetectionCanny;
        public event Action DirectionalEdgeDetection;
        public event Action LinearSharpen;
        public event Action CustomFilter;
        public event Action MedianBlur;
        public event Action TwoImages;
        public event Action Morphology;
        public event Action Skeletonization;
        public event Action<ImageType> ImageTypeChanged;
        public event Action ManualThresholdingSegmentation;
        public event Action AdaptiveThresholdingSegmentation;
        public event Action OtsuSegmentation;
        public event Action WatershedSegmentation;
        public event Action FindContours;

        public bool IsImageLoaded
        {
            get => _isImageLoaded;
            set
            {
                _isImageLoaded = value;
                MiEdit.IsEnabled = value;
                MiView.IsEnabled = value;
                MiDuplicate.IsEnabled = value;
                MiTools.IsEnabled = value;
                MiCloseImage.IsEnabled = value;
                MiSaveAs.IsEnabled = value;
                if (!value) MiSave.IsEnabled = false;
            }
        }

        private ImageType _colorMode = ImageType.Unknown;
        public ImageType ColorMode
        {
            get=>_colorMode;
            set
            {
                _colorMode = value;
                MiThresholdingShow.IsEnabled = false;
                MiPosterizeShow.IsEnabled = false;
                MiEdgeDetectionShow.IsEnabled = false;
                MiBlurShow.IsEnabled = false;
                MiLinearSharpenShow.IsEnabled = false;
                MiTwoImagesShow.IsEnabled = false;
                MiCustomShow.IsEnabled = false;
                MiTwoStepCustomShow.IsEnabled = false;
                MiTwoImagesShow.IsEnabled = false;
                MiMorphologyShow.IsEnabled = false;
                MiSkeletonizationShow.IsEnabled = false;
                MiFilters.IsEnabled = false;
                MiAdaptiveThresholding.IsEnabled = false;
                MiManualThresholding.IsEnabled = false;
                MiOtsuSegmentation.IsEnabled = false;
                switch (value)
                {
                    case ImageType.Grayscale:
                        MiGrayscale.IsChecked = true;
                        MiThresholdingShow.IsEnabled = true;
                        MiPosterizeShow.IsEnabled = true;
                        MiEdgeDetectionShow.IsEnabled = true;
                        MiBlurShow.IsEnabled = true;
                        MiLinearSharpenShow.IsEnabled = true;
                        MiTwoImagesShow.IsEnabled = true;
                        MiCustomShow.IsEnabled = true;
                        MiTwoStepCustomShow.IsEnabled = true;
                        MiTwoImagesShow.IsEnabled = true;
                        MiMorphologyShow.IsEnabled = true;
                        MiSkeletonizationShow.IsEnabled = true;
                        MiFilters.IsEnabled = true;
                        MiAdaptiveThresholding.IsEnabled = true;
                        MiManualThresholding.IsEnabled = true;
                        MiOtsuSegmentation.IsEnabled = true;

                        break;
                    case ImageType.Bgr:
                        MiBgr.IsChecked = true;
                        break;
                    case ImageType.Bgra:
                        MiBgra.IsChecked = true;
                        break;
                    case ImageType.Unknown:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }

        public bool IsImageSaved
        {
            get => _isImageSaved;
            set
            {
                _isImageSaved = value;
                MiSave.IsEnabled = !value;
            }
        }

        private bool _isImageLoaded;
        private bool _isImageSaved;
        private readonly ScalingMenuItemModel _scalingModel;


        public MenuUc()
        {
            _scalingModel = new ScalingMenuItemModel
            {
                IsFantChecked = false,
                IsHighQualityChecked = true,
                IsLinearChecked = false,
                IsLowQualityChecked = false,
                IsNearestNeighborChecked = false
            };
            InitializeComponent();
            ScalingMenuItem.DataContext = _scalingModel;
            IsImageLoaded = false;
        }


        private void Open_OnClick(object sender, RoutedEventArgs e)
        {
            Open?.Invoke();
        }

        private void OpenInNewWindow_OnClick(object sender, RoutedEventArgs e)
        {
            OpenInNewWindow?.Invoke();
        }

        private void CloseImage_OnClick(object sender, RoutedEventArgs e)
        {
            Close?.Invoke();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Exit?.Invoke();
        }

        private void Undo_OnClick(object sender, RoutedEventArgs e)
        {
            Undo?.Invoke();
        }

        private void Redo_OnClick(object sender, RoutedEventArgs e)
        {
            Redo?.Invoke();
        }

        private void Duplicate_OnClick(object sender, RoutedEventArgs e)
        {
            Duplicate?.Invoke();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            Save?.Invoke();
        }

        private void SaveAs_OnClick(object sender, RoutedEventArgs e)
        {
            SaveAs?.Invoke();
        }

        // private void ScalingFant_OnClick(object sender, RoutedEventArgs e)
        // {
        //     InvokeScaling(BitmapScalingMode.Fant);
        // }

        private void ScalingHighQuality_OnClick(object sender, RoutedEventArgs e)
        {
            InvokeScaling(BitmapScalingMode.HighQuality);
        }

        // private void ScalingLinear_OnClick(object sender, RoutedEventArgs e)
        // {
        //     InvokeScaling(BitmapScalingMode.Linear);
        // }

        private void ScalingLowQuality_OnClick(object sender, RoutedEventArgs e)
        {
            InvokeScaling(BitmapScalingMode.LowQuality);
        }

        private void ScalingNearestNeighbor_OnClick(object sender, RoutedEventArgs e)
        {
            InvokeScaling(BitmapScalingMode.NearestNeighbor);
        }

        private void InvokeScaling(BitmapScalingMode mode)
        {
            Scaling?.Invoke(mode);
            _scalingModel.IsFantChecked = false;
            _scalingModel.IsHighQualityChecked = false;
            _scalingModel.IsLinearChecked = false;
            _scalingModel.IsLowQualityChecked = false;
            _scalingModel.IsNearestNeighborChecked = false;
            if (mode == BitmapScalingMode.LowQuality) _scalingModel.IsLowQualityChecked = true;
            else if (mode == BitmapScalingMode.HighQuality) _scalingModel.IsHighQualityChecked = true;
            else if (mode == BitmapScalingMode.NearestNeighbor) _scalingModel.IsNearestNeighborChecked = true;
            //else if (mode == BitmapScalingMode.Fant) _scalingModel.IsFantChecked = true;// highQuality duplicate
            //else if (mode == BitmapScalingMode.Linear) _scalingModel.IsLinearChecked = true; //low quality duplicate
        }

        private void HistogramShow_OnClick(object sender, RoutedEventArgs e)
        {
            HistogramShow?.Invoke();
        }

        private void IntensityProfile_OnClick(object sender, RoutedEventArgs e)
        {
            IntensityProfileShow?.Invoke();
        }

        private void HistogramStretch_OnClick(object sender, RoutedEventArgs e)
        {
            HistogramStretch?.Invoke();
        }

        private void HistogramEqualize_OnClick(object sender, RoutedEventArgs e)
        {
            HistogramEqualize?.Invoke();
        }

        private void MiNegationShow_OnClick(object sender, RoutedEventArgs e)
        {
            Negation?.Invoke();
        }

        private void MiThresholdingShow_OnClick(object sender, RoutedEventArgs e)
        {
            Thresholding?.Invoke();
        }

        private void MiPosterizeShow_OnClick(object sender, RoutedEventArgs e)
        {
            Posterize?.Invoke();
        }

        private void MiBlurShow_OnClick(object sender, RoutedEventArgs e)
        {
            Blur?.Invoke();
        }

        private void MiGaussianBlurShow_OnClick(object sender, RoutedEventArgs e)
        {
            GaussianBlur?.Invoke();
        }


        private void MiLinearSharpenShow_OnClick(object sender, RoutedEventArgs e)
        {
            LinearSharpen?.Invoke();
        }


        private void MiCustomShow_OnClick(object sender, RoutedEventArgs e)
        {
            CustomFilter?.Invoke();
        }

        private void MiDirectionalEdgeDetectionShow_OnClick(object sender, RoutedEventArgs e)
        {
            DirectionalEdgeDetection?.Invoke();
        }

        private void MiEdgeDetectionLaplacianShow_OnClick(object sender, RoutedEventArgs e)
        {
            EdgeDetectionLaplacian?.Invoke();
        }

        private void MiEdgeDetectionSobelShow_OnClick(object sender, RoutedEventArgs e)
        {
            EdgeDetectionSobel?.Invoke();
        }

        private void MiEdgeDetectionCannyShow_OnClick(object sender, RoutedEventArgs e)
        {
            EdgeDetectionCanny?.Invoke();
        }

        private void MiMedianBlurShow_OnClick(object sender, RoutedEventArgs e)
        {
            MedianBlur?.Invoke();
        }

        private void MiTwoImages_OnClick(object sender, RoutedEventArgs e)
        {
            TwoImages?.Invoke();
        }

        private void MiMorphology_OnClick(object sender, RoutedEventArgs e)
        {
            Morphology?.Invoke();
        }

        private void MiSkeletonization_OnClick(object sender, RoutedEventArgs e)
        {
            Skeletonization?.Invoke();
        }

        private void MiTwoStepCustomShow_OnClick(object sender, RoutedEventArgs e)
        {
            TwoStepCustom?.Invoke();
        }

        public event Action TwoStepCustom;

        private void RadioColorModeBgra_OnClick(object sender, RoutedEventArgs e)
        {
            ImageTypeChanged?.Invoke(ImageType.Bgra);
        }
        private void RadioColorModeBgr_OnClick(object sender, RoutedEventArgs e)
        {
            ImageTypeChanged?.Invoke(ImageType.Bgr);
        }
        private void RadioColorModeGrayscale_OnClick(object sender, RoutedEventArgs e)
        {
            ImageTypeChanged?.Invoke(ImageType.Grayscale);
        }

        private void MiManualThresholding_OnClick(object sender, RoutedEventArgs e)
        {
            ManualThresholdingSegmentation?.Invoke();
        }

        private void MiAdaptiveThresholding_OnClick(object sender, RoutedEventArgs e)
        {
            AdaptiveThresholdingSegmentation?.Invoke();
        }

        private void MiOtsuSegmentation_OnClick(object sender, RoutedEventArgs e)
        {
            OtsuSegmentation?.Invoke();
        }

        private void MiWatershedSegmentation_OnClick(object sender, RoutedEventArgs e)
        {
            WatershedSegmentation?.Invoke();
        }

        private void MiFindContours_OnClick(object sender, RoutedEventArgs e)
        {
            FindContours?.Invoke();
        }

        private void MiAbout_OnClick(object sender, RoutedEventArgs e)
        {
            new AboutDialog().ShowDialog();
        }
    }
}