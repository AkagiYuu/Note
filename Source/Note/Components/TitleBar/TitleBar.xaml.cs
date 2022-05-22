using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.Graphics;
using static Note.Utilities.WindowUtility;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Note.Components;

public sealed partial class TitleBar : UserControl
{
    private bool IsPressed = false;

    private PointInt32 PreviousCursorPosition, WindowPosition;

    public TitleBar() => InitializeComponent();

    private void TitleBar_PointerReleased(object sender, PointerRoutedEventArgs e) => IsPressed = false;

    private void TitleBar_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        var properties = e.GetCurrentPoint((UIElement)sender).Properties;
        if (properties.IsLeftButtonPressed)
        {
            WindowPosition = MainWindow.Current.AppWindow.Position;

            GetCursorPos(out PreviousCursorPosition);
            IsPressed = true;
        }
    }

    private void TitleBar_PointerMoved(object sender, PointerRoutedEventArgs e)
    {
        if (!IsPressed) return;

        var properties = e.GetCurrentPoint((UIElement)sender).Properties;
        if (!properties.IsLeftButtonPressed) return;

        GetCursorPos(out var point);

        MainWindow.Current.AppWindow.Move(new PointInt32(
            WindowPosition.X + point.X - PreviousCursorPosition.X,
            WindowPosition.Y + point.Y - PreviousCursorPosition.Y
        ));
        e.Handled = true;
    }
}
