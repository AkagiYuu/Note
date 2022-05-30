using System;
using System.Text.Json;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Note.Extensions;

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

    public static async void LoadUserConfig()
    {
        var SettingFile = $@"{AppSettingFolder}\Config.json";

        if (!System.IO.File.Exists(SettingFile))
            return;

        var json = await File.Open(SettingFile);
        var Options = JsonSerializer.Deserialize<Option>(json);
        Setting.ChangeOption(Options);
    }

    public static void ChangeOption(Option Options) => ChangeColorScheme(Options.Scheme);

    public static void ChangeColorScheme(ColorScheme Scheme)
    {
        if (Scheme is null)
            return;

        if (Scheme.Border.IsValidHexColor())
            Border = new SolidColorBrush(Scheme.Border.ToColor());
        if (Scheme.Background.IsValidHexColor())
            Background = new SolidColorBrush(Scheme.Background.ToColor());
        if (Scheme.SelectedTabHeaderForeground.IsValidHexColor())
            SelectedTabHeaderForeground = new SolidColorBrush(Scheme.SelectedTabHeaderForeground.ToColor());
    }
}