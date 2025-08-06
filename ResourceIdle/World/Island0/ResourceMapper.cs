namespace ResourceIdle.World.Island0;

public sealed class ResourceMapper : IResourceMapper
{
    private readonly int _seed;

    public ResourceMapper(int seed)
    {
        _seed = seed;
    }

    public WorldTileData Map(WorldTileData data)
    {
        float value = (FastNoise.Noise.GetNoise(data.Position.X, data.Position.Y) + 1) * 0.5f;
        switch (data.Type)
        {
            case WorldTileType.Water:
                data.Resources[Resource.Fish] = value;
                data.Resources[Resource.Water] = 1f - value;
                break;
            case WorldTileType.Land:
                data.Resources[Resource.Flowers] = value;
                data.Resources[Resource.Nothing] = 1f - value;
                break;
        }
        return data;
    }
}