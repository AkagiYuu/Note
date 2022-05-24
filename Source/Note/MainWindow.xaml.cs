using System;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Note.Controls;
using Note.Utilities;
using System.Diagnostics;
using static Note.Utilities.User32API;
using Microsoft.UI.Composition.SystemBackdrops;
using System.Linq;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Note;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
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

        SetTitleBar();
        InitializeTransparency();

        _appWindow.Closing += OnClosing;
    }

    private static void InitializeTransparency()
    {
        long CurrentExtenededStyle = GetWindowAttribute(_hWnd, WindowAttribute.ExtendedStyle);
        if ((CurrentExtenededStyle & ExtendedWindowStyle.Layered) == 0)
        {
            SetWindowAttribute(_hWnd, WindowAttribute.ExtendedStyle, (IntPtr)(CurrentExtenededStyle | ExtendedWindowStyle.Layered));
            SetLayeredWindowAttributes(_hWnd, 0, 240, LayeredWindowFlag.Alpha);
        }
    }

    private async void OnClosing(AppWindow sender, AppWindowClosingEventArgs args)
    {
        args.Cancel = true;
        var IsUnsaved = _tabs.TabItems.ToList().Any(Tab => ((TabViewItem)Tab).IconSource is not null);

        if (!IsUnsaved) Application.Current.Exit();

        var Action = await Popup.Display("Notify", "There're unsaved files", "Save All & Exit", "Discard & Exit", "Cancel");

        bool? Save = Action switch
        {
            ContentDialogResult.Primary => true,
            ContentDialogResult.Secondary => false,
            _ => null,
        };

        if (Save is null) return;

        await FilePicker.CloseAll(_tabs);
    }

    private void SetTitleBar()
    {
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(_titleBar);
    }

    public XamlRoot Root => Content.XamlRoot;
}
