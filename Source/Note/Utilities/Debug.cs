using Microsoft.UI.Xaml.Controls;
using System;

namespace Note.Utilities
{
    internal class Debug
    {
        public static async void DisplayDialog(string Content)
        {
            var Dialog = new ContentDialog
            {
                Title = "Debug",
                Content = Content,
                CloseButtonText = "Ok",
                XamlRoot = App.Main.Root
            };

            await Dialog.ShowAsync();
        }
    }
}
