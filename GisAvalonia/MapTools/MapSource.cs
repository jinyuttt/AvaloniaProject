using BruTile;
using BruTile.Cache;
using BruTile.Predefined;
using BruTile.Web;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Providers;
using Mapsui.Samples.Common.DataBuilders;
using Mapsui.Styles;
using Mapsui.Tiling.Fetcher;
using Mapsui.Tiling.Layers;
using Mapsui.Tiling.Rendering;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace MapAvalonia.MapTools
{
    internal class MapSource
    {
       
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
       
       
       

        private static HttpTileSource CreateTileSource(string userAgent)
        {

            
            //  string url = "https://server.arcgisonline.com/arcgis/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}";//arcgis影像
           
            string url = "https://rt2.map.gtimg.com/realtimerender?z={z}&x={x}&y={y}&type=vector&style=0";//腾讯
           // url = "https://rt2.map.gtimg.com/tile?z={z}&x={x}&y={y}&type=vector&styleid=3&version=376";//腾讯底图
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
