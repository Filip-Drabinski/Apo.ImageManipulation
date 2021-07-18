using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Apo.Core;

namespace Apo.Gui.Models
{
    public class ImageModel : INotifyPropertyChanged
    {
        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        public string Name
        {
            get => $"{(IsSaved ? "" : "*")} {_name}";
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool IsSaved
        {
            get => _isSaved;
            set
            {
                _isSaved = value;
                OnPropertyChanged(nameof(IsSaved));
                OnPropertyChanged(nameof(Name));
            }
        }

        public string FullPath
        {
            get => _fullPath;
            set
            {
                _fullPath = value;
                OnPropertyChanged(nameof(FullPath));
            }
        }

        public ImageSource ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        private string _type;

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public ImageType ImageType
        {
            get => _imageType;
            set
            {
                _imageType = value;
                Type = value.ToString();
                _imageDataGreyVisibility = Visibility.Collapsed;
                _imageDataRedVisibility = Visibility.Collapsed;
                _imageDataGreenVisibility = Visibility.Collapsed;
                _imageDataBlueVisibility = Visibility.Collapsed;
                _imageDataAlphaVisibility = Visibility.Collapsed;
                if (value == ImageType.Grayscale)
                {
                    _imageDataGreyVisibility = Visibility.Visible;
                }
                else if (value == ImageType.Bgr || value == ImageType.Bgra)
                {
                    _imageDataRedVisibility = Visibility.Visible;
                    _imageDataGreenVisibility = Visibility.Visible;
                    _imageDataBlueVisibility = Visibility.Visible;
                }

                if (value == ImageType.Bgra) _imageDataAlphaVisibility = Visibility.Visible;
                OnPropertyChanged(nameof(ImageType));
                OnPropertyChanged(nameof(ImageDataGreyVisibility));
                OnPropertyChanged(nameof(ImageDataRedVisibility));
                OnPropertyChanged(nameof(ImageDataGreenVisibility));
                OnPropertyChanged(nameof(ImageDataBlueVisibility));
                OnPropertyChanged(nameof(ImageDataAlphaVisibility));
            }
        }

        public Visibility ImageDataGreyVisibility
        {
            get => _imageDataGreyVisibility;
            set
            {
                _imageDataGreyVisibility = value;
                OnPropertyChanged(nameof(ImageDataGreyVisibility));
            }
        }

        public Visibility ImageDataRedVisibility
        {
            get => _imageDataRedVisibility;
            set
            {
                _imageDataRedVisibility = value;
                OnPropertyChanged(nameof(ImageDataRedVisibility));
            }
        }

        public Visibility ImageDataGreenVisibility
        {
            get => _imageDataGreenVisibility;
            set
            {
                _imageDataGreenVisibility = value;
                OnPropertyChanged(nameof(ImageDataGreenVisibility));
            }
        }

        public Visibility ImageDataBlueVisibility
        {
            get => _imageDataBlueVisibility;
            set
            {
                _imageDataBlueVisibility = value;
                OnPropertyChanged(nameof(ImageDataBlueVisibility));
            }
        }

        public Visibility ImageDataAlphaVisibility
        {
            get => _imageDataAlphaVisibility;
            set
            {
                _imageDataAlphaVisibility = value;
                OnPropertyChanged(nameof(ImageDataAlphaVisibility));
            }
        }


        public Visibility GridVisibility =>
            _imageVisibility != Visibility.Visible ? Visibility.Visible : Visibility.Hidden;

        public Visibility ImageVisibility
        {
            get => _imageVisibility;
            set
            {
                _imageVisibility = value;
                OnPropertyChanged(nameof(ImageVisibility));
                OnPropertyChanged(nameof(GridVisibility));
            }
        }

        private int _width;
        private int _height;
        private bool _isSaved;
        private string _name;
        private string _fullPath;
        private ImageSource _imageSource;
        private Visibility _imageVisibility = Visibility.Visible;
        private Visibility _imageDataGreyVisibility;
        private Visibility _imageDataRedVisibility;
        private Visibility _imageDataGreenVisibility;
        private Visibility _imageDataBlueVisibility;
        private Visibility _imageDataAlphaVisibility;
        private ImageType _imageType;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}