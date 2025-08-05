using System.Collections.Generic;
using Joyersch.Monogame.Storage;

namespace ResourceIdle.World.Island0;

public sealed class Save : ISave
{
    public List<CaveData> CaveData { get; set; } = [];
    public void Reset()
    {
        CaveData = [];
    }
}