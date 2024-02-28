using Mapsui;
using Mapsui.Extensions;
using Mapsui.Styles;
using Mapsui.Widgets;

namespace MapAvalonia.Models
{
    public class MyMouseCoordinatesWidget : TextBox, IWidgetExtended, IWidget
    {
        public Map Map { get; }
        public MyMouseCoordinatesWidget(Map map)
        {
            base.HorizontalAlignment = HorizontalAlignment.Center;
            base.VerticalAlignment = VerticalAlignment.Bottom;
            base.Text = "Mouse Position";
            this.BackColor = Color.White;
            Map = map;
        }

        public bool HandleWidgetMoving(Navigator navigator, MPoint position, WidgetArgs args)
        {
            MPoint mPoint = Map.Navigator.Viewport.ScreenToWorld(position);
            base.Text = $"{mPoint.X:F0}, {mPoint.Y:F0}";
            Map.RefreshGraphics();
            return false;
        }

        public bool HandleWidgetTouched(Navigator navigator, MPoint position, WidgetArgs args)
        {
            return false;
        }

        public bool HandleWidgetTouching(Navigator navigator, MPoint position, WidgetArgs args)
        {
            return false;
        }
    }
}
