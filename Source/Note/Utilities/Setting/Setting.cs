using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Note.Extensions;
using Windows.UI;

namespace Note.Utilities;

public static class Setting
{
    public static readonly string AppSettingFolder = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Note";
    
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

    public static void LoadUserConfig()
    {
        var Options = JsonSerializer.Deserialize<Option>(System.IO.File.ReadAllText($@"{AppSettingFolder}\Config.json"));
        Setting.ChangeOption(Options);
    }

    public static void ChangeOption(Option Options) => ChangeColorScheme(Options.Scheme);
    
    public static void ChangeColorScheme(ColorScheme Scheme)
    {
        Border = new SolidColorBrush(Scheme.Border.ToColor());
        Background = new SolidColorBrush(Scheme.Background.ToColor());
        SelectedTabHeaderForeground = new SolidColorBrush(Scheme.SelectedTabHeaderForeground.ToColor());
    }
}
