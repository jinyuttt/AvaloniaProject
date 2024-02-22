using Avalonia.Controls;
using MapAvalonia.MapTools;
using Mapsui;
using Mapsui.Logging;
using Mapsui.Projections;
using Mapsui.Samples.CustomWidget;
using Mapsui.Styles;
using Mapsui.Utilities;
using Mapsui.Widgets.Zoom;

namespace GisAvalonia.Views
{
    public partial class MonitorPage : UserControl
    {
      
        public MonitorPage()
        {
            InitializeComponent();

            this.DataContext = new ViewModels.MonitorViewModel();

            Init();

        }



        private async void Init()
        {
            using var geometryLayer = MapSource.CreateWorldCitiesLayer();
            var extent = geometryLayer.Extent!.Grow(10000);

            var _titlelayer = MapSource.CreateTileLayer();

            // var titlelayer = await MapSource.CreateLayerAsync();
            var map = new Map
            {

                CRS = "EPSG:3857", // The Map CRS needs to be set
                BackColor = Color.Gray
            };
            mapControl.Map = map;


            map.Widgets.Enqueue(new ZoomInOutWidget { MarginX = 20, MarginY = 40 });
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

    }
}
