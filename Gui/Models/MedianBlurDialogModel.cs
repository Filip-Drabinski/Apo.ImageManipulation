using System.ComponentModel;
using System.Windows.Media;

namespace Apo.Gui.Models
{
    public class MedianBlurDialogModel : INotifyPropertyChanged
    {
        private int _size = 5;
        private string _sizeString = "5";
        private SolidColorBrush _colorSize = Brushes.Black;
        public SolidColorBrush ColorSize
        {
            get => _colorSize;
            set
            {
                _colorSize = value;
                OnPropertyChanged(nameof(ColorSize));
            }
        }
        public int Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        public string SizeString
        {
            get => _sizeString;
            set
            {
                _sizeString = value;
                if (int.TryParse(value, out var tmp) && tmp > 0)
                {
                    ColorSize = Brushes.Black;
                    Size = tmp;
                }
                else
                {
                    ColorSize = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsCalculateEnabled));
                OnPropertyChanged(nameof(SizeString));
            }
        }
        public bool IsCalculateEnabled
        {
            get
            {
                if (!int.TryParse(_sizeString, out var tmp0)) return false;
                if (tmp0 >= 1 && tmp0%2!=1) return false;
                return true;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}