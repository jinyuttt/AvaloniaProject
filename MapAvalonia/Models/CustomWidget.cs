using Mapsui;
using Mapsui.Styles;
using Mapsui.Widgets;

namespace MapAvalonia.Models;

public class CustomWidget : IWidget
{
    public HorizontalAlignment HorizontalAlignment { get; set; }
    public VerticalAlignment VerticalAlignment { get; set; }
    public MRect Margin { get; set; } = new MRect(20,20,100,100);
    public MRect? Envelope { get; set; }
    public bool HandleWidgetTouched(Navigator navigator, MPoint position)
    {
        navigator.CenterOn(0, 0);
        return true;
    }

    public Color? Color { get; set; }
    public MPoint Position { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public bool Enabled { get; set; } = true;
    public bool NeedsRedraw { get; set; }
    public float MarginX { get; set; }
    public float MarginY { get; set; }
}
