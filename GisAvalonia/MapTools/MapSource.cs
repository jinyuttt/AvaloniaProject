using BruTile.Predefined;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Styles;
using Mapsui.Tiling.Layers;
using Mapsui.UI.Avalonia;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace GisAvalonia.MapTools
{
    internal class MapSource
    {
        public MapSource(MapControl mapControl) {
            MapControlTool.mapControl = mapControl;
            MapControlTool.Init();
        }
        public ILayer Req()
        {
            var httpClient = new HttpClient();
             string url = "http://wprd01.is.autonavi.com/appmaptile?x={0}&y={1}&z={2}&lang=zh_cn&size=1&scl=1&style=7";
           // string url = "http://online3.map.bdimg.com/onlinelabel/?qt=tile&x={0}&y={1}&z={2}&styles=pl&udt=20200727&scaler=1&p=1";
            var osmSource = new HttpClientTileSource(httpClient, new GlobalSphericalMercator(), url);

            var osmLayer = new TileLayer(osmSource) { Name = "高德地图" };
            return osmLayer;
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
        public  void TestMap()
        {
            MapControlTool.AddLayer(Req());
        }

        public  void TestLine()
        {
            MapControlTool.AddLayer(CreateRandomPointLayer());
          
        }
    }
}
