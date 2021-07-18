using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Apo.Gui.UserControls
{
    /// <summary>
    ///     Interaction logic for BarChart.xaml
    /// </summary>
    public partial class BarChart : UserControl
    {
        private int[] _data;
        private int _dataMax;
        private Rectangle[] _rectangles;
        private double _scaleY = 1;
        public double ScaleY { 
            get=>_scaleY;
            set
            {
                _scaleY = value;
                MaxYValue.Content = $"{_dataMax / _scaleY:0}";
                Redraw();
            }
        }
        public BarChart()
        {
            InitializeComponent();
            IsVisibleChanged += VisibilityChanged;
        }

        private void VisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible) Redraw();
        }

        public void SetData(int[] data, Color color, string[] labels = null)
        {
            _data = data;
            _dataMax = data.Max();
            MaxYValue.Content = _dataMax;
            MaxXValue.Content = data.Length;
            var mult = _dataMax != 0 ? (DataRow.ActualHeight / _dataMax) * ScaleY : 0;
            if (_rectangles != null)
            {
                DataGrid.Children.Clear();
                DataGrid.ColumnDefinitions.Clear();
            }

            _rectangles = new Rectangle[data.Length];
            for (var i = 0; i < data.Length; i++)
            {
                _rectangles[i] = new Rectangle
                {
                    Height = _data[i] * mult,
                    Fill = new SolidColorBrush(color),
                    ToolTip = labels != null ? labels[i] : i + ": " + _data[i],
                    VerticalAlignment = VerticalAlignment.Bottom
                };
                DataGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)});
                DataGrid.Children.Add(_rectangles[i]);
                Grid.SetColumn(_rectangles[i], i);
            }
        }
        public void SetData(byte[] data, Color color, string[] labels = null)
        {
            _data = new int[data.Length];
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = data[i];
            }
            _dataMax = data.Max();
            MaxYValue.Content = _dataMax;
            MaxXValue.Content = data.Length;
            var mult = _dataMax != 0 ? (DataRow.ActualHeight / _dataMax) * ScaleY : 0;
            if (_rectangles != null)
            {
                DataGrid.Children.Clear();
                DataGrid.ColumnDefinitions.Clear();
            }

            _rectangles = new Rectangle[data.Length];
            for (var i = 0; i < data.Length; i++)
            {
                _rectangles[i] = new Rectangle
                {
                    Height = _data[i] * mult,
                    Fill = new SolidColorBrush(color),
                    ToolTip = labels != null ? labels[i] : i + ": " + _data[i],
                    VerticalAlignment = VerticalAlignment.Bottom
                };
                DataGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)});
                DataGrid.Children.Add(_rectangles[i]);
                Grid.SetColumn(_rectangles[i], i);
            }
        }

        private void BarChart_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Redraw();
        }

        public void Redraw()
        {
            if (_rectangles == null) return;
            var mult = _dataMax != 0 ? (DataRow.ActualHeight / _dataMax)* ScaleY : 0;
            for (var index = 0; index < _rectangles.Length; index++) _rectangles[index].Height = _data[index] * mult;
        }
    }
}