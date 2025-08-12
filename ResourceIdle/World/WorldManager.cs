using System;
using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ResourceIdle.World;

public sealed class WorldManager : IManageable
{
    private readonly Scene _scene;
    public Rectangle Rectangle => Rectangle.Empty;
    private PlayerData _playerData;
    public Action<WorldTile> TriggeredMenu;
    private IIsland _island;
    private IslandFactory _islandFactory;
    private IslandWrapper _islandWrapper;
    private InteractHandler _interactHandler;

    public WorldManager(Scene scene, InteractHandler interactHandler, WorldSave save)
    {
        _scene = scene;

        FastNoise.SetNoise(save.WorldSeed);
        _islandFactory = new IslandFactory(scene, save);
        _island = _islandFactory.GetIsland(save.SelectedIsland);
        _islandWrapper = new IslandWrapper(_island);

        _interactHandler = interactHandler;
        _interactHandler.AddInteractable(_islandWrapper, -1000);
        LoadSave(save);
    }

    public void LoadSave(WorldSave save)
    {
        _playerData = save.PlayerData;
    }

    public void Update(GameTime gameTime)
    {
        _island.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _island.Draw(spriteBatch);
    }
}