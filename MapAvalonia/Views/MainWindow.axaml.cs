using Avalonia.Controls;
using MapAvalonia.MapTools;
using MapAvalonia.Models;
using Mapsui.Projections;
using Mapsui;
using System;
using Mapsui.Styles;
using Mapsui.Logging;
using Mapsui.Extensions;

using Mapsui.Utilities;
using Mapsui.Widgets;
using Mapsui.Widgets.MouseCoordinatesWidget;
using Mapsui.Widgets.PerformanceWidget;
using Mapsui.Widgets.BoxWidget;
using Mapsui.Widgets.ButtonWidget;
using Mapsui.Widgets.ScaleBar;
using Mapsui.Widgets.Zoom;
using BruTile;
using System.Text;

namespace MapAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           
            InitializeComponent();
            titleBar.OnPointerMouseHander += TitleBar_OnPointerMouseHander;
            AttachMapsuiLogging();
            Init();
           
        }

        private void TitleBar_OnPointerMouseHander(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
           this.BeginMoveDrag(e);
        }

        private async void Init()
        {
            using var geometryLayer = MapSource.CreateWorldCitiesLayer();
            var extent = geometryLayer.Extent!.Grow(10000);

          var _titlelayer=  MapSource.CreateTileLayer();

            var lineloygon = ComplexPolygon.CreateLineLayer();

            var complexPolygon = ComplexPolygon.CreateLayer();
            // var titlelayer = await MapSource.CreateLayerAsync();
            var map = new Map
            {
                
                CRS = "EPSG:3857", // The Map CRS needs to be set
                BackColor = Color.Gray
            };
            mapControl.Map = map;
            mapControl.Performance = new Performance();

            map.Widgets.Add(new ZoomInOutWidget { MarginX = 20, MarginY = 40 });

            //   map.Widgets.Add(new BoxWidget { MarginX = 20, MarginY = 40, Width=40 });
            //   map.Widgets.Add(new ButtonWidget { MarginX = 20, MarginY = 40,  Text= "²âButtonWidget" });
            //  map.Widgets.Add(new ScaleBarWidget(map) { MarginX = 20, MarginY = 40 });
            //  map.Widgets.Add(new HyperlinkWidget { MarginX = 20, MarginY = 40 });
            // map.Widgets.Add(new IconButtonWidget { MarginX = 20, MarginY = 40 });
            //   map.Widgets.Add(new TextButtonWidget { MarginX = 20, MarginY = 40 });
         


            map.Widgets.Add(new MapInfoWidget(map) { MarginX = 20, MarginY = 40 , Text= "ÎÒÔÚ²âÊÔMapInfoWidget", FeatureToText=AcText });
            map.Widgets.Add(new MyMouseCoordinatesWidget(map) {  HorizontalAlignment=Mapsui.Widgets.HorizontalAlignment.Right, VerticalAlignment= Mapsui.Widgets.VerticalAlignment.Bottom, Width=100, Height=20, MarginX = 20, MarginY = 10 , BackColor=Color.Green,  Text= "MouseCoordinatesWidget" });
       //  map.Widgets.Add(new PerformanceWidget(mapControl.Performance) { MarginX = 20, MarginY = 40 });
          //  map.Widgets.Add(new TextButtonWidget { MarginX = 20, MarginY = 40 });
           map.Layers.Add(_titlelayer);
            map.Layers.Add(geometryLayer);
            map.Layers.Add(lineloygon);
            map.Navigator.ZoomToBox(extent);
            map.Navigator.RotationLock = false;
         //   map.Navigator.CenterOnAndZoomTo(lineloygon.Extent!.Centroid, map.Navigator.Resolutions[15]);
            // mapControl.Renderer.WidgetRenders[typeof(CustomWidget)] = new CustomWidgetSkiaRenderer();

            mapControl.Renderer.StyleRenderers.Add(typeof(CustomStyle), new SkiaCustomStyleRenderer());

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
            map.Info += Map_Info;
            mapControl.FeatureInfo += MapControl_FeatureInfo;
         
           
            mapControl.Refresh();
          

        }

        

        private void MapControl_FeatureInfo(object? sender, FeatureInfoEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Map_Info(object? sender, MapInfoEventArgs e)
        {
            
        }
        //public static string ToStringOfKeyValuePairs(this IFeature feature)
        //{
        //    var stringBuilder = new StringBuilder();
        //    foreach (var field in feature.Fields)
        //        stringBuilder.Append($"{field}: {feature[field]}\n");
        //    return stringBuilder.ToString();
        //}
        private string AcText(IFeature? feature)
        {
            return "12345";
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