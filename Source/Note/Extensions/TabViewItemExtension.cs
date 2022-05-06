using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Controls;

namespace Note.Extensions
{
    public static class TabViewItemExtension
    {
        public static RichEditBox GetTextEditor(this TabViewItem Tab) => (RichEditBox)Tab.Content;

        public static string GetText(this TabViewItem Tab)
        {
            Tab.GetTextEditor()
               .Document
               .GetText(TextGetOptions.None, out var Text);
            return Text;
        }

        public static void SetText(this TabViewItem Tab, string Text)
            => Tab.GetTextEditor()
                  .TextDocument
                  .SetText(TextSetOptions.None, Text);

        public static void SetFileInfo(this TabViewItem Tab, string FilePath, string FileName) => (Tab.Name, Tab.Header) = (FilePath, FileName);

        public static string GetFilePath(this TabViewItem Tab) => Tab.Name;

        public static string GetFileName(this TabViewItem Tab) => Tab.Header.ToString();

        public static void Undo(this TabViewItem Tab)
            => Tab.GetTextEditor()
                  .TextDocument
                  .Undo();

        public static void Cut(this TabViewItem Tab)
            => Tab.GetTextEditor()
                  .TextDocument
                  .Selection
                  .Cut();

        public static void Copy(this TabViewItem Tab)
            => Tab.GetTextEditor()
                  .TextDocument
                  .Selection
                  .Copy();

        public static void Paste(this TabViewItem Tab, int format = 0)
            => Tab.GetTextEditor()
                  .TextDocument
                  .Selection
                  .Paste(format);
    }
}
