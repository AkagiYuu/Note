using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Note.Extensions;
using Note.Utilities;
using Windows.Storage;
using System.Linq;
using System;
using Microsoft.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Note.Controls;

public sealed class TabBar : TabView
{
    public TabViewItem SelectedTab => (TabViewItem)SelectedItem;

    public void Initialize()
    {
        var Arguments = (Application.Current as App).Arguments;

        if (Arguments.Length < 2)
        {
            NewTab();
            return;
        }

        NewTab(new FileInfo(Arguments[1]));
    }

    public TabBar()
    {
        Loaded += (senders, args) => Initialize();
        AddTabButtonClick += (sender, args) => ((TabBar)sender).NewTab();
        TabCloseRequested += async (sender, args) => await ((TabBar)sender).Close(args.Tab);
    }

    public TabViewItem NewTab(string Header = "New Document")
    {
        var Tab = new TabViewItem
        {
            Header = Header,
        };
        Tab.Content = new TextEditor()
        {
            Parent = Tab,
        };

        TabItems.Add(Tab);
        SelectedIndex = TabItems.Count - 1;

        return Tab;
    }

    public TabViewItem NewTab(FileInfo file)
    {
        var Tab = new TabViewItem
        {
            Header = file.Name,
        };
        Tab.Content = new TextEditor()
        {
            Parent = Tab,
            Text = System.IO.File.ReadAllText(file.FullName),
        };

        TabItems.Add(Tab);
        SelectedIndex = TabItems.Count - 1;

        return Tab;
    }

    public async Task Close(TabViewItem Tab, bool? Save = null)
    {
        if (Tab.GetTextEditor().IsModified == false)
        {
            Remove(Tab);
            return;
        }

        if(Save is null)
        {
            var Action = await Popup.Display("Save", $"Save file {Tab.Header}?", "Save", "Don't Save");

            Save = Action switch
            {
                ContentDialogResult.Primary => true,
                ContentDialogResult.Secondary => false,
                _ => null
            };

            if (Save is null) return;
        }

        if (Save is true)
            await FilePicker.Save(this, SelectedTab);

        Remove(Tab);
    }
    private void Remove(TabViewItem Tab)
    {
        TabItems.Remove(Tab);
        if (TabItems.Count == 0) Application.Current.Exit();
    }


    public async Task CloseAll(bool Save = true)
    {
        var Items = TabItems.AsEnumerable().ToList();
        foreach (var Tab in Items)
        {
            SelectedItem = Tab;
            await Close((TabViewItem)Tab, Save);
        }
    }

    public object Find(string Name) => TabItems.ToList().SingleOrDefault(Tab => ((TabViewItem)Tab).Name == Name, null);

    public bool Navigate(string Name)
    {
        try
        {
            var Tab = Find(Name);

            if (Tab is null) return false;

            SelectedItem = Tab;
        }
        catch (Exception e)
        {
            Popup.Display("Error", $"2 or more tab open same file ({e.Message})");
        }
        return true;
    }
}
