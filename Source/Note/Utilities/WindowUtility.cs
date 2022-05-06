using System;
using System.Runtime.InteropServices;

namespace Note.Utilities
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RectangleArea
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
        public RectangleArea(int Left, int Top, int Right, int Bottom)
        {
            left = Left;
            top = Top;
            right = Right;
            bottom = Bottom;
        }
    }

    public static class WindowAttribute
    {
        public const int Style = -16;
        public const int ExtendedStyle = -20;
    }

    /// <summary>
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles"/>
    /// </summary>
    public static class ExtendedWindowStyle
    {
        public const int DoubleBorder = 0x00000001;

        /// <summary>
        /// Don't send WM_PARENTNOTIFY message to parent window when created or destroyed.
        /// </summary>
        public const int NoParentNotify = 0x00000004;

        public const int TopMost = 0x00000008;

        /// <summary>
        /// The window accepts drag-drop files
        /// </summary>
        public const int AcceptFiles = 0x00000010;

        public const int Transparent = 0x00000020;

        public const int MDIChild = 0x00000040;

        /// <summary>
        /// The window is intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font.
        /// </summary>
        public const int ToolWindow = 0x00000080;

        /// <summary>
        /// The window has a border with a raised edge.
        /// </summary>
        public const int WindowEdge = 0x00000100;

        /// <summary>
        /// The window has a border with a sunken edge.
        /// </summary>
        public const int ClientEdge = 0x00000200;

        /// <summary>
        /// The title bar of the window includes a question mark. When the user clicks the question mark, the cursor changes to a question mark with a pointer.
        /// </summary>
        public const int ContextHelp = 0x00000400;

        public const int RightAlign = 0x00001000;
        public const int Left_Align = 0x00000000;

        public const int RightToLeftReading = 0x00002000;
        public const int LeftToRightReading = 0x00000000;

        public const int LeftScrollBar = 0x00004000;
        public const int RightScrollBar = 0x00000000;

        /// <summary>
        /// The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.
        /// </summary>
        public const int ControlParent = 0x00010000;

        /// <summary>
        /// The window has a three-dimensional border style intended to be used for items that do not accept user input.
        /// </summary>
        public const int StaticEdge = 0x00020000;

        /// <summary>
        /// Forces a top-level window onto the taskbar when the window is visible.
        /// </summary>
        public const int AppWindow = 0x00040000;

        public const int OverLappedWindow = WindowEdge | ClientEdge;

        /// <summary>
        /// Modeless dialog box that presents an array of commands.
        /// </summary>
        public const int PaletteWindow = WindowEdge | ToolWindow | TopMost;

        public const int Layered = 0x00080000;

        /// <summary>
        /// The window does not pass its window layout to its child windows.
        /// </summary>
        public const int NoInheritLayout = 0x00100000;

        public const int NoRedirectionBitMap = 0x00200000;

        public const int RightToLeft = 0x00400000;

        /// <summary>
        /// Paints all descendants of a window in bottom-to-top painting order using double-buffering
        /// </summary>
        public const int Composited = 0x02000000;


        /// <summary>
        /// A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
        /// </summary>
        public const int NoActive = 0x08000000;
    }

    public static class LayeredWindowFlag
    {
        /// <summary>
        /// Use ColorCode as the transparency color.
        /// </summary>
        public const uint ColorCode = 0x00000001;

        /// <summary>
        /// Use Alpha to determine the opacity of the layered window.
        /// </summary>
        public const uint Alpha = 0x00000002;

        public const uint All = ColorCode | Alpha;
    }
    /// <summary>
    /// Note: this may effect performance
    /// </summary>
    public class WindowUtility
    {
        /// <summary>
        /// Retrieves a handle to a window whose class name and window name match the specified strings
        /// </summary>
        /// <param name="ParentHandle">A handle to the parent window</param>
        /// <param name="ChildAfter">A handle to a child window</param>
        /// <param name="ClassName">Classname</param>
        /// <param name="WindowTitle">Window's title(name)</param>
        /// <returns>Handle to the window</returns>
        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr ParentHandle, IntPtr ChildAfter, string ClassName, string WindowTitle);


        /// <summary>
        /// Retrieves the coordinates of a window's client area
        /// </summary>
        /// <param name="hWnd">A handle to the window</param>
        /// <param name="Result">Coordinates</param>
        /// <returns>Coordinates</returns>
        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetClientRect(IntPtr hWnd, out RectangleArea Result);


        /// <summary>
        /// Retrieves the coordinates of a window area (includes the non-client area, i.e. the window borders, caption bar, ...)
        /// </summary>
        /// <param name="hWnd">A handle to the window</param>
        /// <param name="Result">Coordinates</param>
        /// <returns>Coordinates</returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RectangleArea Result);


        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint ColorCode, byte Alpha, uint Flags);


        public static IntPtr SetWindowAttribute(IntPtr hWnd, int Attribute, IntPtr Value)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, Attribute, Value);
            }
            return SetWindowLongPtr64(hWnd, Attribute, Value);
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        public static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int Attribute, IntPtr Value);

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
        public static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int Attribute, IntPtr Value);


        public static long GetWindowAttribute(IntPtr hWnd, int Attribute)
        {
            if (IntPtr.Size == 4)
            {
                return GetWindowLong32(hWnd, Attribute);
            }
            return GetWindowLongPtr64(hWnd, Attribute);
        }

        [DllImport("User32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern long GetWindowLong32(IntPtr hWnd, int Attribute);

        [DllImport("User32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern long GetWindowLongPtr64(IntPtr hWnd, int Attribute);


        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetCursorPos(out Windows.Graphics.PointInt32 Position);
    }
}
