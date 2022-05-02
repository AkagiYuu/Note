using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WinRT.Interop;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Threading.Tasks;
using Windows.Storage.Provider;
using Note.Controls;

namespace Note.Utilities
{
    public record Infomation(string Path, string Name);

    internal static class FileManager
    {
        public static async void Open(TabBar Tabs)
        {
            var Picker = new FileOpenPicker();

            InitializeWithWindow.Initialize(Picker, MainWindow.Hwnd);

            Picker.FileTypeFilter.Add(".txt");
            Picker.FileTypeFilter.Add("*");

            var file = await Picker.PickSingleFileAsync();
            if (file == null) return;
            Tabs.FileInfomation = new Infomation(file.Path, file.Name);

            var Text = await FileIO.ReadTextAsync(file);
            Tabs.TextEditorContent = Text;
        }

        public static async void SaveAs(TabBar Tabs)
        {
            var Picker = new FileSavePicker();

            InitializeWithWindow.Initialize(Picker, MainWindow.Hwnd);

            Picker.FileTypeChoices.Add("Plain Text", new List<string> { ".txt" });
            Picker.FileTypeChoices.Add("All file", new List<string> { "." });

            var file = await Picker.PickSaveFileAsync();

            if (file == null) return;

            // Prevent updates to the remote version of the file until we finish making changes
            CachedFileManager.DeferUpdates(file);


            await FileIO.WriteTextAsync(file, Tabs.TextEditorContent);

            // Let Windows know that we're finished changing the file so
            // the other app can update the remote version of the file.
            // Completing updates may require Windows to ask for user input.
            var status = await CachedFileManager.CompleteUpdatesAsync(file);

            if (status != FileUpdateStatus.Complete) throw new IOException($"File {file.Name} couldn't be saved.");

            Tabs.FileInfomation = new Infomation(file.Path, file.Name);
        }

        public static async void Save(TabBar Tabs)
        {
            if (Tabs.FileInfomation.Path == String.Empty)
            {
                SaveAs(Tabs);
                return;
            }

            var file = await StorageFile.GetFileFromPathAsync(Tabs.FileInfomation.Path);
            await FileIO.WriteTextAsync(file, Tabs.TextEditorContent);
        }
    }
}
