using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using Joyersch.Monogame.Storage;

namespace ResourceIdle.World;

public class WorldSave : ISave
{
    public List<CaveData> CaveData { get; set; } = [];

    public PlayerData PlayerData { get; set; } = GenerateNewPlayerDataSet();

    public int WorldSeed { get; set; } = GenerateSeed();

    public void Reset()
    {
        PlayerData = GenerateNewPlayerDataSet();
        CaveData = [];
    }

    private static int GenerateSeed()
    {
        byte[] toHash = BitConverter.GetBytes(DateTime.Now.Ticks);
        byte[] result = [];
        using SHA256 sha = SHA256.Create();
        result = sha.ComputeHash(toHash);

        return BitConverter.ToInt32(result);
    }

    private static PlayerData GenerateNewPlayerDataSet()
    {
        var values = Enum.GetValues<Resource>();
        var inventory =
            values.ToDictionary<Resource, Resource, BigInteger>(value => value, value => 0);

        return new PlayerData() { Inventory = inventory };
    }
}