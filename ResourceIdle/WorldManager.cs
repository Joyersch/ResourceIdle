using System;
using System.Collections.Generic;
using Joyersch.Monogame;
using Joyersch.Monogame.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.World;

namespace ResourceIdle;

public sealed class WorldManager : IManageable, IInteractable
{
    private readonly Scene _scene;
    public Rectangle Rectangle => Rectangle.Empty;

    private WorldTileSelect _tileSelect;
    private List<WorldTile> _tiles;
    private List<Cave> _caves;

    private PlayerData _playerData;
    private IWorldBackground _background;

    public Action<WorldTile> TriggeredMenu;

    public WorldManager(Scene scene, WorldSave save)
    {
        _scene = scene;
        _caves = new();
        _tiles = new List<WorldTile>();

        float tileScale = scene.Display.Scale * 4f * WorldTile.Single;
        Vector2 topLeft = scene.Camera.RealPosition;
        topLeft.X -= topLeft.X % tileScale;
        topLeft.Y += topLeft.Y % tileScale;

        _tileSelect = new WorldTileSelect(topLeft - new Vector2(tileScale), scene.Display.Scale * 4f);

        for (int y = 0; y < 12; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                var position = topLeft + new Vector2(x * tileScale, y * tileScale);
                var anchorLeft = topLeft + new Vector2(1 * tileScale, 1 * tileScale);
                var anchorRight = topLeft + new Vector2(11 * tileScale, 1 * tileScale);
                var tile = new WorldTile(
                    new WorldTileData() { Id = y * 20 + x, Anchor = x >= 10 ?  anchorLeft : anchorRight}, position,
                    scene.Display.Scale * 4f);
                tile.Clicked += sender =>
                {
                    var t = (WorldTile)sender;
                    _tileSelect.Move(t.Position);
                    TriggeredMenu?.Invoke(t);
                };
                _tiles.Add(tile);
            }
        }

        _background = new Island0(scene.Display.Scale * 4f);
        _background.Move(topLeft);

        LoadSave(save);
    }


    public void LoadSave(WorldSave save)
    {
        foreach (var cave in _caves)
        {
            cave.Dispose();
        }

        // clear old save!
        _caves.Clear();

        // load new save
        foreach (var data in save.CaveData)
        {
            int pos = (int)data.Position.X + (int)data.Position.Y * 20;
            var cave = SpawnCave(_tiles[pos], data);

            cave.InRectangle(_tiles[pos])
                .OnCenter()
                .Centered()
                .Apply();
        }

        Log.Information(save.CaveData.Count.ToString());

        _playerData = save.PlayerData;
    }

    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        foreach (var tile in _tiles)
            tile.UpdateInteraction(gameTime, toCheck);
    }

    public void Update(GameTime gameTime)
    {
        _tileSelect.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _background.Draw(spriteBatch);

        foreach (var cave in _caves)
        {
            if (_scene.Camera.Rectangle.Intersects(cave.Rectangle))
                cave.Draw(spriteBatch);
        }

        _tileSelect.Draw(spriteBatch);
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

    private void CaveClicked(Cave cave)
    {
        // ToDo: show cave menu instead
        cave.Data.Generated++;
        _playerData.Inventory[Resource.Rock]++;
    }
}