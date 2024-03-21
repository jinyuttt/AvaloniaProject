
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Widgets;
namespace Mapsui.Widgets.MouseCoordinatesWidget
{
    public class MyMouseCoordinatesWidget : TextBox, IWidgetExtended, IWidget
    {
        public Map Map { get; }
        public MyMouseCoordinatesWidget(Map map)
        {
            base.HorizontalAlignment = HorizontalAlignment.Center;
            base.VerticalAlignment = VerticalAlignment.Bottom;
            base.Text = "Mouse Position";
         
            Map = map;
        }

        public bool HandleWidgetMoving(Navigator navigator, MPoint position, WidgetArgs args)
        {
            MPoint mPoint = Map.Navigator.Viewport.ScreenToWorld(position);
            base.Text = $"{mPoint.X:F0}, {mPoint.Y:F0}";
            Map.RefreshGraphics();
            return false;
        }

        public bool HandleWidgetTouching(Navigator navigator, MPoint position, WidgetArgs args)
        {
            return false;
        }

        public bool HandleWidgetTouched(Navigator navigator, MPoint position, WidgetArgs args)
        {
            return false;
        }
    }
}
