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

    private List<WorldTile> _background;
    private List<Cave> _caves;

    private float _tileScale;

    private PlayerData _playerData;

    public Action<WorldMenuElement, object> TriggeredMenu;

    public WorldManager(Scene scene, WorldSave save)
    {
        _scene = scene;
        _caves = new();
        _background = new List<WorldTile>();
        _tileScale = scene.Display.Scale * 4f * WorldTile.Single;
        Vector2 topLeft = scene.Camera.RealPosition;
        topLeft.X -= topLeft.X % _tileScale;
        topLeft.Y += topLeft.Y % _tileScale;

        for (int y = 0; y < 12; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                var position = topLeft + new Vector2(x * _tileScale, y * _tileScale);
                _background.Add(new WorldTile(scene, position,
                    scene.Display.Scale * 4f, MapToLevel(position, 450, 900)));
            }
        }

        LoadSave(save);
    }

    private static int MapToLevel(Vector2 v, float minDistance, float maxDistance)
    {
        float distance = MathF.Sqrt(v.X * v.X * 0.35f + v.Y * v.Y * 0.65f);

        if (distance <= minDistance)
            return 0;
        if (distance >= maxDistance)
            return 3;

        float t = (distance - minDistance) / (maxDistance - minDistance);
        return (int)(t * 3);
    }

    public void LoadSave(WorldSave save)
    {
        // clear old save!
        _caves.Clear();

        // load new save
        foreach (var data in save.CaveData)
            SpawnCave(data);

        var size = new Vector2(_tileScale).ToPoint();
        foreach (var cave in _caves)
        {
            int pos = (int)cave.Data.Position.X + (int)cave.Data.Position.Y * 20;

            cave.InRectangle(_background[pos])
                .OnCenter()
                .Centered()
                .Apply();
        }

        _playerData = save.PlayerData;
    }

    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        foreach (var cave in _caves)
            cave.UpdateInteraction(gameTime, toCheck);

        foreach (var tile in _background)
            tile.UpdateInteraction(gameTime, toCheck);
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var tile in _background)
        {
            if (_scene.Camera.Rectangle.Intersects(tile.Rectangle))
                tile.Draw(spriteBatch);
        }

        foreach (var cave in _caves)
        {
            if (_scene.Camera.Rectangle.Intersects(cave.Rectangle))
                cave.Draw(spriteBatch);
        }
    }

    public Cave SpawnCave(CaveData caveData = null, Vector2? position = null)
    {
        var data = caveData ?? new CaveData();
        if (position.HasValue)
            data.Position = position!.Value;

        var cave = new Cave(data, _scene.Display.Scale * 6);
        cave.Clicked += CaveClicked;
        _caves.Add(cave);
        return cave;
    }

    private void CaveClicked(Cave cave)
    {
        cave.Data.Generated++;
        _playerData.Inventory[Resource.Rock]++;

        TriggeredMenu?.Invoke(WorldMenuElement.Cave, cave);
    }
}