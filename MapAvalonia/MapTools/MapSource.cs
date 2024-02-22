using Avalonia.Platform;
using BruTile;
using BruTile.Cache;
using BruTile.Predefined;
using BruTile.Web;
using BruTile.Wmts;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Providers;
using Mapsui.Samples.Common.DataBuilders;
using Mapsui.Styles;
using Mapsui.Tiling;
using Mapsui.Tiling.Extensions;
using Mapsui.Tiling.Fetcher;
using Mapsui.Tiling.Layers;
using Mapsui.Tiling.Provider;
using Mapsui.Tiling.Rendering;
using Mapsui.UI.Avalonia;
using Mapsui.Widgets;
using NetTopologySuite.Shape.Random;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MapAvalonia.MapTools
{
    internal class MapSource
    {
        static readonly string UrlFormat = "http://webrd01.is.autonavi.com/appmaptile?lang=zh_cn&size=1&scale=1&style=7&x={0}&y={1}&z={2}";
        public static IPersistentCache<byte[]>? DefaultCache;
        private static readonly BruTile.Attribution _openStreetMapAttribution = new(
        "© OpenStreetMap contributors", "https://www.openstreetmap.org/copyright");
       static ITileSource _tileSource = null;
        public static TileLayer CreateTileLayer(string? userAgent = null)
        {
            userAgent ??= $"user-agent-of-{Path.GetFileNameWithoutExtension(System.AppDomain.CurrentDomain.FriendlyName)}";

            _tileSource = CreateTileSource(userAgent);
          
            return new TileLayer(_tileSource, 200,500,new MinimalDataFetchStrategy(),new MinimalRenderFetchStrategy()) { Name = "高德",   IsMapInfoLayer=true  };
        }
        public static async Task<ILayer> CreateLayerAsync()
        {
            string url = "http://t0.tianditu.com/DataServer?T=cva_w&x={0}&y={1}&l={2}";//矢量道路图
            var handler = new HttpClientHandler();
            using var httpClient = new HttpClient(handler);
            // When testing today (20-10-2021) tile 0,0,0 returned a 500. Perhaps this should be fixed in the xml.
            using var response = await httpClient.GetStreamAsync(url).ConfigureAwait(false);
            var tileSource = WmtsParser.Parse(response).First();


            return new TileLayer(tileSource) { Name = tileSource.Name };
        }
        public static ILayer CreateSTileLayer(string? userAgent = null)
        {
            userAgent ??= $"user-agent-of-{Path.GetFileNameWithoutExtension(System.AppDomain.CurrentDomain.FriendlyName)}";

            _tileSource = CreateTileSource(userAgent);
         
           // TileLayer tileLayer = new TileLayer(_tileSource);
         //   RasterizingTileLayer tileProvider = new RasterizingTileLayer(tileLayer,projection: ProjectionDefaults.Projection) { };

            TileProvider provider = new TileProvider(_tileSource) {  CRS= "EPSG:3857" };
            //var dataSource = new ProjectingProvider(provider, ProjectionDefaults.Projection)
            //{
            //    CRS = "EPSG:3857"
            //};

          //  return tileProvider;
            return new Layer
            {
                DataSource = provider,
                Name = "Cities",
                Style = CreateCityStyle(),
               
            };
        }
       

        private static HttpTileSource CreateTileSource(string userAgent)
        {

            
            //  string url = "https://server.arcgisonline.com/arcgis/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}";//arcgis影像
           
            string url = "https://rt2.map.gtimg.com/realtimerender?z={z}&x={x}&y={y}&type=vector&style=0";//腾讯
            url = "https://rt2.map.gtimg.com/tile?z={z}&x={x}&y={y}&type=vector&styleid=3&version=376";//腾讯
            url = "https://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineStreetPurplishBlue/MapServer/tile/{z}/{y}/{x}"; //ArcGIS Server发布的WMTS地图服务
            return new HttpTileSource(new GlobalSphericalMercator() { Srs= "EPSG:3857" },
                url,
                name: "高德",
                attribution: _openStreetMapAttribution, userAgent: userAgent, persistentCache: DefaultCache);
        }
        public static Layer CreateWorldCitiesLayer()
        {

            var features = WorldCitiesFeaturesBuilder.CreateTop100Cities();
            var memoryProvider = new MemoryProvider(features)
            {
                CRS = "EPSG:4326" // The DataSource CRS needs to be set
            };

            var dataSource = new ProjectingProvider(memoryProvider)
            {
                CRS = "EPSG:3857"
            };

            return new Layer
            {
                DataSource = dataSource,
                Name = "Cities",
                Style = CreateCityStyle(),
                IsMapInfoLayer = true
            };
        }

       

       

        private static IStyle SmalleDot()
        {
            return new SymbolStyle { SymbolScale = 0.2, Fill = new Brush(new Color(40, 40, 40)) };
        }
        private static SymbolStyle CreateCityStyle()
        {

            // var location = typeof(GeodanOfficesLayerBuilder).LoadBitmapId("Images.location.png");


            return new SymbolStyle
            {
                //  BitmapId = location,
                SymbolOffset = new Offset { Y = 64 },
                SymbolScale = 0.25,
                Opacity = 0.5f,
                Fill = new Brush(new Color(200, 40, 40))
            };
        }
        private  MemoryLayer CreateRandomPointLayer()
        {
            var rnd = new Random(3462); // Fix the random seed so the features don't move after a refresh
            var observableCollection = new ObservableCollection<MPoint>();

            var layer = new ObservableMemoryLayer<MPoint>(f => new PointFeature(f))
            {
                Name = "Points",
                Style = new SymbolStyle
                {
                    SymbolType = SymbolType.Triangle,
                    Fill = new Brush(Color.Red)
                },
                ObservableCollection = observableCollection,
            };

            _ = Task.Run(async () =>
            {
                for (var i = 0; i < 100; i++)
                {
                    observableCollection.Add(new MPoint(rnd.Next(0, 5000000), rnd.Next(0, 5000000)));
                    await Task.Delay(100);
                }
            });

            return layer;
        }


       
      

    }
}
