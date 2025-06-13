

using System;
using Joyersch.Monogame.Logging;
using Microsoft.Xna.Framework;

namespace ResourceIdle.World;

public sealed class WorldState
{
    public int Level { get; set; }
    private readonly WorldSave _worldSave;
    private readonly float _scale;
    private readonly FastNoiseLite _noise;

    public WorldState(WorldSave worldSave, float scale)
    {
        _worldSave = worldSave;
        _scale = scale;

        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        noise.SetFrequency(100);
        noise.SetSeed(worldSave.WorldSeed);

        _noise = noise;
    }

    public int MapToLevel(Vector2 v)
    {
        float distance = MathF.Sqrt(v.X * v.X * 0.35f + v.Y * v.Y * 0.65f);

        Vector2 lowered = v / _scale;
        float x = (lowered.X - lowered.X % 16) / 16;
        float y = (lowered.Y - lowered.Y % 16) / 16;

        float r = _noise.GetNoise(x, y) * 1000;
        Log.Information(r.ToString());

        float min = 120 + (Level % 3 * 120) - r * 5;
        float max = 600 + (Level % 3 * 120) + r * 7;

        if (distance <= min)
            return 0;
        if (distance >= max)
            return 3;

        float t = (distance - min) / (max - min);
        return (int)(t * 3);
    }
}