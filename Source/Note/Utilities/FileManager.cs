using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Note.Controls;
using Note.Extensions;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Note.Utilities;

//public record FileInfomation(string Path, string Name);

public static class FileManager
{
    private static readonly FileOpenPicker _fileOpenPicker;
    private static readonly FileSavePicker _fileSavePicker;

    static FileManager()
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

    public static async void Open(TabBar Tabs)
    {
        try
        {
            var Tab = Tabs.SelectedTab;

            var file = await _fileOpenPicker.PickSingleFileAsync();
            if (file == null) return;

            var index = Tabs.FindTab(file.Path);

            if (index != -1)
            {
                Tabs.SelectedIndex = index;
                return;
            }
            //Tabs.Info = new FileInfomation(file.Path, file.Name);
            Tab.SetFileInfo(file.Path, file.Name);

            var Text = await FileIO.ReadTextAsync(file);
            Tab.GetTextEditor().Text = Text;
        }
        catch (Exception e)
        {
            var Message = $"Can't open the file (Exception: {e.Message})";
            Debug.DisplayDialog(Message, "Exeption");
        }
    }

    public static async void SaveAs(TabViewItem Tab)
    {
        var file = await _fileSavePicker.PickSaveFileAsync();

        if (file == null) return;

        // Prevent updates to the remote version of the file until we finish making changes
        // CachedFileManager.DeferUpdates(file);

        var TextEditor = Tab.GetTextEditor();
        await FileIO.WriteTextAsync(file, TextEditor.Text);

        // Let Windows know that we're finished changing the file so
        // the other app can update the remote version of the file.
        // Completing updates may require Windows to ask for user input.
        // var status = await CachedFileManager.CompleteUpdatesAsync(file);

        // if (status != FileUpdateStatus.Complete) throw new IOException($"File {file.Name} couldn't be saved.");
        Tab.SetFileInfo(file.Path, file.Name);
    }

    public static async void Save(TabViewItem Tab)
    {
        var FilePath = Tab.GetFilePath();
        if (FilePath == string.Empty)
        {
            SaveAs(Tab);
            return;
        }

        var file = await StorageFile.GetFileFromPathAsync(FilePath);
        var TextEditor = Tab.GetTextEditor();

        await FileIO.WriteTextAsync(file, TextEditor.Text);
    }
}
