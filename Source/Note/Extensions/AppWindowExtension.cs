using Microsoft.UI;
using Microsoft.UI.Windowing;
using System;

namespace Note.Extensions
{
    public static class AppWindowExtension
    {
        public static AppWindow GetAppWindow(this Microsoft.UI.Xaml.Window window)
        {
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
            return GetAppWindowFromWindowHandle(windowHandle);
        }
        private static AppWindow GetAppWindowFromWindowHandle(IntPtr windowHandle)
        {
            WindowId windowId;
            windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            return AppWindow.GetFromWindowId(windowId);
        }
    }
}
