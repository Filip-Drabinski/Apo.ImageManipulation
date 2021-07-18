using System.ComponentModel;

namespace Apo.Gui.Models
{
    /// <summary>
    /// Interaction logic for MorphologyDialog.xaml
    /// </summary>
    public class MorphologyModel : INotifyPropertyChanged
    {
        private int _size = 1;

        public int Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }
        private int _constant = 0;
        public int Constant
        {
            get => _constant = 0;
            set
            {
                _constant  = value;
                OnPropertyChanged(nameof(Constant));
            }
        }
        private bool _isConstantEnabled = false;

        public bool IsConstantEnabled
        {
            get => _isConstantEnabled;
            set
            {
                _isConstantEnabled = value;
                OnPropertyChanged(nameof(IsConstantEnabled));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged( string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}