using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Note.Extensions;
using Windows.UI;

namespace Note.Utilities;

public static class Setting
{
    public static SolidColorBrush Border
    {
        get => (SolidColorBrush)Application.Current.Resources["Border"];
        set => Application.Current.Resources["Border"] = value;
    }

    public static SolidColorBrush Background
    {
        get => (SolidColorBrush)Application.Current.Resources["Background"];
        set => Application.Current.Resources["Background"] = value;
    }

    public static SolidColorBrush SelectedTabHeaderForeground
    {
        get => (SolidColorBrush)Application.Current.Resources["TabViewItemHeaderForegroundSelected"];
        set => Application.Current.Resources["TabViewItemHeaderForegroundSelected"] = value;
    }

    public static void LoadConfig(Option Options)
    {
        Border = new SolidColorBrush(Options.Border.ToColor());
        Background = new SolidColorBrush(Options.Background.ToColor());
        SelectedTabHeaderForeground = new SolidColorBrush(Options.SelectedTabHeaderForeground.ToColor());
    }
}
