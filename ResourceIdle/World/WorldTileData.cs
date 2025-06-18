using Microsoft.Xna.Framework;

namespace ResourceIdle.World;

public class WorldTileData
{
    public int Id { get; set; }

    public Vector2 Anchor { get; set; }

    public WorldTileType Type { get; set; }
}