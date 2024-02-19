using Avalonia.Threading;
using GisAvalonia.Log;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Logging;
using Mapsui.Projections;
using Mapsui.Samples.CustomWidget;
using Mapsui.UI.Avalonia;
using Mapsui.Widgets.ScaleBar;
using System;
using LogLevel = Mapsui.Logging.LogLevel;

namespace GisAvalonia.MapTools
{
    internal class MapControlTool
    {
       public static  MapControl mapControl=null;

        public static  void Init()
        {
            Logger.LogDelegate = RecordLog;
            mapControl.Map.Widgets.Enqueue(new ScaleBarWidget(mapControl.Map) { TextAlignment = Mapsui.Widgets.Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top });
            mapControl.Map.Widgets.Enqueue(new Mapsui.Widgets.Zoom.ZoomInOutWidget { MarginX = 20, MarginY = 40 });
            mapControl.Map.Navigator.RotationLock = false;
            mapControl.Renderer.WidgetRenders[typeof(CustomWidget)] = new CustomWidgetSkiaRenderer();
            //var point = SphericalMercator.FromLonLat(16.58, 31.2);

            //mapControl.Map.Home = n => n.CenterOn(new MPoint(point.x, point.y));

            //mapControl.Map.Navigator.RotateTo(200 * 360);
        }
        public Uri GetUri(int x, int y, int zoomLevel)
        {
            string url = string.Format("http://wprd01.is.autonavi.com/appmaptile?x={0}&y={1}&z={2}&lang=zh_cn&size=1&scl=1&style=7", x, y, zoomLevel);
            return new Uri(url, UriKind.Absolute);
        }

        private static GisAvalonia.Log.LogLevel Convert(LogLevel logLevel)
        {
            GisAvalonia.Log.LogLevel level = GisAvalonia.Log.LogLevel.Info;
            switch (logLevel)
            {
                case LogLevel.Error:
                    level=Log.LogLevel.Error;
                    break;
                    case LogLevel.Warning:
                    level=Log.LogLevel.Warn; break;
                case LogLevel.Information:
                    level = Log.LogLevel.Info;break;
                case LogLevel.Debug:
                    level=Log.LogLevel.Debug; break;
                default:
                    level=Log.LogLevel.Info; break;
            }
            return level;
        }
        private static void RecordLog(LogLevel level, string arg2, Exception? exception)
        {
            GisLog.Logger.Log(Convert(level), arg2, exception);
        }

        public static  void AddLayer(ILayer layer)
        {
            mapControl.Map.Layers.Add(layer);
            Dispatcher.UIThread.Invoke(new Action(() => { mapControl.Refresh(); }));
            
        }

        public static void RemoveLayer(ILayer layer)
        {
            mapControl.Map.Layers.Remove(layer);
            Dispatcher.UIThread.Invoke(new Action(() => { mapControl.Refresh(); }));
        }
       

    }
}
