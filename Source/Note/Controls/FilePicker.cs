using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Note.Extensions;
using Note.Utilities;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Note.Controls;

public static class FilePicker
{
    private static readonly FileOpenPicker _fileOpenPicker;
    private static readonly FileSavePicker _fileSavePicker;

    static FilePicker()
    {
        _fileOpenPicker = CreateFileOpenPicker();
        _fileSavePicker = CreateFileSavePicker();
    }

    public static FileOpenPicker CreateFileOpenPicker()
    {
        var Picker = new FileOpenPicker();

        InitializeWithWindow.Initialize(Picker, MainWindow.Hwnd);

        Picker.FileTypeFilter.Add("*");
        Picker.FileTypeFilter.Add(".txt");

        return Picker;
    }

    public static FileSavePicker CreateFileSavePicker()
    {
        var Picker = new FileSavePicker();

        InitializeWithWindow.Initialize(Picker, MainWindow.Hwnd);

        Picker.FileTypeChoices.Add("Plain Text", new List<string> { ".txt" });
        Picker.FileTypeChoices.Add("All file", new List<string> { "." });

        return Picker;
    }

    public static async Task Open(TabBar Tabs)
    {
        try
        {
            var file = await _fileOpenPicker.PickSingleFileAsync();
            if (file == null) return;

            var IsSucceed = Tabs.Navigate(file.Path);
            if (IsSucceed) return;

            var Tab = Tabs.SelectedTab.IconSource is null? Tabs.SelectedTab : Tabs.NewTab();
            var TextEditor = Tab.GetTextEditor();

            TextEditor.Text = await File.Open(file);

            Tab.SetFileInfo(file.Path, file.Name);
            TextEditor.IsModified = false;
        }
        catch (Exception e)
        {
            var Message = $"Can't open the file (Exception: {e.Message})";
            await Popup.Display("Error", Message);
        }
    }

    public static async Task SaveAs(TabBar Tabs, TabViewItem Tab)
    {
        try
        {
            var file = await _fileSavePicker.PickSaveFileAsync();
            if (file == null) return;

            var IsSucceed = Tabs.Navigate(file.Path);
            if(IsSucceed)
            {
                await Popup.Display("Notify", "Consider closing this first");
                return;
            }

            var TextEditor = Tab.GetTextEditor();

            await File.Save(file, TextEditor.Text);

            Tab.SetFileInfo(file.Path, file.Name);
            TextEditor.IsModified = false;
        }
        catch (Exception e)
        {
            var Message = $"Can't save file (Exception: {e.Message})";
            await Popup.Display("Error", Message);
        }
    }

    public static async Task Save(TabBar Tabs, TabViewItem Tab)
    {
        try
        {
            var Path = Tab.Name;

            if (Path == string.Empty)
            {
                await SaveAs(Tabs, Tab);
                return;
            }

            var TextEditor = Tab.GetTextEditor();
            if (!TextEditor.IsModified) return;

            await File.Save(Path, TextEditor.Text);
            TextEditor.IsModified = false;
        }
        catch (Exception e)
        {
            var Message = $"Can't save file (Exception: {e.Message})";
            await Popup.Display("Error", Message);
        }
    }

    public static async Task SaveAll(TabBar Tabs)
    {
        var Items = Tabs.TabItems;
        for(int i = 0; i < Items.Count; i++)
            await Save(Tabs, (TabViewItem)Items[i]);
    }

    public static async Task CloseAll(TabBar Tabs, bool Save = true)
    {
        while(Tabs.TabItems.Count > 0)
        {
            await Tabs.Close(Tabs.SelectedTab, Save);
        }
    }
}
