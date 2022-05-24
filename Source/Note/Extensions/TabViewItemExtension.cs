using Microsoft.UI.Xaml.Controls;
using Note.Controls;

namespace Note.Extensions;

public static class TabViewItemExtension
{
    public static TextEditor GetTextEditor(this TabViewItem Tab) => (TextEditor)Tab.Content;

    public static void SetFileInfo(this TabViewItem Tab, string FilePath, string FileName) => (Tab.Name, Tab.Header) = (FilePath, FileName);
}
