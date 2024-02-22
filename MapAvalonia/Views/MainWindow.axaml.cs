using Avalonia.Controls;
using MapAvalonia.MapTools;
using MapAvalonia.Models;
using Mapsui.Projections;
using Mapsui;
using System;
using Mapsui.Styles;
using Mapsui.Logging;
using Mapsui.Extensions;
using Mapsui.Widgets.Zoom;
using Mapsui.Utilities;
using Mapsui.Tiling.Layers;

namespace MapAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           
            InitializeComponent();
            AttachMapsuiLogging();
            Init();
           
        }
        private async void Init()
        {
            using var geometryLayer = MapSource.CreateWorldCitiesLayer();
            var extent = geometryLayer.Extent!.Grow(10000);

          var _titlelayer=  MapSource.CreateTileLayer();

           // var titlelayer = await MapSource.CreateLayerAsync();
            var map = new Map
            {
                
                CRS = "EPSG:3857", // The Map CRS needs to be set
                BackColor = Color.Gray
            };
            mapControl.Map = map;
          
                
           map.Widgets.Add(new ZoomInOutWidget { MarginX = 20, MarginY = 40 });
            map.Layers.Add(_titlelayer);
            map.Layers.Add(geometryLayer);
            map.Navigator.ZoomToBox(extent);
            map.Navigator.RotationLock = false;
            mapControl.Renderer.WidgetRenders[typeof(CustomWidget)] = new CustomWidgetSkiaRenderer();
          
          
            map.Navigator.ViewportChanged += (s, e) =>
            {
              
                (var lon, var lat) = SphericalMercator.ToLonLat(map.Navigator.Viewport.CenterX, map.Navigator.Viewport.CenterY);

                Logger.Log(LogLevel.Information, $"Map center at {lat:0.000}/{lon:0.000}");
            };
            map.Home = (n) =>
            {
                var box = new MRect(10340390, 7554750, 10340735, 7554670);

                double resolution = ZoomHelper.CalculateResolutionForWorldSize(box.Width, box.Height,
                    map.Navigator.Viewport.Width, map.Navigator.Viewport.Height, MBoxFit.Fit);
                var p = box.Centroid;
                long d = 300;//-1;
                             //resolution = 1;
                map.Navigator.CenterOnAndZoomTo(p, resolution, d);
            };
            //var extent = mapControl.Map.Layers[0].Extent!.Grow(mapControl.Map.Layers[0].Extent!.Width * 0.1);
            //mapControl.Map.Navigator.ZoomToBox(extent);

            

            mapControl.Refresh();
          

        }

       
        

        public  void AttachMapsuiLogging()
        {
            Mapsui.Logging.Logger.LogDelegate += (level, message, ex) =>
            {
                Console.WriteLine($"{message} {ex?.Message}"); // <-- Put a break point here, most UI platforms do not show the console logging.
                                                               // todo: Forward to your own logger
            };
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
          var mr=  SphericalMercator.FromLonLat(116.322373, 40.002694).ToMPoint();
            mapControl.Map.Navigator.CenterOnAndZoomTo(mr,10);
        }
    }
}