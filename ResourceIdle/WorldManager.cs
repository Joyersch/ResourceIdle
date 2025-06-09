using System;
using System.Collections.Generic;
using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.World;

namespace ResourceIdle;

public sealed class WorldManager : IManageable, IInteractable
{
    private readonly Scene _scene;
    public Rectangle Rectangle => Rectangle.Empty;
    private List<Cave> _caves;

    private PlayerData _playerData;

    public Action<WorldMenuElement, object> MenuEvent;

    public WorldManager(Scene scene, WorldSave save)
    {
        _scene = scene;
        _caves = new();

        LoadSave(save);
    }

    public void LoadSave(WorldSave save)
    {
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

        MenuEvent?.Invoke(WorldMenuElement.Cave, cave);
    }
}