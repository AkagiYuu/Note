using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Note.Controls;
using Note.Extensions;
using Note.Utilities;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Note.Components;

public sealed partial class Menu : UserControl
{
    private TabBar Tabs;

    public Menu() => InitializeComponent();

    private void WindowLoaded(object sender, RoutedEventArgs e) => Tabs = MainWindow.Current.Tabs;

    private void Undo(object sender, RoutedEventArgs e) => Tabs.SelectedTab.GetTextEditor().Undo();

    private void Cut(object sender, RoutedEventArgs e) => Tabs.SelectedTab.GetTextEditor().Cut();

    private void Copy(object sender, RoutedEventArgs e) => Tabs.SelectedTab.GetTextEditor().Copy();

    //private void Paste( object sender, RoutedEventArgs e ) => Tabs.SelectedTab.GetTextEditor().Paste();

    private async void OpenFile(object sender, RoutedEventArgs e) => await FilePicker.Open(Tabs);

    private async void SaveFile(object sender, RoutedEventArgs e) => await FilePicker.Save(Tabs, Tabs.SelectedTab);

    private async void SaveFileAs(object sender, RoutedEventArgs e) => await FilePicker.SaveAs(Tabs, Tabs.SelectedTab);

    //private void Exit(object sender, RoutedEventArgs e) => Application.Current.Exit();

    private async void Exit(object sender, RoutedEventArgs e) => await FilePicker.CloseAll(Tabs);

    private async void SaveAllFile(object sender, RoutedEventArgs e) => await FilePicker.SaveAll(Tabs);
}
