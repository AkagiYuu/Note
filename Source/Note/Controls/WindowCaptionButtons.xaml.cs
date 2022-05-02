using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Note.Utilities;

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
