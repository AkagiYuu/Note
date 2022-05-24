using System.Diagnostics;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Note.Controls;

public sealed class TextEditor : RichEditBox
{
    public static readonly FontIconSource ModifiedIcon = new() { Glyph = "\xE915" };

    public string Text
    {
        get
        {
            Document.GetText(TextGetOptions.None, out var Text);
            return Text;
        }

        set => Document.SetText(TextSetOptions.None, value);
    }

    private bool _isModified;

    public bool IsModified
    {
        get => _isModified;
        set 
        {
            if (_isModified == value) return;

            _isModified = value;
            Parent.IconSource = _isModified? ModifiedIcon : null;
        }
    }

    public new TabViewItem Parent { get; set; }

    public TextEditor()
    { 
        Style = (Style)Application.Current.Resources["RichEditBoxStyle"];
        
        Loaded += Initialize;
    }

    private void Initialize(object sender, RoutedEventArgs e)
    {
        TextChanging += (sender, args) => IsModified = true;
        Loaded -= Initialize;
    }


    public void Undo() => TextDocument.Undo();

    public void Cut() => TextDocument.Selection.Cut();

    public void Copy() => TextDocument.Selection.Copy();
}
