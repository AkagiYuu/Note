using System;
using Microsoft.UI.Xaml.Controls;

namespace Note.Utilities;

internal class Debug
{
    public static async void DisplayDialog(string Content, string Title = "Debug")
    {
        var Dialog = new ContentDialog
        {
            Title = Title,
            Content = Content,
            CloseButtonText = "Ok",
            XamlRoot = MainWindow.Current.Root
        };

        await Dialog.ShowAsync();
    }
}
