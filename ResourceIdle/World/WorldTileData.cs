using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ResourceIdle.World;

public sealed class WorldTileData
{
    public int Id { get; set; }

    public Vector2 Anchor { get; set; }

    public Point Position { get; set; }

    public WorldTileType Type { get; set; }

    public Dictionary<Resource, float> Resources { get; set; } = [];
}