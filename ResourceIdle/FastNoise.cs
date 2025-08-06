namespace ResourceIdle;

public static class FastNoise
{
    public static FastNoiseLite Noise { get; private set; }

    public static void SetNoise(int seed)
    {
        var noise = new FastNoiseLite(seed);
        noise.SetNoiseType(FastNoiseLite.NoiseType.Cellular);

        noise.SetFrequency(0.200f);
        noise.SetCellularDistanceFunction(FastNoiseLite.CellularDistanceFunction.Hybrid);
        noise.SetCellularReturnType(FastNoiseLite.CellularReturnType.CellValue);
        noise.SetCellularJitter(1.88f);

        noise.SetDomainWarpType(FastNoiseLite.DomainWarpType.OpenSimplex2);
        noise.SetDomainWarpAmp(0f);

        noise.SetFractalType(FastNoiseLite.FractalType.FBm);
        Noise = noise;
    }
}