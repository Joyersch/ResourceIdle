using System.Collections.Generic;
using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.World;

namespace ResourceIdle;

public sealed class WorldManager : IManageable, IInteractable
{
    private readonly Scene _scene;
    private readonly MenuManager _menuManager;
    public Rectangle Rectangle => Rectangle.Empty;
    private List<Cave> _caves;

    public WorldManager(Scene scene, MenuManager menuManager)
    {
        _scene = scene;
        _menuManager = menuManager;
        _caves = new();
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

    public void SpawnCave(CaveData caveData = null, Vector2? position = null)
    {
        var data = caveData ?? new CaveData() { Id = "test_id" };
        if (position.HasValue)
            data.Position = position!.Value;

        var cave = new Cave(data, _scene.Display.Scale * 4);
        cave.Clicked += cave => { _menuManager.ToggleCaveView(cave); };
        _caves.Add(cave);
    }
}