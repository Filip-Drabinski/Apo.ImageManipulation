using System.Windows;

namespace Apo.Gui.Windows
{
    /// <summary>
    ///     Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public DebugWindow()
        {
            InitializeComponent();
        }

        private bool TextBoxInt_OnValidateValue(int value)
        {
            return value >= 0 && value < 10;
        }

        private bool TextBoxDouble_OnValidateValue(double value)
        {
            return value >= 0 && value < 10;
        }
    }
}