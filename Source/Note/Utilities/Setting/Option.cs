namespace Note.Utilities;

public class ColorScheme
{
    public string Border { get; set; }
    public string Background { get; set; }
    public string SelectedTabHeaderForeground { get; set; }
}

public class Option
{
    public ColorScheme Scheme { get; set; }
}