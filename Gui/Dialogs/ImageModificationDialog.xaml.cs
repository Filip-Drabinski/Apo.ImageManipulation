using System.Windows;

namespace Apo.Gui.Dialogs
{
    /// <summary>
    ///     Interaction logic for ImageModificationDialog.xaml
    /// </summary>
    public partial class ImageModificationDialog : Window
    {
        public ImageModificationDialog()
        {
            InitializeComponent();
        }

        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}