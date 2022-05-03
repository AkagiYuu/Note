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
    public sealed partial class Menu : UserControl
    {
        public Menu()
        {
            this.InitializeComponent();
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            App.Main.Tabs.TextEditor.TextDocument.Undo();
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            App.Main.Tabs.TextEditor.TextDocument.Selection.Cut();
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            App.Main.Tabs.TextEditor.TextDocument.Selection.Copy();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            App.Main.Tabs.TextEditor.TextDocument.Selection.Paste(0);
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            FileManager.Open(App.Main.Tabs.SelectedTab);
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            FileManager.Save(App.Main.Tabs.SelectedTab);
        }

        private void SaveFileAs(object sender, RoutedEventArgs e)
        {
            FileManager.SaveAs(App.Main.Tabs.SelectedTab);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }
    }
}