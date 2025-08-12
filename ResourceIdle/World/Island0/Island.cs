using System.Collections.Generic;
using System.Linq;
using Joyersch.Monogame;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using BaseIsland = ResourceIdle.World.Island;

namespace ResourceIdle.World.Island0;

public sealed class Island : BaseIsland
{
    private readonly Scene _scene;
    private readonly PlayerData _playerData;
    private List<Cave> _caves;

    public Island(Scene scene, WorldSave worldSave) : base(scene, worldSave.PlayerData,
        new Background(scene.Display.Scale * 4f), new DataMapper(), new ResourceMapper())
    {
        _scene = scene;
        _playerData = worldSave.PlayerData;

        _caves = new();
        TileClicked += tile =>
        {
            Log.Information(tile.Data.Type.ToString());
            foreach (var pair in tile.Data.Resources.ToArray())
            {
                Log.Information(pair.Key + "->" + pair.Value);
            }
        };
    }

    public override void LoadSave(ISave save)
    {
        var s = (Save)save;
        foreach (var cave in _caves)
        {
            cave.Dispose();
        }

        // clear old save!
        _caves.Clear();

        // load new save
        foreach (var data in s.CaveData)
        {
            int pos = (int)data.Position.X + (int)data.Position.Y * 20;
            var cave = SpawnCave(Tiles[pos], data);

            cave.InRectangle(Tiles[pos])
                .OnCenter()
                .Centered()
                .Apply();
        }
    }

    public Cave SpawnCave(WorldTile tile, CaveData caveData = null, Vector2? position = null)
    {
        var data = caveData ?? new CaveData();
        if (position.HasValue)
            data.Position = position!.Value;

        var cave = new Cave(data, tile, _scene.Display.Scale * 6);
        cave.Clicked += CaveClicked;
        _caves.Add(cave);
        return cave;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Background.Draw(spriteBatch);

        foreach (var cave in _caves)
        {
            if (_scene.Camera.Rectangle.Intersects(cave.Rectangle))
                cave.Draw(spriteBatch);
        }

        TileSelected.Draw(spriteBatch);
    }

    private void CaveClicked(Cave cave)
    {
        // ToDo: show cave menu instead
        cave.Data.Generated++;
        // _playerData.Inventory[Resource.Rock]++;
    }
}