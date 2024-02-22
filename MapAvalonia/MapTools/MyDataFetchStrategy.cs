using BruTile;
using Mapsui.Tiling.Fetcher;
using Mapsui.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapAvalonia.MapTools
{
    internal class MyDataFetchStrategy : IDataFetchStrategy
    {
        public IList<TileInfo> Get(ITileSchema schema, Extent extent, int level)
        {
            schema.GetTileInfos(extent, level);
            return (from t in schema.GetTileInfos(extent, level)
                    orderby Algorithms.Distance(extent.CenterX, extent.CenterY, t.Extent.CenterX, t.Extent.CenterY)
                    select t).ToList();
        }
    }
}
