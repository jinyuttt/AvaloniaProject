using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace GroupBox.Avalonia.ControlThemes
{
    public class PanelTheme:Styles
    {
        public PanelTheme(IServiceProvider? sp = null) {
            AvaloniaXamlLoader.Load(sp, this);
        }
    }
}
