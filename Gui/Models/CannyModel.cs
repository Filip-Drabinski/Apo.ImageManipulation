using System.ComponentModel;
using System.Windows.Media;

namespace Apo.Gui.Models
{
    class CannyModel:INotifyPropertyChanged
    {
        private bool _isOkEnabled;
        private double _threshold1 = 100;
        private double _threshold2 = 200;
        private string _threshold1Str = "100";
        private string _threshold2Str = "200";

        public double Threshold2
        {
            get => _threshold2;
            set
            {
                _threshold2 = value;
                OnPropertyChanged(nameof(Threshold2));
            }
        }

        public double Threshold1
        {
            get => _threshold1;
            set
            {
                _threshold1 = value;
                OnPropertyChanged(nameof(Threshold1));
            }
        }

        public string Threshold2Str
        {
            get => _threshold2Str;
            set
            {
                _threshold2Str = value;
                if (double.TryParse(value, out var tmp))
                {
                    ColorThreshold2 = Brushes.Black;
                    Threshold2 = tmp;
                }
                else
                {
                    ColorThreshold2 = Brushes.Red;
                }
                OnPropertyChanged(nameof(IsCalculateEnabled));
                OnPropertyChanged(nameof(Threshold2Str));
            }
        }

        public string Threshold1Str
        {
            get => _threshold1Str;
            set
            {
                _threshold1Str = value;
                if (double.TryParse(value, out var tmp))
                {
                    ColorThreshold1 = Brushes.Black;
                    Threshold1 = tmp;
                }
                else
                {
                    ColorThreshold1 = Brushes.Red;
                }
                OnPropertyChanged(nameof(IsCalculateEnabled));
                OnPropertyChanged(nameof(Threshold1Str));
            }
        }
        public bool IsOkEnabled
        {
            get => _isOkEnabled;
            set
            {
                _isOkEnabled = value;
                OnPropertyChanged(nameof(IsOkEnabled));
            }
        }

        private SolidColorBrush _colorThreshold1 = Brushes.Black;
        public SolidColorBrush ColorThreshold1
        {
            get => _colorThreshold1;
            set
            {
                _colorThreshold1 = value;
                OnPropertyChanged(nameof(ColorThreshold1));
            }
        }
        private SolidColorBrush _colorThreshold2 = Brushes.Black;
        public SolidColorBrush ColorThreshold2
        {
            get => _colorThreshold2;
            set
            {
                _colorThreshold2 = value;
                OnPropertyChanged(nameof(ColorThreshold2));
            }
        }
        public bool IsCalculateEnabled
        {
            get
            {
                if (!double.TryParse(_threshold1Str, out var tmp0)) return false;
                if (!double.TryParse(_threshold2Str, out var tmp1)) return false;
                return true;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}