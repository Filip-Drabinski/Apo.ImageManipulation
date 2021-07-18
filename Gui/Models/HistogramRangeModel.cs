using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Apo.Gui.Models
{
    public class HistogramRangeModel : INotifyPropertyChanged
    {
        private int _sliderMinVal;

        public int SliderMinVal
        {
            get => _sliderMinVal;
            set
            {
                _sliderMinVal = value < _sliderMaxVal ? value : _sliderMaxVal - 1;
                OnPropertyChanged(nameof(SliderMinVal));
            }
        }

        private int _sliderMaxVal = 255;

        public int SliderMaxVal
        {
            get => _sliderMaxVal;
            set
            {
                _sliderMaxVal = value > _sliderMinVal ? value : _sliderMinVal + 1;
                OnPropertyChanged(nameof(SliderMaxVal));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ThresholdingModel : INotifyPropertyChanged
    {
        private int _sliderMinVal;

        public int SliderMinVal
        {
            get => _sliderMinVal;
            set
            {
                _sliderMinVal = value < _sliderMaxVal ? value : _sliderMaxVal;
                OnPropertyChanged(nameof(SliderMinVal));
            }
        }

        private int _sliderMaxVal = 255;

        public int SliderMaxVal
        {
            get => _sliderMaxVal;
            set
            {
                _sliderMaxVal = value > _sliderMinVal ? value : _sliderMinVal;
                OnPropertyChanged(nameof(SliderMaxVal));
            }
        }

        private int _topVal;

        public int TopVal
        {
            get => _topVal;
            set
            {
                _topVal = value;
                OnPropertyChanged(nameof(TopVal));
            }
        }

        private int _bottomVal;

        public int BottomVal
        {
            get => _bottomVal;
            set
            {
                _bottomVal = value;
                OnPropertyChanged(nameof(BottomVal));
            }
        }

        private ImageSource _bitmap;

        public ImageSource Bitmap
        {
            get => _bitmap;
            set
            {
                _bitmap = value;
                OnPropertyChanged(nameof(Bitmap));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}