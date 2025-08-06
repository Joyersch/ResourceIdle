using System.Collections.Generic;

namespace ResourceIdle.World;

public interface IResourceMapper
{
    public WorldTileData Map(WorldTileData data);
}