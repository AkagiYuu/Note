using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Note.Utilities;
using Note.Extensions;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Note.Controls
{
    public sealed class TabBar : TabView
    {

        public TabViewItem SelectedTab
        {
            get => (TabViewItem)SelectedItem;
            set => SelectedItem = value;
        }

        public RichEditBox TextEditor
        {
            get => (RichEditBox)SelectedTab.Content;
        }

        public string TextEditorContent
        {
            get
            {
                TextEditor.TextDocument.GetText(TextGetOptions.None, out var result);
                return result;
            }
            set => TextEditor.TextDocument.SetText(TextSetOptions.None, value);
        }

        //public FileInfomation Info
        //{
        //    get => new(SelectedTab.Name, SelectedTab.Header.ToString());
        //    set => (SelectedTab.Name, SelectedTab.Header) = value;
        //}

        public TabBar()
        {
            NewTab();

            AddTabButtonClick += (sender, _) => ((TabBar)sender).NewTab();
            TabCloseRequested += (sender, args) => ((TabBar)sender).RemoveTab(args.Tab);
        }

        public void NewTab(string Header = "New Document", Symbol Icon = Symbol.Document)
            => NewTab(new SymbolIconSource() { Symbol = Icon }, Header);

        public void NewTab(IconSource Icon, string Header = "New Document")
        {
            var newTab = new TabViewItem
            {
                IconSource = Icon,
                Header = Header,
                Content = new RichEditBox()
                {
                    Style = (Style)Application.Current.Resources["RichEditBoxStyle"]
                }
            };

            TabItems.Add(newTab);
            SelectedIndex = TabItems.Count - 1;
        }

        public void RemoveTab(TabViewItem Tab)
        {
            TabItems.Remove(Tab);
            if (TabItems.Count == 0) Application.Current.Exit();
        }
    }
}
