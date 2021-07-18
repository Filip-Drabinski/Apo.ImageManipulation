using System.ComponentModel;
using System.Windows.Media;

namespace Apo.Gui.Models
{
    public class GaussianBlurDialogModel : INotifyPropertyChanged
    {
        private int _width = 5;
        private int _height = 5;
        private string _widthString = "5";
        private string _heightString = "5";
        private double _sigmaX;
        private double _sigmaY;
        private string _sigmaXStr = "0";
        private string _sigmaYStr = "0";
        private SolidColorBrush _colorWidth = Brushes.Black;
        private SolidColorBrush _colorHeight = Brushes.Black;
        private SolidColorBrush _colorSigmaX = Brushes.Black;
        private SolidColorBrush _colorSigmaY = Brushes.Black;
        private bool _isOkEnabled;

        public bool IsOkEnabled
        {
            get => _isOkEnabled;
            set
            {
                _isOkEnabled = value;
                OnPropertyChanged(nameof(IsOkEnabled));
            }
        }

        public SolidColorBrush ColorWidth
        {
            get => _colorWidth;
            set
            {
                _colorWidth = value;
                OnPropertyChanged(nameof(ColorWidth));
            }
        }

        public SolidColorBrush ColorHeight
        {
            get => _colorHeight;
            set
            {
                _colorHeight = value;
                OnPropertyChanged(nameof(ColorHeight));
            }
        }

        public SolidColorBrush ColorSigmaX
        {
            get => _colorSigmaX;
            set
            {
                _colorSigmaX = value;
                OnPropertyChanged(nameof(ColorSigmaX));
            }
        }

        public SolidColorBrush ColorSigmaY
        {
            get => _colorSigmaY;
            set
            {
                _colorSigmaY = value;
                OnPropertyChanged(nameof(ColorSigmaY));
            }
        }


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

        public string WidthString
        {
            get => _widthString;
            set
            {
                _widthString = value;
                if (int.TryParse(value, out var tmp) && tmp > 0 && tmp % 2 == 1)
                {
                    ColorWidth = Brushes.Black;
                    Width = tmp;
                }
                else
                {
                    ColorWidth = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsCalculateEnabled));
                OnPropertyChanged(nameof(WidthString));
            }
        }

        public string HeightString
        {
            get => _heightString;
            set
            {
                _heightString = value;
                if (int.TryParse(value, out var tmp) && tmp > 0 && tmp % 2 == 1)
                {
                    ColorHeight = Brushes.Black;
                    Height = tmp;
                }
                else
                {
                    ColorHeight = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsCalculateEnabled));
                OnPropertyChanged(nameof(HeightString));
            }
        }

        public bool IsCalculateEnabled
        {
            get
            {
                if (!int.TryParse(_widthString, out var tmp0)) return false;
                if (!int.TryParse(_heightString, out var tmp1)) return false;
                if (!int.TryParse(_sigmaXStr, out _)) return false;
                if (!int.TryParse(_sigmaYStr, out _)) return false;
                if (tmp0 <= 0 || tmp0 % 2 != 1) return false;
                if (tmp1 <= 0 || tmp1 % 2 != 1) return false;
                return true;
            }
        }


        public double SigmaX
        {
            get => _sigmaX;
            set
            {
                _sigmaX = value;
                OnPropertyChanged(nameof(SigmaX));
            }
        }

        public double SigmaY
        {
            get => _sigmaY;
            set
            {
                _sigmaY = value;
                OnPropertyChanged(nameof(SigmaY));
            }
        }


        public string SigmaXStr
        {
            get => _sigmaXStr;
            set
            {
                _sigmaXStr = value;
                if (int.TryParse(value, out var tmp))
                {
                    ColorSigmaX = Brushes.Black;
                    SigmaX = tmp;
                }
                else
                {
                    ColorSigmaX = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsCalculateEnabled));
                OnPropertyChanged(nameof(SigmaX));
                OnPropertyChanged(nameof(SigmaXStr));
            }
        }

        public string SigmaYStr
        {
            get => _sigmaYStr;
            set
            {
                _sigmaYStr = value;
                if (int.TryParse(value, out var tmp))
                {
                    ColorSigmaY = Brushes.Black;
                    SigmaY = tmp;
                }
                else
                {
                    ColorSigmaY = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsCalculateEnabled));
                OnPropertyChanged(nameof(SigmaY));
                OnPropertyChanged(nameof(SigmaYStr));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}