using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Note.Controls
{
    public sealed partial class WindowCaptionButtons : UserControl
    {
        private MainWindow _main;

        public WindowCaptionButtons()
        {
            this.InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            _main = App.Main;
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            _main.Minimize();
        }

        private void Maximize(object sender, RoutedEventArgs e)
        {
            MaximizeButton.Content = _main.MaximizeHandle();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }
    }
}
