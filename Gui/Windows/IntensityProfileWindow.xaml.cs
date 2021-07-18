using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Apo.Core;

namespace Apo.Gui.Windows
{
    /// <summary>
    ///     Interaction logic for IntensityProfileWindow.xaml
    /// </summary>
    public partial class IntensityProfileWindow : Window
    {
        private Guid _guid;
        private readonly Func<ApoImage> _getImage;
        private NotificationSystem _notificationSystem;
        private readonly Color _cGrey = Color.FromRgb(0, 0, 0);
        private readonly Color _cRed = Color.FromRgb(255, 0, 0);
        private readonly Color _cGreen = Color.FromRgb(0, 255, 0);
        private readonly Color _cBlue = Color.FromRgb(0, 0, 255);
        private readonly Color _cAlpha = Color.FromRgb(200, 200, 200);

        public IntensityProfileWindow()
        {
            InitializeComponent();
        }

        public IntensityProfileWindow(Guid guid, [NotNull] Func<ApoImage> getImage)
        {
            InitializeComponent();
            InitializeNotifications(guid);
            _getImage = getImage;
            BcGrey.Title.Text = "Intensity profile grey channel";
            BcRed.Title.Text = "Intensity profile red  channel";
            BcGreen.Title.Text = "Intensity profile green channel";
            BcBlue.Title.Text = "Intensity profile blue channel";
            BcAlpha.Title.Text = "Intensity profile alpha channel";
            var img = _getImage();
            Model.ImageType = img.Type;
            Model.X0 = (int) (img.Width * 0.4);
            Model.X1 = (int) (img.Width * 0.6);
            Model.Y0 = (int) (img.Height * 0.4);
            Model.Y1 = (int) (img.Height * 0.6);
            SetChartsData(ref img);
        }

        private void InitializeNotifications(Guid guid)
        {
            _guid = guid;
            _notificationSystem = NotificationSystem.Instance;
            _notificationSystem.RegisterInChannel(_guid, Notification);
        }

        private void Notification(Message message, params object[] args)
        {
            switch (message)
            {
                case Message.GroupRemoved:
                case Message.ImageClosed:
                case Message.ImageOpened:
                case Message.ImageReplaced:
                    Close();
                    break;
                case Message.ImageValueChanged:
                {
                    var img = _getImage();
                    Model.ImageType = img.Type;
                    SetChartsData(ref img);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(message), message, null);
            }
        }

        private void SetChartsData(ref ApoImage img)
        {
            Model.Bitmap = img.ToImageSource();
            Model.ImageWidth = img.Width;
            Model.ImageHeight = img.Height;
            var ips = new ApoIntensityProfile(img, new System.Drawing.Point(Model.X0, Model.Y0),
                new System.Drawing.Point(Model.X1, Model.Y1));
            if (img.Type == ImageType.Grayscale)
            {
                string[] labels = new string[ips.Points.Length];
                for (int i = 0; i < ips.Points.Length; i++)
                {
                    labels[i] = $"({ips.Points[i].X},{ips.Points[i].Y}): {ips[0][i]}";
                }
                BcGrey.SetData(ips[0].Data, _cGrey, labels);
                BcGrey.MaxXValue.Content = ips[0].Min;
                BcGrey.MinXValue.Content = ips[0].Max;
            }
            else if (img.Type == ImageType.Bgr || img.Type == ImageType.Bgra)
            {
                string[] labels0 = new string[ips.Points.Length];
                string[] labels1 = new string[ips.Points.Length];
                string[] labels2 = new string[ips.Points.Length];
                string[] labels3 = new string[ips.Points.Length];
                for (int i = 0; i < ips.Points.Length; i++)
                {
                    labels0[i] = $"({ips.Points[i].X},{ips.Points[i].Y}): {ips[0][i]}";
                    labels1[i] = $"({ips.Points[i].X},{ips.Points[i].Y}): {ips[1][i]}";
                    labels2[i] = $"({ips.Points[i].X},{ips.Points[i].Y}): {ips[2][i]}";
                    if (img.Type == ImageType.Bgra) labels3[i] = $"({ips.Points[i].X},{ips.Points[i].Y}): {ips[2][i]}";
                }
                BcRed.SetData(ips[0].Data, _cRed, labels0);
                BcRed.MaxXValue.Content = ips[0].Min;
                BcRed.MinXValue.Content = ips[0].Max;
                BcRed.Redraw();
                BcGreen.SetData(ips[1].Data, _cGreen, labels1);
                BcGreen.MaxXValue.Content = ips[1].Min;
                BcGreen.MinXValue.Content = ips[1].Max;
                BcGreen.Redraw();
                BcBlue.SetData(ips[2].Data, _cBlue, labels2);
                BcBlue.MaxXValue.Content = ips[2].Min;
                BcBlue.MinXValue.Content = ips[2].Max;
                BcBlue.Redraw();
                if (img.Type == ImageType.Bgra)
                {
                    BcAlpha.SetData(ips[3].Data, _cAlpha, labels3);
                    BcAlpha.MaxXValue.Content = ips[3].Min;
                    BcAlpha.MinXValue.Content = ips[3].Max;
                    BcAlpha.Redraw();
                }
            }
        }

        private void RangeZoomX_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Model.HorizontalScrollBarVisibility =
                Model.ZoomX > 1 ? ScrollBarVisibility.Visible : ScrollBarVisibility.Hidden;
            UpdateChartZoom();
        }

        private void RangeZoomY_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Model.VerticalScrollBarVisibility =
                Model.ZoomY > 1 ? ScrollBarVisibility.Visible : ScrollBarVisibility.Hidden;
            UpdateChartZoom();
        }

        private void UpdateChartZoom()
        {
            BcGrey.Height = GridRow.ActualHeight * Model.ZoomY;
            BcGrey.Width = MGrid.ActualWidth * Model.ZoomX;
            BcRed.Height = GridRow.ActualHeight * Model.ZoomY;
            BcRed.Width = MGrid.ActualWidth * Model.ZoomX;
            BcGreen.Height = GridRow.ActualHeight * Model.ZoomY;
            BcGreen.Width = MGrid.ActualWidth * Model.ZoomX;
            BcBlue.Height = GridRow.ActualHeight * Model.ZoomY;
            BcBlue.Width = MGrid.ActualWidth * Model.ZoomX;
            BcAlpha.Height = GridRow.ActualHeight * Model.ZoomY;
            BcAlpha.Width = MGrid.ActualWidth * Model.ZoomX;
        }

        private void RangeScaleY_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BcGrey.ScaleY = Model.ScaleY;
            BcRed.ScaleY = Model.ScaleY;
            BcGreen.ScaleY = Model.ScaleY;
            BcBlue.ScaleY = Model.ScaleY;
            BcAlpha.ScaleY = Model.ScaleY;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            GridSettings.MaxHeight = GridRow.ActualHeight / 2.75;
            GridSettings.Height = GridRow.ActualHeight / 2.75;
            Model.LineOriginPoint = new Point(GridSettings.ActualWidth / 2 - Img.ActualWidth / 2,
                GridSettings.ActualHeight / 2 - Img.ActualHeight / 2);
            Model.IndicatorX = Model.LineOriginPoint.X + Img.ActualWidth / Model.Bitmap.Width * Model.X0;
            Model.IndicatorY = Model.LineOriginPoint.Y + Img.ActualHeight / Model.Bitmap.Height * Model.Y0;
            Model.LineX0 = Model.LineOriginPoint.X + Img.ActualWidth / Model.Bitmap.Width * Model.X0;
            Model.LineY0 = Model.LineOriginPoint.Y + Img.ActualHeight / Model.Bitmap.Height * Model.Y0;
            Model.LineX1 = Model.LineOriginPoint.X + Img.ActualWidth / Model.Bitmap.Width * Model.X1;
            Model.LineY1 = Model.LineOriginPoint.Y + Img.ActualHeight / Model.Bitmap.Height * Model.Y1;
            UpdateChartZoom();
            base.OnRenderSizeChanged(sizeInfo);
        }


        private void ChBoxSettings_OnClick(object sender, RoutedEventArgs e)
        {
            if (ChBoxSettings.IsChecked == true)
            {
                GridSettings.Visibility = Visibility.Visible;
                GridRowSett.Height = GridLength.Auto;
                Model.LineOriginPoint = new Point(GridSettings.ActualWidth / 2 - Img.ActualWidth / 2,
                    GridSettings.ActualHeight / 2 - Img.ActualHeight / 2);
                Model.IndicatorX = Model.LineOriginPoint.X + Img.ActualWidth / Model.Bitmap.Width * Model.X0;
                Model.IndicatorY = Model.LineOriginPoint.Y + Img.ActualHeight / Model.Bitmap.Height * Model.Y0;
                Model.LineX0 = Model.LineOriginPoint.X + Img.ActualWidth / Model.Bitmap.Width * Model.X0;
                Model.LineY0 = Model.LineOriginPoint.Y + Img.ActualHeight / Model.Bitmap.Height * Model.Y0;
                Model.LineX1 = Model.LineOriginPoint.X + Img.ActualWidth / Model.Bitmap.Width * Model.X1;
                Model.LineY1 = Model.LineOriginPoint.Y + Img.ActualHeight / Model.Bitmap.Height * Model.Y1;
            }
            else
            {
                GridSettings.Visibility = Visibility.Collapsed;
                GridRowSett.Height = new GridLength(0, GridUnitType.Pixel);
            }

            ForceRedraw();
        }

        private void ForceRedraw()
        {
            var tmp = Application.Current.MainWindow;
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Height += 1;
            Application.Current.MainWindow.Width += 1;
            Application.Current.MainWindow.Height -= 1;
            Application.Current.MainWindow.Width -= 1;
            Application.Current.MainWindow = tmp;
        }

        private void Img_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SetPoints(e);
        }

        private void Line_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SetPoints(e);
        }

        private void SetPoints(MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(Img);
                var x = (int) Math.Round(pos.X / Img.ActualWidth * Model.ImageWidth);
                var y = (int) Math.Round(pos.Y / Img.ActualHeight * Model.ImageHeight);
                Model.LineOriginPoint = new Point(GridSettings.ActualWidth / 2 - Img.ActualWidth / 2,
                    GridSettings.ActualHeight / 2 - Img.ActualHeight / 2);
                if (e.ChangedButton == MouseButton.Left)
                {
                    Model.X0 = x;
                    Model.Y0 = y;
                    Model.IndicatorX = Model.LineOriginPoint.X + Img.ActualWidth / Model.Bitmap.Width * Model.X0;
                    Model.IndicatorY = Model.LineOriginPoint.Y + Img.ActualHeight / Model.Bitmap.Height * Model.Y0;
                    Model.LineX0 = Model.LineOriginPoint.X + Img.ActualWidth / Model.Bitmap.Width * Model.X0;
                    Model.LineY0 = Model.LineOriginPoint.Y + Img.ActualHeight / Model.Bitmap.Height * Model.Y0;
                }

                if (e.ChangedButton == MouseButton.Right)
                {
                    Model.X1 = x;
                    Model.Y1 = y;
                    Model.LineX1 = Model.LineOriginPoint.X + Img.ActualWidth / Model.Bitmap.Width * Model.X1;
                    Model.LineY1 = Model.LineOriginPoint.Y + Img.ActualHeight / Model.Bitmap.Height * Model.Y1;
                }

                var img = _getImage();
                SetChartsData(ref img);
            }
        }
    }
}