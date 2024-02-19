using Avalonia.Controls;
using BruTile.Predefined;
using GisAvalonia.MapTools;
using Mapsui.Samples.CustomWidget;
using Mapsui.Tiling.Layers;
using Mapsui.UI.Avalonia;
using Mapsui.Widgets.ScaleBar;
using System.Net.Http;

namespace GisAvalonia.Views
{
    public partial class MonitorPage : UserControl
    {
        private const string uul = "http://developer.baidu.com/map/jsdemo.htm#a1_2";
        static readonly string UrlFormat = "http://webrd01.is.autonavi.com/appmaptile?lang=zh_cn&size=1&scale=1&style=7&x={0}&y={1}&z={2}";
        public MonitorPage()
        {
            InitializeComponent();

            this.DataContext = new ViewModels.MonitorViewModel();
            //  Init();
            var httpClient = new HttpClient();
            string url = "http://wprd01.is.autonavi.com/appmaptile?x={0}&y={1}&z={2}&lang=zh_cn&size=1&scl=1&style=7";
            // string url = "http://online3.map.bdimg.com/onlinelabel/?qt=tile&x={0}&y={1}&z={2}&styles=pl&udt=20200727&scaler=1&p=1";
          
            var osmSource = new HttpClientTileSource(httpClient, new GlobalSphericalMercator(), UrlFormat);

            var osmLayer = new TileLayer(osmSource) { Name = "¸ßµÂµØÍ¼" };
            map.Map.Widgets.Enqueue(new ScaleBarWidget(map.Map) { TextAlignment = Mapsui.Widgets.Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top });
            map.Map.Widgets.Enqueue(new Mapsui.Widgets.Zoom.ZoomInOutWidget { MarginX = 20, MarginY = 40 });
            map.Map.Navigator.RotationLock = false;
            map.Renderer.WidgetRenders[typeof(CustomWidget)] = new CustomWidgetSkiaRenderer();
            map.Map.Layers.Add(osmLayer);
            map.Map.Refresh();

        }

      
       
        private void Init()
        {
            var cur = map;
            var source=new  MapSource(cur);
            source.TestMap();
            source.TestLine();

        }
    }
}
