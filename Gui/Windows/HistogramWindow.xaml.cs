using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Apo.Core;

namespace Apo.Gui.Windows
{
    /// <summary>
    ///     Interaction logic for HistogramWindow.xaml
    /// </summary>
    public partial class HistogramWindow : Window
    {
        private Guid _guid;
        private readonly Func<ApoImage> _getImage;
        private NotificationSystem _notificationSystem;
        private readonly Color _cGrey = Color.FromRgb(0, 0, 0);
        private readonly Color _cRed = Color.FromRgb(255, 0, 0);
        private readonly Color _cGreen = Color.FromRgb(0, 255, 0);
        private readonly Color _cBlue = Color.FromRgb(0, 0, 255);
        private readonly Color _cAlpha = Color.FromRgb(200, 200, 200);

        public HistogramWindow()
        {
            InitializeComponent();
        }

        public HistogramWindow(Guid guid, [NotNull] Func<ApoImage> getImage)
        {
            InitializeComponent();
            InitializeNotifications(guid);
            _getImage = getImage;
            BcGrey.Title.Text =  "Histogram grey channel";
            BcRed.Title.Text =   "Histogram bed  channel";
            BcGreen.Title.Text = "Histogram green channel";
            BcBlue.Title.Text =  "Histogram blue channel";
            BcAlpha.Title.Text = "Histogram alpha channel";
            var img = _getImage();
            Model.ImageType = img.Type;
            SetChartsData(ref img);
        }

        private void SetChartsData(ref ApoImage img)
        {
            var hists = new ApoHistogram(img);
            if (img.Type == ImageType.Grayscale)
            {
                BcGrey.SetData(hists[0].Data, _cGrey);
            }
            else if (img.Type == ImageType.Bgr || img.Type == ImageType.Bgra)
            {
                BcRed.SetData(hists[0].Data, _cRed);
                BcRed.Redraw();
                BcGreen.SetData(hists[1].Data, _cGreen);
                BcGreen.Redraw();
                BcBlue.SetData(hists[2].Data, _cBlue);
                BcBlue.Redraw();
                if (img.Type == ImageType.Bgra)
                {
                    BcAlpha.SetData(hists[3].Data, _cAlpha);
                    BcAlpha.Redraw();
                }
            }
        }


        private void InitializeNotifications(Guid guid)
        {
            _guid = guid;
            _notificationSystem = NotificationSystem.Instance;
            _notificationSystem.RegisterInChannel(_guid, Notification);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _notificationSystem.UnregisterInChannel(_guid, Notification);
            base.OnClosing(e);
        }

        private void Notification(Message message, params object[] args)
        {
            switch (message)
            {
                case Message.GroupRemoved:
                case Message.ImageClosed:
                    Close();
                    break;
                case Message.ImageOpened:
                case Message.ImageReplaced:
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

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            UpdateChartZoom();
            base.OnRenderSizeChanged(sizeInfo);
        }

        private void RangeZoomX_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Model.HorizontalScrollBarVisibility = Model.ZoomX > 1 ? ScrollBarVisibility.Visible : ScrollBarVisibility.Hidden;
            UpdateChartZoom();
        }

        private void RangeZoomY_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Model.VerticalScrollBarVisibility = Model.ZoomY > 1 ? ScrollBarVisibility.Visible : ScrollBarVisibility.Hidden;
            UpdateChartZoom();
        }

        private void UpdateChartZoom()
        {
            BcGrey.Height = GridRow.ActualHeight * Model.ZoomY;
            BcGrey.Width = MGrid.ActualWidth     * Model.ZoomX;
            BcRed.Height = GridRow.ActualHeight * Model.ZoomY;
            BcRed.Width = MGrid.ActualWidth     * Model.ZoomX;
            BcGreen.Height = GridRow.ActualHeight * Model.ZoomY;
            BcGreen.Width = MGrid.ActualWidth    * Model.ZoomX;
            BcBlue.Height = GridRow.ActualHeight * Model.ZoomY;
            BcBlue.Width = MGrid.ActualWidth    * Model.ZoomX;
            BcAlpha.Height = GridRow.ActualHeight * Model.ZoomY;
            BcAlpha.Width = MGrid.ActualWidth    * Model.ZoomX;
        }

        private void RangeScaleY_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BcGrey.ScaleY  = Model.ScaleY;
            BcRed.ScaleY   = Model.ScaleY;
            BcGreen.ScaleY = Model.ScaleY;
            BcBlue.ScaleY  = Model.ScaleY;
            BcAlpha.ScaleY = Model.ScaleY;
        }
    }
}