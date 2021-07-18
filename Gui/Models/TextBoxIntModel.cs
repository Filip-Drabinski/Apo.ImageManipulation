using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Apo.Gui.Models
{
    public class TextBoxIntModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Func<int, bool> ValueValidate;
        public event Action<int, int> ValueChanged;
        public event Action<bool,bool> IsValidChanged;

        private int _value = 0;
        private string _valueText = "0";
        private SolidColorBrush _valueColor = Brushes.Black;
        private bool _isValid = true;


        public int Value
        {
            get => _value;
            set
            {
                ValueChanged?.Invoke(_value, value);
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public SolidColorBrush ValueColor
        {
            get => _valueColor;
            set
            {
                _valueColor = value;
                OnPropertyChanged(nameof(ValueColor));
            }
        }

        public string ValueText
        {
            get => _valueText;
            set
            {
                _valueText = value;
                if (int.TryParse(value, out var tmp) && (ValueValidate?.Invoke(tmp) ?? true))
                {
                    ValueColor = Brushes.Black;
                    Value = tmp;
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                    ValueColor = Brushes.Red;
                }

                OnPropertyChanged(nameof(ValueText));
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                IsValidChanged?.Invoke(_isValid, value);
                _isValid = value;
                OnPropertyChanged(nameof(IsValid));
            }
        }

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}