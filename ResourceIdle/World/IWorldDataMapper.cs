using System.Collections.Generic;

namespace ResourceIdle.World;

public interface IWorldDataMapper
{
    public WorldTileData Map(WorldTileData tile, int x, int y);
}