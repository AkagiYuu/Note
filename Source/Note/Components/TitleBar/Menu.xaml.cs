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

    private void OpenFile(object sender, RoutedEventArgs e) => FileManager.Open(Tabs);

    private void SaveFile(object sender, RoutedEventArgs e) => FileManager.Save(Tabs.SelectedTab);

    private void SaveFileAs(object sender, RoutedEventArgs e) => FileManager.SaveAs(Tabs.SelectedTab);

    private void Exit(object sender, RoutedEventArgs e) => Application.Current.Exit();
}
