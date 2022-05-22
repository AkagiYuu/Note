using System;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Note.Controls;
using Note.Utilities;
using static Note.Utilities.WindowUtility;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Note;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public dynamic Test { get; set; }
    public static new MainWindow Current { get; set; }

    private readonly AppWindow _appWindow;
    public AppWindow AppWindow => _appWindow;

    private readonly OverlappedPresenter _presenter;

    public TabBar Tabs => _tabs;

    private static IntPtr _hWnd;
    public static IntPtr Hwnd => _hWnd;

    public MainWindow()
    {
        InitializeComponent();

        _hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

        var Id = Win32Interop.GetWindowIdFromWindow(_hWnd);
        _appWindow = AppWindow.GetFromWindowId(Id);

        _presenter = _appWindow.Presenter as OverlappedPresenter;
        HideTitlebar();


        long CurrentExtenededStyle = GetWindowAttribute(_hWnd, WindowAttribute.ExtendedStyle);
        if ((CurrentExtenededStyle & ExtendedWindowStyle.Layered) == 0)
        {
            SetWindowAttribute(_hWnd, WindowAttribute.ExtendedStyle, (IntPtr)(CurrentExtenededStyle | ExtendedWindowStyle.Layered));
            SetLayeredWindowAttributes(_hWnd, 0, 240, LayeredWindowFlag.Alpha);
        }
    }

    private void HideTitlebar()
    {
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(FakeTitleBar);
    }

    public XamlRoot Root => Content.XamlRoot;

    public void Minimize() => _presenter.Minimize();

    public string MaximizeHandle() => _presenter.State == OverlappedPresenterState.Maximized ? Restore() : Maximize();

    public string Maximize()
    {
        _presenter.Maximize();
        return (string)Application.Current.Resources["RestoreButton"];
    }

    public string Restore()
    {
        _presenter.Restore();
        return (string)Application.Current.Resources["MaximizeButton"];
    }
}
