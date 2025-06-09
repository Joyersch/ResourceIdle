using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Joyersch.Monogame.Storage;

namespace ResourceIdle.World;

public class WorldSave : ISave
{
    public List<CaveData> CaveData { get; set; } = [];

    public PlayerData PlayerData { get; set; } = GenerateNewPlayerDataSet();

    public void Reset()
    {
        PlayerData = GenerateNewPlayerDataSet();
        CaveData = [];
    }

    private static PlayerData GenerateNewPlayerDataSet()
    {
        var values = Enum.GetValues<Resource>();
        var inventory =
            values.ToDictionary<Resource, Resource, BigInteger>(value => value, value => 0);

        return new PlayerData() { Inventory = inventory };
    }
}