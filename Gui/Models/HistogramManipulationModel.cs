using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Apo.Gui.Models
{
    public class HistogramManipulationModel : INotifyPropertyChanged
    {
        private ImageSource _imageSourceBefore;

        public ImageSource ImageSourceBefore
        {
            get => _imageSourceBefore;
            set
            {
                _imageSourceBefore = value;
                OnPropertyChanged(nameof(ImageSourceBefore));
            }
        }

        private ImageSource _imageSourceAfter;

        public ImageSource ImageSourceAfter
        {
            get => _imageSourceAfter;
            set
            {
                _imageSourceAfter = value;
                OnPropertyChanged(nameof(ImageSourceAfter));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}