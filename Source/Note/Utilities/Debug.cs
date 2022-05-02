using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Utilities
{
    internal class Debug
    {
        public static async void DisplayDialog(string Content, XamlRoot Root)
        {
            ContentDialog Dialog = new ContentDialog
            {
                Title = "Debug",
                Content = Content,
                CloseButtonText = "Ok",
                XamlRoot = Root
            };

            ContentDialogResult result = await Dialog.ShowAsync();
        }
    }
}
