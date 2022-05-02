using Microsoft.UI;
using Microsoft.UI.Windowing;
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
using Note.Controls;

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

        public TabBar Tabs { get => _tabs; }

        private static IntPtr _hwnd;
        public static IntPtr Hwnd
        {
            get => _hwnd;
        }

        private bool _isMaximize = false;

        public MainWindow()
        {
            this.InitializeComponent();

            HideTitleBar();

            _hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        }

        private void HideTitleBar()
        {
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
        }

        public string MaximizeHandle()
        {
            return _isMaximize ? Restore() : Maximize();
        }

        public string Maximize()
        {
            PInvoke.User32.ShowWindow(_hwnd, PInvoke.User32.WindowShowStyle.SW_SHOWMAXIMIZED);
            _isMaximize = true;
            return _restoreButtonPath;
        }

        public string Restore()
        {
            PInvoke.User32.ShowWindow(_hwnd, PInvoke.User32.WindowShowStyle.SW_RESTORE);
            _isMaximize = false;
            return _maximizeButtonPath;
        }

        public void Minimize()
        {
            PInvoke.User32.ShowWindow(_hwnd, PInvoke.User32.WindowShowStyle.SW_SHOWMINIMIZED);
        }
    }
}
