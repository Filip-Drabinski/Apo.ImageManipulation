using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Apo.Gui.Models
{
    public class SobelModel:INotifyPropertyChanged
    {
        private SolidColorBrush _colorKSize = Brushes.Black;
        private SolidColorBrush _colorScale = Brushes.Black;
        private SolidColorBrush _colorDelta = Brushes.Black;
        private bool _isOkEnabled;
        private double _scale = 1;
        private double _delta  = 0;
        private int _kSize = 3;
        private string _scaleStr = "1.0";
        private string _deltaStr = "0.0";
        private string _kSizeStr = "3";

        public bool IsOkEnabled
        {
            get => _isOkEnabled;
            set
            {
                _isOkEnabled = value;
                OnPropertyChanged(nameof(IsOkEnabled));
            }
        }

        public bool IsCalculateEnabled
        {
            get
            {
                if (!double.TryParse(_scaleStr, out var tmp0)) return false;
                if (!double.TryParse(_deltaStr, out var tmp1)) return false;
                if (!int.TryParse(_kSizeStr, out var tmp2)) return false;
                return true;
            }
        }

        public double Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                OnPropertyChanged(nameof(Scale));
            }
        }

        public double Delta
        {
            get => _delta;
            set
            {
                _delta = value;
                OnPropertyChanged(nameof(Delta));
            }
        }

        public int KSize
        {
            get => _kSize;
            set
            {
                _kSize = value;
                OnPropertyChanged(nameof(KSize));
            }
        }

        public string ScaleStr
        {
            get => _scaleStr;
            set
            {
                _scaleStr = value;
                if (double.TryParse(value, out var tmp) && tmp > 0)
                {
                    ColorScale = Brushes.Black;
                    _scale = tmp;
                }
                else
                {
                    ColorScale = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsCalculateEnabled));
                OnPropertyChanged(nameof(ScaleStr));
            }
        }

        public string DeltaStr
        {
            get => _deltaStr;
            set
            {
                _deltaStr = value;
                if (double.TryParse(value, out var tmp) && tmp > 0)
                {
                    ColorDelta = Brushes.Black;
                    _delta = tmp;
                }
                else
                {
                    ColorDelta = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsCalculateEnabled));
                OnPropertyChanged(nameof(DeltaStr));
            }
        }

        public string KSizeStr
        {
            get => _kSizeStr;
            set
            {
                _kSizeStr = value;
                if (int.TryParse(value, out var tmp) && tmp < 31 && tmp%2==1)
                {
                    ColorKSize = Brushes.Black;
                    _kSize = tmp;
                }
                else
                {
                    ColorKSize = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsCalculateEnabled));
                OnPropertyChanged(nameof(KSizeStr));
            }
        }

        public SolidColorBrush ColorKSize
        {
            get => _colorKSize;
            set
            {
                _colorKSize = value;
                OnPropertyChanged(nameof(ColorKSize));
            }
        }
        public SolidColorBrush ColorScale
        {
            get => _colorScale;
            set
            {
                _colorScale = value;
                OnPropertyChanged(nameof(ColorScale));
            }
        }
        public SolidColorBrush ColorDelta
        {
            get => _colorDelta;
            set
            {
                _colorDelta = value;
                OnPropertyChanged(nameof(ColorDelta));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}