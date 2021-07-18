using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Apo.Core;

namespace Apo.Gui.Models
{
    public class ChartWindowModel : INotifyPropertyChanged
    {
        private int _selectedIndex;
        private Visibility _bcGreyVisibility = Visibility.Collapsed;
        private Visibility _bcRedVisibility = Visibility.Collapsed;
        private Visibility _bcGreenVisibility = Visibility.Collapsed;
        private Visibility _bcBlueVisibility = Visibility.Collapsed;
        private Visibility _bcAlphaVisibility = Visibility.Collapsed;
        private ImageType _imageType;
        private Visibility _cbiRedVisibility;
        private Visibility _cbiGreenVisibility;
        private Visibility _cbiBlueVisibility;
        private Visibility _cbiAlphaVisibility;
        private Visibility _cbiAllVisibility;
        private ScrollBarVisibility _horizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        private ScrollBarVisibility _verticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        private double _zoomX = 1;
        private double _zoomY = 1;
        private double _scaleY = 1;

        public Visibility BcGreyVisibility
        {
            get => _bcGreyVisibility;
            set
            {
                _bcGreyVisibility = value;
                OnPropertyChanged(nameof(BcGreyVisibility));
            }
        }

        public Visibility BcRedVisibility
        {
            get => _bcRedVisibility;
            set
            {
                _bcRedVisibility = value;
                OnPropertyChanged(nameof(BcRedVisibility));
            }
        }

        public Visibility BcGreenVisibility
        {
            get => _bcGreenVisibility;
            set
            {
                _bcGreenVisibility = value;
                OnPropertyChanged(nameof(BcGreenVisibility));
            }
        }

        public Visibility BcBlueVisibility
        {
            get => _bcBlueVisibility;
            set
            {
                _bcBlueVisibility = value;
                OnPropertyChanged(nameof(BcBlueVisibility));
            }
        }

        public Visibility BcAlphaVisibility
        {
            get => _bcAlphaVisibility;
            set
            {
                _bcAlphaVisibility = value;
                OnPropertyChanged(nameof(BcAlphaVisibility));
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                BcGreyVisibility = Visibility.Collapsed;
                BcRedVisibility = Visibility.Collapsed;
                BcGreenVisibility = Visibility.Collapsed;
                BcBlueVisibility = Visibility.Collapsed;
                BcAlphaVisibility = Visibility.Collapsed;
                switch (value)
                {
                    case 0: //Grey
                        BcGreyVisibility = Visibility.Visible;
                        break;
                    case 1: //Red
                        BcRedVisibility = Visibility.Visible;
                        break;
                    case 2: //Green
                        BcGreenVisibility = Visibility.Visible;
                        break;
                    case 3: //Blue
                        BcBlueVisibility = Visibility.Visible;
                        break;
                    case 4: //Alpha
                        BcAlphaVisibility = Visibility.Visible;
                        break;
                    /*case 5://All
                        break;*/
                }
            }
        }
        /*
        public ImageType ImageType
        {
            get => _imageType;
            set
            {
                _imageType = value;
                OnPropertyChanged(nameof(ImageType));
                CbiGreyVisibility = Visibility.Collapsed;
                CbiRedVisibility = Visibility.Collapsed;
                CbiGreenVisibility = Visibility.Collapsed;
                CbiBlueVisibility = Visibility.Collapsed;
                CbiAlphaVisibility = Visibility.Collapsed;
                CbiAllVisibility = Visibility.Collapsed;

                if (value == ImageType.Greyscale)
                {
                    SelectedIndex = 0;
                    CbiGreyVisibility = Visibility.Visible;
                }
                else if (value == ImageType.Rgb || value == ImageType.Rgba)
                {
                    SelectedIndex = 1;
                    CbiRedVisibility = Visibility.Visible;
                    CbiGreenVisibility = Visibility.Visible;
                    CbiBlueVisibility = Visibility.Visible;
                    CbiAllVisibility = Visibility.Visible;
                    if (value == ImageType.Rgba) _cbiAlphaVisibility = Visibility.Visible;
                }
            }
        }
        */

        public ImageType ImageType
        {
            get => _imageType;
            set
            {
                _imageType = value;
                OnPropertyChanged(nameof(ImageType));
                CbiGreyVisibility = Visibility.Collapsed;
                CbiRedVisibility = Visibility.Collapsed;
                CbiGreenVisibility = Visibility.Collapsed;
                CbiBlueVisibility = Visibility.Collapsed;
                CbiAlphaVisibility = Visibility.Collapsed;
                CbiAllVisibility = Visibility.Collapsed;

                if (value == ImageType.Grayscale)
                {
                    SelectedIndex = 0;
                    CbiGreyVisibility = Visibility.Visible;
                }
                else if (value == ImageType.Bgr || value == ImageType.Bgra)
                {
                    SelectedIndex = 1;
                    CbiRedVisibility = Visibility.Visible;
                    CbiGreenVisibility = Visibility.Visible;
                    CbiBlueVisibility = Visibility.Visible;
                    CbiAllVisibility = Visibility.Visible;
                    if (value == ImageType.Bgra) _cbiAlphaVisibility = Visibility.Visible;
                }
            }
        }
        private Visibility _cbiGreyVisibility;

        public Visibility CbiGreyVisibility
        {
            get => _cbiGreyVisibility;
            set
            {
                _cbiGreyVisibility = value;
                OnPropertyChanged(nameof(CbiGreyVisibility));
            }
        }

        public Visibility CbiRedVisibility
        {
            get => _cbiRedVisibility;
            set
            {
                _cbiRedVisibility = value;
                OnPropertyChanged(nameof(CbiRedVisibility));
            }
        }

        public Visibility CbiGreenVisibility
        {
            get => _cbiGreenVisibility;
            set
            {
                _cbiGreenVisibility = value;
                OnPropertyChanged(nameof(CbiGreenVisibility));
            }
        }

        public Visibility CbiBlueVisibility
        {
            get => _cbiBlueVisibility;
            set
            {
                _cbiBlueVisibility = value;
                OnPropertyChanged(nameof(CbiBlueVisibility));
            }
        }

        public Visibility CbiAlphaVisibility
        {
            get => _cbiAlphaVisibility;
            set
            {
                _cbiAlphaVisibility = value;
                OnPropertyChanged(nameof(CbiAlphaVisibility));
            }
        }

        public Visibility CbiAllVisibility
        {
            get => _cbiAllVisibility;
            set
            {
                _cbiAllVisibility = value;
                OnPropertyChanged(nameof(CbiAllVisibility));
            }
        }


        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get => _horizontalScrollBarVisibility;
            set
            {
                _horizontalScrollBarVisibility = value;
                OnPropertyChanged(nameof(HorizontalScrollBarVisibility));
            }
        }

        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get => _verticalScrollBarVisibility;
            set
            {
                _verticalScrollBarVisibility = value;
                OnPropertyChanged(nameof(VerticalScrollBarVisibility));
            }
        }

        public double ZoomX
        {
            get => _zoomX;
            set
            {
                _zoomX = value;
                OnPropertyChanged(nameof(ZoomX));
                OnPropertyChanged(nameof(ZoomXStr));
            }
        }

        public double ZoomY
        {
            get => _zoomY;
            set
            {
                _zoomY = value;
                OnPropertyChanged(nameof(ZoomY));
                OnPropertyChanged(nameof(ZoomYStr));
            }
        }

        public string ZoomXStr => $"{ZoomX:0.0}";

        public string ZoomYStr => $"{ZoomY:0.0}";
        public string ScaleYStr => $"{Math.Round(ScaleY)}";


        public double ScaleY
        {
            get => _scaleY;
            set
            {
                _scaleY = value;
                OnPropertyChanged(nameof(ScaleY));
                OnPropertyChanged(nameof(ScaleYStr));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}