using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Note.Utilities;
internal class Popup
{
    public static async void Display(string Title, string Content, string CloseButtonText = "OK")
    {
        var Dialog = new ContentDialog
        {
            Title = Title,
            Content = Content,
            CloseButtonText = CloseButtonText,
            Translation = new Vector3(0, 0, 16),
            XamlRoot = MainWindow.Current.Root
        };

        await Dialog.ShowAsync();
    }
    public static async Task<ContentDialogResult> Display(string Title, string Content, string PrimaryButtonText, string SecondaryButtonText, string CloseButtonText = "Close", ContentDialogButton DefaultButton = ContentDialogButton.Primary)
    {
        var Dialog = new ContentDialog
        {
            Title = Title,
            Content = Content,
            PrimaryButtonText = PrimaryButtonText,
            SecondaryButtonText = SecondaryButtonText,
            CloseButtonText = CloseButtonText,
            DefaultButton = DefaultButton,
            Translation = new Vector3(0, 0, 16),
            XamlRoot = MainWindow.Current.Root
        };

        return await Dialog.ShowAsync();
    }
}
