using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Note.Extensions;
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
            App.Main.Tabs.SelectedTab.Undo();
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            App.Main.Tabs.SelectedTab.Cut();
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            App.Main.Tabs.SelectedTab.Copy();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            App.Main.Tabs.SelectedTab.Paste();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            FileManager.Open(App.Main.Tabs);
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