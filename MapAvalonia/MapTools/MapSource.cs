using BruTile.Cache;
using BruTile.Predefined;
using BruTile.Web;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Styles;
using Mapsui.Tiling.Layers;
using Mapsui.UI.Avalonia;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MapAvalonia.MapTools
{
    internal class MapSource
    {
        public static IPersistentCache<byte[]>? DefaultCache;
        private static readonly BruTile.Attribution _openStreetMapAttribution = new(
        "© OpenStreetMap contributors", "https://www.openstreetmap.org/copyright");
      
        public static TileLayer CreateTileLayer(string? userAgent = null)
        {
            userAgent ??= $"user-agent-of-{Path.GetFileNameWithoutExtension(System.AppDomain.CurrentDomain.FriendlyName)}";

            return new TileLayer(CreateTileSource(userAgent)) { Name = "高德" };
        }
        private static HttpTileSource CreateTileSource(string userAgent)
        {
          
            string url = "http://wprd01.is.autonavi.com/appmaptile?x={0}&y={1}&z={2}&lang=zh_cn&size=1&scl=1&style=7";
            return new HttpTileSource(new GlobalSphericalMercator(),
                url,
                name: "高德",
                attribution: _openStreetMapAttribution, userAgent: userAgent, persistentCache: DefaultCache);
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
