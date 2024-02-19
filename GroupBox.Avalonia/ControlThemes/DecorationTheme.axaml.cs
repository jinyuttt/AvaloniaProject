using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace GroupBox.Avalonia.ControlThemes
{
    public partial class DecorationTheme: Styles
    {
        public DecorationTheme(IServiceProvider? sp = null)
        {
            AvaloniaXamlLoader.Load(sp, this);
        }
    }
}
