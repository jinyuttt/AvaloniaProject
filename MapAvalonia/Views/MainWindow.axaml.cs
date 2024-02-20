using Avalonia.Controls;
using MapAvalonia.MapTools;
using MapAvalonia.Models;
using Mapsui.Projections;
using Mapsui;
using Mapsui.UI.Avalonia;
using Mapsui.UI;
using Mapsui.Extensions;

namespace MapAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {


            mapControl.Map.CRS = "EPSG:3857";
            mapControl.Map.Layers.Add(MapSource.CreateTileLayer());
            mapControl.Map.Navigator.RotationLock = false;
            mapControl.Renderer.WidgetRenders[typeof(CustomWidget)] = new CustomWidgetSkiaRenderer();
            var extent = mapControl.Map.Layers[0].Extent!.Grow(mapControl.Map.Layers[0].Extent!.Width * 0.1);
            mapControl.Map.Navigator.ZoomToBox(extent);
           
            //
           // var point = SphericalMercator.FromLonLat( 25.04505, 102.70734);
          //  mapControl.Map.Navigator.CenterOnAndZoomTo(point.ToMPoint(), 8);
            //var point = SphericalMercator.FromLonLat(15.01917, 46.58806);

            //map.Home = n => n.NavigateTo(new MPoint(point.x, point.y), map.Resolutions[12]);  //0 zoomed out-19 zoomed in

            //var line = new Mapsui.UI.Maui.Polyline { StrokeWidth = 150, StrokeColor = Mapsui.UI.Maui.KnownColor.Red, IsClickable = true };
            //line.Positions.Add(new Position(point.x, point.y));
            //line.Positions.Add(new Position(point.x + 1, point.y - 1));.Home = n => n.NavigateTo(new MPoint(point.x, point.y), map.Resolutions[12]);  //0 zoomed out-19 zoomed in

            //var line = new Mapsui.UI.Maui.Polyline { StrokeWidth = 150, StrokeColor = Mapsui.UI.Maui.KnownColor.Red, IsClickable = true };
            //line.Positions.Add(new Position(point.x, point.y));
            //line.Positions.Add(new Position(point.x + 1, point.y - 1));
            //

            mapControl.Refresh();
          

        }
    }
}