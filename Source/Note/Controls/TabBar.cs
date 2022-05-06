using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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

        public int FindTab(string FilePath)
        {
            for (int i = 0; i < TabItems.Count; i++)
            {
                var Tab = (TabViewItem)TabItems[i];
                if (Tab.GetFilePath() == FilePath) return i;
            }

            return -1;
        }
    }
}
