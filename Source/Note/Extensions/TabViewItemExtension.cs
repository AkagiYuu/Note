using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Text;

namespace Note.Extensions
{
    public static class TabViewItemExtension
    {
        public static RichEditBox GetTextEditor(this TabViewItem Tab) => (RichEditBox)Tab.Content;

        public static string GetText(this TabViewItem Tab)
        {
            Tab.GetTextEditor().TextDocument.GetText(TextGetOptions.None, out var Text);
            return Text;
        }

        public static void SetText(this TabViewItem Tab, string Text) => Tab.GetTextEditor().TextDocument.SetText(TextSetOptions.None, Text);

        public static void SetFileInfo(this TabViewItem Tab, string FilePath, string FileName) => (Tab.Name, Tab.Header) = (FilePath, FileName);

        public static string GetFilePath(this TabViewItem Tab) => Tab.Name;

        public static string GetFileName(this TabViewItem Tab) => Tab.Header.ToString();
    }
}
