using System.ComponentModel;
using System.Windows.Media;

namespace Apo.Gui.Models
{
    public class BlurDialogModel : INotifyPropertyChanged
    {
        private int _width = 5;
        private int _height = 5;
        private string _widthString = "5";
        private string _heightString = "5";
        private readonly string _constantString = "0";
        private SolidColorBrush _colorWidth = Brushes.Black;
        private SolidColorBrush _colorHeight = Brushes.Black;

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
                if (int.TryParse(value, out var tmp) && tmp > 0)
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
                if (int.TryParse(value, out var tmp) && tmp > 0)
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
                if (!int.TryParse(_constantString, out _)) return false;
                if (tmp0 <= 0) return false;
                return tmp1 > 0;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}