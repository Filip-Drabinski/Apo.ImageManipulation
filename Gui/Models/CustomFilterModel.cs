using System.ComponentModel;
using System.Windows.Media;

namespace Apo.Gui.Models
{
    public class CustomFilterModel : INotifyPropertyChanged
    {
        private double _val00 = 1;
        private string _val00Str = "1.0";
        private SolidColorBrush _val00Brush = Brushes.Black;
        private double _val01 = 1;
        private string _val01Str = "1.0";
        private SolidColorBrush _val01Brush = Brushes.Black;
        private double _val02 = 1;
        private string _val02Str = "1.0";
        private SolidColorBrush _val02Brush = Brushes.Black;
        private double _val10 = 1;
        private string _val10Str = "1.0";
        private SolidColorBrush _val10Brush = Brushes.Black;
        private double _val11 = 1;
        private string _val11Str = "1.0";
        private SolidColorBrush _val11Brush = Brushes.Black;
        private double _val12 = 1;
        private string _val12Str = "1.0";
        private SolidColorBrush _val12Brush = Brushes.Black;
        private double _val20 = 1;
        private string _val20Str = "1.0";
        private SolidColorBrush _val20Brush = Brushes.Black;
        private double _val21 = 1;
        private string _val21Str = "1.0";
        private SolidColorBrush _val21Brush = Brushes.Black;
        private double _val22 = 1;
        private string _val22Str = "1.0";
        private SolidColorBrush _val22Brush = Brushes.Black;

        public double Val00
        {
            get => _val00;
            set
            {
                _val00 = value;
                OnPropertyChanged(nameof(Val00));
            }
        }


        public string Val00Str
        {
            get => _val00Str;
            set
            {
                _val00Str = value;
                if (double.TryParse(value, out var tmp))
                {
                    Val00Brush = Brushes.Black;
                    Val00 = tmp;
                }
                else
                {
                    Val00Brush = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsBtnCalculateActive));
                OnPropertyChanged(nameof(Val00Str));
            }
        }


        public SolidColorBrush Val00Brush
        {
            get => _val00Brush;
            set
            {
                _val00Brush = value;
                OnPropertyChanged(nameof(Val00Brush));
            }
        }

        public double Val01
        {
            get => _val01;
            set
            {
                _val01 = value;
                OnPropertyChanged(nameof(Val01));
            }
        }


        public string Val01Str
        {
            get => _val01Str;
            set
            {
                _val01Str = value;
                if (double.TryParse(value, out var tmp))
                {
                    Val01Brush = Brushes.Black;
                    Val01 = tmp;
                }
                else
                {
                    Val01Brush = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsBtnCalculateActive));
                OnPropertyChanged(nameof(Val01Str));
            }
        }


        public SolidColorBrush Val01Brush
        {
            get => _val01Brush;
            set
            {
                _val01Brush = value;
                OnPropertyChanged(nameof(Val01Brush));
            }
        }

        public double Val02
        {
            get => _val02;
            set
            {
                _val02 = value;
                OnPropertyChanged(nameof(Val02));
            }
        }


        public string Val02Str
        {
            get => _val02Str;
            set
            {
                _val02Str = value;
                if (double.TryParse(value, out var tmp))
                {
                    Val02Brush = Brushes.Black;
                    Val02 = tmp;
                }
                else
                {
                    Val02Brush = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsBtnCalculateActive));
                OnPropertyChanged(nameof(Val02Str));
            }
        }


        public SolidColorBrush Val02Brush
        {
            get => _val02Brush;
            set
            {
                _val02Brush = value;
                OnPropertyChanged(nameof(Val02Brush));
            }
        }


        public double Val10
        {
            get => _val10;
            set
            {
                _val10 = value;
                OnPropertyChanged(nameof(Val10));
            }
        }



        public string Val10Str
        {
            get => _val10Str;
            set
            {
                _val10Str = value;
                if (double.TryParse(value, out var tmp))
                {
                    Val10Brush = Brushes.Black;
                    Val00 = tmp;
                }
                else
                {
                    Val10Brush = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsBtnCalculateActive));
                OnPropertyChanged(nameof(Val00Str));
            }
        }

        public SolidColorBrush Val10Brush
        {
            get => _val10Brush;
            set
            {
                _val10Brush = value;
                OnPropertyChanged(nameof(Val10Brush));
            }
        }


        public double Val11
        {
            get => _val11;
            set
            {
                _val11 = value;
                OnPropertyChanged(nameof(Val11));
            }
        }


        public string Val11Str
        {
            get => _val11Str;
            set
            {
                _val11Str = value;
                if (double.TryParse(value, out var tmp))
                {
                    Val11Brush = Brushes.Black;
                    Val11 = tmp;
                }
                else
                {
                    Val11Brush = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsBtnCalculateActive));
                OnPropertyChanged(nameof(Val11Str));
            }
        }


        public SolidColorBrush Val11Brush
        {
            get => _val11Brush;
            set
            {
                _val11Brush = value;
                OnPropertyChanged(nameof(Val11Brush));
            }
        }


        public double Val12
        {
            get => _val12;
            set
            {
                _val12 = value;
                OnPropertyChanged(nameof(Val12));
            }
        }


        public string Val12Str
        {
            get => _val12Str;
            set
            {
                _val12Str = value;
                if (double.TryParse(value, out var tmp))
                {
                    Val12Brush = Brushes.Black;
                    Val12 = tmp;
                }
                else
                {
                    Val12Brush = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsBtnCalculateActive));
                OnPropertyChanged(nameof(Val12Str));
            }
        }


        public SolidColorBrush Val12Brush
        {
            get => _val12Brush;
            set
            {
                _val12Brush = value;
                OnPropertyChanged(nameof(Val12Brush));
            }
        }


        public double Val20
        {
            get => _val20;
            set
            {
                _val20 = value;
                OnPropertyChanged(nameof(Val20));
            }
        }


        public string Val20Str
        {
            get => _val20Str;
            set
            {
                _val20Str = value;
                if (double.TryParse(value, out var tmp))
                {
                    Val20Brush = Brushes.Black;
                    Val00 = tmp;
                }
                else
                {
                    Val20Brush = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsBtnCalculateActive));
                OnPropertyChanged(nameof(Val00Str));
            }
        }


        public SolidColorBrush Val20Brush
        {
            get => _val20Brush;
            set
            {
                _val20Brush = value;
                OnPropertyChanged(nameof(Val20Brush));
            }
        }

        public double Val21
        {
            get => _val21;
            set
            {
                _val21 = value;
                OnPropertyChanged(nameof(Val21));
            }
        }


        public string Val21Str
        {
            get => _val21Str;
            set
            {
                _val21Str = value;
                if (double.TryParse(value, out var tmp))
                {
                    Val21Brush = Brushes.Black;
                    Val21 = tmp;
                }
                else
                {
                    Val21Brush = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsBtnCalculateActive));
                OnPropertyChanged(nameof(Val21Str));
            }
        }

        public SolidColorBrush Val21Brush
        {
            get => _val21Brush;
            set
            {
                _val21Brush = value;
                OnPropertyChanged(nameof(Val21Brush));
            }
        }

        public double Val22
        {
            get => _val22;
            set
            {
                _val22 = value;
                OnPropertyChanged(nameof(Val22));
            }
        }

        public string Val22Str
        {
            get => _val22Str;
            set
            {
                _val22Str = value;
                if (double.TryParse(value, out var tmp))
                {
                    Val22Brush = Brushes.Black;
                    Val22 = tmp;
                }
                else
                {
                    Val22Brush = Brushes.Red;
                }

                OnPropertyChanged(nameof(IsBtnCalculateActive));
                OnPropertyChanged(nameof(Val22Str));
            }
        }

        public SolidColorBrush Val22Brush
        {
            get => _val22Brush;
            set
            {
                _val22Brush = value;
                OnPropertyChanged(nameof(Val22Brush));
            }
        }


        public bool IsBtnCalculateActive => double.TryParse(_val00Str, out _) &&
                                            double.TryParse(_val01Str, out _) &&
                                            double.TryParse(_val02Str, out _) &&
                                            double.TryParse(_val10Str, out _) &&
                                            double.TryParse(_val11Str, out _) &&
                                            double.TryParse(_val12Str, out _) &&
                                            double.TryParse(_val20Str, out _) &&
                                            double.TryParse(_val21Str, out _) &&
                                            double.TryParse(_val22Str, out _);


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}