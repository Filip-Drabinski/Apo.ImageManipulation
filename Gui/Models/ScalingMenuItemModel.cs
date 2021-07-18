using System.ComponentModel;

namespace Apo.Gui.Models
{
    public class ScalingMenuItemModel : INotifyPropertyChanged
    {
        private bool _isFantCheched = true;
        private bool _isHighQualityChecked;
        private bool _isLinearChecked;
        private bool _isLowQualityChecked;
        private bool _isNearestNeighborChecked;

        public bool IsFantChecked
        {
            get => _isFantCheched;
            set
            {
                _isFantCheched = value;
                OnPropertyChanged(nameof(IsFantChecked));
            }
        }

        public bool IsHighQualityChecked
        {
            get => _isHighQualityChecked;
            set
            {
                _isHighQualityChecked = value;
                OnPropertyChanged(nameof(IsHighQualityChecked));
            }
        }

        public bool IsLinearChecked
        {
            get => _isLinearChecked;
            set
            {
                _isLinearChecked = value;
                OnPropertyChanged(nameof(IsLinearChecked));
            }
        }

        public bool IsLowQualityChecked
        {
            get => _isLowQualityChecked;
            set
            {
                _isLowQualityChecked = value;
                OnPropertyChanged(nameof(IsLowQualityChecked));
            }
        }

        public bool IsNearestNeighborChecked
        {
            get => _isNearestNeighborChecked;
            set
            {
                _isNearestNeighborChecked = value;
                OnPropertyChanged(nameof(IsNearestNeighborChecked));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}