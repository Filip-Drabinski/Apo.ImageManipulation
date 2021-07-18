using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Apo.Gui.Models
{
    internal class IntensityProfileModel : ChartWindowModel
    {
        private int _x0;
        private int _y0;
        private int _x1;
        private int _y1;
        private int _imageWidth;
        private int _imageHeight;
        private int _sliderX1Max;
        private int _sliderY1Max;
        private ImageSource _bitmap;
        private Visibility _settGridVisibility = Visibility.Collapsed;
        private Point _lineOriginPoint;
        private double _lineX0;
        private double _lineY0;
        private double _lineX1;
        private double _lineY1;

        public int X0
        {
            get => _x0;
            set
            {
                _x0 = value;
                OnPropertyChanged(nameof(X0));
                OnPropertyChanged(nameof(X0Str));
            }
        }

        public int Y0
        {
            get => _y0;
            set
            {
                _y0 = value;
                OnPropertyChanged(nameof(Y0));
                OnPropertyChanged(nameof(Y0Str));
            }
        }

        public int X1
        {
            get => _x1;
            set
            {
                _x1 = value;
                OnPropertyChanged(nameof(X1));
                OnPropertyChanged(nameof(X1Str));
            }
        }

        public int Y1
        {
            get => _y1;
            set
            {
                _y1 = value;
                OnPropertyChanged(nameof(Y1));
                OnPropertyChanged(nameof(Y1Str));
            }
        }

        public string X0Str => _x0.ToString();
        public string Y0Str => _y0.ToString();
        public string X1Str => _x1.ToString();
        public string Y1Str => _y1.ToString();

        public int ImageWidth
        {
            get => _imageWidth;
            set
            {
                _imageWidth = value;
                OnPropertyChanged(nameof(ImageWidth));
            }
        }

        public int ImageHeight
        {
            get => _imageHeight;
            set
            {
                _imageHeight = value;
                OnPropertyChanged(nameof(ImageHeight));
            }
        }

        public int SliderX1Max
        {
            get => _sliderX1Max;
            set
            {
                _sliderX1Max = value;
                OnPropertyChanged(nameof(SliderX1Max));
            }
        }

        public int SliderY1Max
        {
            get => _sliderY1Max;
            set
            {
                _sliderY1Max = value;
                OnPropertyChanged(nameof(SliderY1Max));
            }
        }

        public Point LineOriginPoint
        {
            get => _lineOriginPoint;
            set
            {
                _lineOriginPoint = value;
                OnPropertyChanged(nameof(LineOriginPoint));
            }
        }


        public double LineX0
        {
            get => _lineX0;
            set
            {
                _lineX0 = value;
                OnPropertyChanged(nameof(LineX0));
            }
        }

        public double LineY0
        {
            get => _lineY0;
            set
            {
                _lineY0 = value;
                OnPropertyChanged(nameof(LineY0));
            }
        }

        public double LineX1
        {
            get => _lineX1;
            set
            {
                _lineX1 = value;
                OnPropertyChanged(nameof(LineX1));
            }
        }

        public double LineY1
        {
            get => _lineY1;
            set
            {
                _lineY1 = value;
                OnPropertyChanged(nameof(LineY1));
            }
        }


        public ImageSource Bitmap
        {
            get => _bitmap;
            set
            {
                _bitmap = value;
                OnPropertyChanged(nameof(Bitmap));
            }
        }

        public Visibility SettGridVisibility
        {
            get => _settGridVisibility;
            set
            {
                _settGridVisibility = value;
                OnPropertyChanged(nameof(SettGridVisibility));
            }
        }

        private double _indicatorX;

        public double IndicatorX
        {
            get => _indicatorX;
            set
            {
                _indicatorX = value;
                OnPropertyChanged(nameof(IndicatorX));
            }
        }

        private double _indicatorY;

        public double IndicatorY
        {
            get => _indicatorY;
            set
            {
                _indicatorY = value;
                OnPropertyChanged(nameof(IndicatorY));
            }
        }
    }
}