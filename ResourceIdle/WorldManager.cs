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

    private Background _background;
    private List<Cave> _caves;

    private PlayerData _playerData;

    public Action<WorldMenuElement, object> TriggeredMenu;

    public WorldManager(Scene scene, WorldSave save)
    {
        _scene = scene;
        _caves = new();
        _background = new Background(scene, 8f);
Log.Information(scene.Camera.Rectangle.Width.ToString());
        LoadSave(save);
    }

    public void LoadSave(WorldSave save)
    {
        // clear old save!
        _caves.Clear();

        // load new save
        foreach (var data in save.CaveData)
            SpawnCave(data);

        _playerData = save.PlayerData;
    }

    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        foreach (var cave in _caves)
            cave.UpdateInteraction(gameTime, toCheck);
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _background.Draw(spriteBatch);
        foreach (var cave in _caves)
            cave.Draw(spriteBatch);
    }

    public Cave SpawnCave(CaveData caveData = null, Vector2? position = null)
    {
        var data = caveData ?? new CaveData();
        if (position.HasValue)
            data.Position = position!.Value;

        var cave = new Cave(data, _scene.Display.Scale * 4);
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