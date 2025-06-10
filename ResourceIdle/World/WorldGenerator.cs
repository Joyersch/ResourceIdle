using Microsoft.Xna.Framework;

namespace ResourceIdle.World;

public class WorldGenerator
{
    private readonly WorldSave _save;
    private readonly FastNoiseLite _noise;

    public WorldGenerator(WorldSave save)
    {
        _save = save;

        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        noise.SetSeed(_save.WorldSeed);

        _noise = noise;
    }

    public float Generate(Vector2 position)
    {
        float x = (position.X - position.X % 16) / 16;
        float y = (position.Y - position.Y % 16) / 16;

        return _noise.GetNoise(x, y);
    }
}