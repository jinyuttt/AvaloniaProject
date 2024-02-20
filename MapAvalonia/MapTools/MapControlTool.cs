using Avalonia.Threading;
using MapAvalonia.Models;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Logging;
using Mapsui.Projections;

using Mapsui.UI.Avalonia;
using Mapsui.Widgets.ScaleBar;
using System;


namespace MapAvalonia.MapTools
{
    internal class MapControlTool
    {
       public static  MapControl mapControl=null;

        public static  void Init()
        {
            
            mapControl.Map.Widgets.Enqueue(new ScaleBarWidget(mapControl.Map) { TextAlignment = Mapsui.Widgets.Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top });
            mapControl.Map.Widgets.Enqueue(new Mapsui.Widgets.Zoom.ZoomInOutWidget { MarginX = 20, MarginY = 40 });
            mapControl.Map.Navigator.RotationLock = false;
          //  mapControl.Renderer.WidgetRenders[typeof(CustomWidget)] = new CustomWidgetSkiaRenderer();
            //var point = SphericalMercator.FromLonLat(16.58, 31.2);

            //mapControl.Map.Home = n => n.CenterOn(new MPoint(point.x, point.y));

            //mapControl.Map.Navigator.RotateTo(200 * 360);
        }
        public Uri GetUri(int x, int y, int zoomLevel)
        {
            string url = string.Format("http://wprd01.is.autonavi.com/appmaptile?x={0}&y={1}&z={2}&lang=zh_cn&size=1&scl=1&style=7", x, y, zoomLevel);
            return new Uri(url, UriKind.Absolute);
        }

       
       

        public static  void AddLayer(ILayer layer)
        {
            mapControl.Map.Layers.Add(layer);
            var extent = mapControl.Map.Layers[0].Extent!.Grow(mapControl.Map.Layers[0].Extent!.Width * 0.1);
            mapControl.Map.Navigator.ZoomToBox(extent);
            Dispatcher.UIThread.Invoke(new Action(() => { mapControl.Refresh(); }));
            
        }

        public static void RemoveLayer(ILayer layer)
        {
            mapControl.Map.Layers.Remove(layer);
            Dispatcher.UIThread.Invoke(new Action(() => { mapControl.Refresh(); }));
        }
       

    }
}
