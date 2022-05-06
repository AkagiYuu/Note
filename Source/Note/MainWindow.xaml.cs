using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Note.Controls;
using Note.Utilities;
using System;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Note
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {



        public static readonly string _maximizeButtonPath = "M 0 0 H 10 V 10 H 0 V 0";
        public static readonly string _restoreButtonPath = "M 0 2.5 H 7.5 V 10 H 0 V 2 M 2.5 2.5 V 0 H 10 V 7.5 H 7.5";

        private readonly AppWindow _appWindow;
        private readonly OverlappedPresenter _presenter;

        public TabBar Tabs { get => _tabs; }

        private static IntPtr _hWnd;
        public static IntPtr Hwnd { get => _hWnd; }

        private PointInt32 PreviousCursorPosition, WindowPosition;
        private bool IsMovingWindow = false;


        public MainWindow()
        {
            InitializeComponent();

            HideTitlebar();

            _hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            var Id = Win32Interop.GetWindowIdFromWindow(_hWnd);
            _appWindow = AppWindow.GetFromWindowId(Id);

            //_apw.Resize(new Windows.Graphics.SizeInt32(360, 360));

            _presenter = _appWindow.Presenter as OverlappedPresenter;
            //_presenter.IsResizable = false;
            //_presenter.IsMinimizable = false;
            //_presenter.SetBorderAndTitleBar(false, false);

            long CurrentExtenededStyle = WindowUtility.GetWindowAttribute(_hWnd, WindowAttribute.ExtendedStyle);
            if ((CurrentExtenededStyle & ExtendedWindowStyle.Layered) == 0)
            {
                WindowUtility.SetWindowAttribute(_hWnd, WindowAttribute.ExtendedStyle, (IntPtr)(CurrentExtenededStyle | ExtendedWindowStyle.Layered));
                //WindowUtility.SetLayeredWindowAttributes(_hWnd, (uint)ColorTranslator.ToWin32(Color.Red), 180, LayeredWindowFlag.All);
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
            return _restoreButtonPath;
        }

        public string Restore()
        {
            _presenter.Restore();
            return _maximizeButtonPath;
        }

        private void TitleBar_PointerReleased(object sender, PointerRoutedEventArgs e) => IsMovingWindow = false;

        private void TitleBar_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var properties = e.GetCurrentPoint((UIElement)sender).Properties;
            if (properties.IsLeftButtonPressed)
            {
                //(nXWindow, nYWindow) = _appWindow.Position;
                WindowPosition = _appWindow.Position;

                WindowUtility.GetCursorPos(out PreviousCursorPosition);
                //(nX, nY) = pt;
                IsMovingWindow = true;
            }
        }

        private void TitleBar_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (!IsMovingWindow) return;

            var properties = e.GetCurrentPoint((UIElement)sender).Properties;
            if (!properties.IsLeftButtonPressed) return;

            WindowUtility.GetCursorPos(out var point);

            _appWindow.Move(new PointInt32(
                WindowPosition.X + point.X - PreviousCursorPosition.X,
                WindowPosition.Y + point.Y - PreviousCursorPosition.Y
            ));
            e.Handled = true;
        }
    }
}
