using System.Collections.Generic;
using Joyersch.Monogame.Storage;

namespace ResourceIdle.World;

public class WorldSave : ISave
{
    public List<CaveData> CaveData { get; set; } = [];

    public void Reset()
    {
        CaveData = new();
    }
}