using BruTile;
using BruTile.Predefined;
using BruTile.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GisAvalonia.MapTools
{
    internal class HttpClientTileSource : ITileSource
    {
        private readonly HttpClient _HttpClient;
        private readonly HttpTileSource _WrappedSource;

        public HttpClientTileSource(HttpClient httpClient, ITileSchema tileSchema, string urlFormatter, IEnumerable<string> serverNodes = null, string apiKey = null, string name = null, BruTile.Cache.IPersistentCache<byte[]> persistentCache = null, Attribution attribution = null)
        {

            _HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            name = "TileSource";
            _WrappedSource = new HttpTileSource(GetTileSchema(), urlFormatter, serverNodes, apiKey, name, persistentCache, ClientFetch, attribution);
        }

        public ITileSchema Schema => _WrappedSource.Schema;

        public string Name => _WrappedSource.Name;

        public Attribution Attribution => _WrappedSource.Attribution;

        public static ITileSchema GetTileSchema()
        {
            var schema = new GlobalSphericalMercator(YAxis.OSM);
            schema.Resolutions.Clear();
            //schema.Resolutions[0] = new Resolution(0, 156543.033900000);
            //schema.Resolutions[1] = new Resolution(1, 78271.516950000);
            return schema;
        }

        public Task<byte[]?> GetTileAsync(TileInfo tileInfo)
        {
            return _WrappedSource.GetTileAsync(tileInfo);
        }

        private async Task<byte[]?> ClientFetch(Uri uri)
        {
            return await _HttpClient.GetByteArrayAsync(uri);
        }
    }
}
