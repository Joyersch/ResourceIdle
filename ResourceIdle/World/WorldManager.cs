using System;
using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.Menu;

namespace ResourceIdle.World;

public sealed class WorldManager : IManageable
{
    private readonly Scene _scene;
    private readonly MenuManager _menuManager;
    public Rectangle Rectangle => Rectangle.Empty;
    public Action<WorldTile> TriggeredMenu;
    private Island _island;
    private IslandFactory _islandFactory;
    private IslandWrapper _islandWrapper;
    private WorldSave _save;

    public WorldManager(Scene scene, InteractHandler interactHandler, MenuManager menuManager, WorldSave save)
    {
        _scene = scene;
        _menuManager = menuManager;
        _save = save;
        _islandFactory = new IslandFactory(scene, save);

        FastNoise.SetNoise(save.WorldSeed);

        _islandWrapper = new IslandWrapper();
        interactHandler.AddInteractable(_islandWrapper, -1000);
        LoadIsland(_save.SelectedIsland);
        LoadSave(save);
    }

    private void LoadIsland(int number)
    {
        _island = _islandFactory.GetIsland(number);

        _island.TileClicked += tile =>
        {
            _menuManager.ShowDrawer(DrawersEnum.TileInfo, tile.Data);
        };

        _islandWrapper.SetIsland(_island);
    }

    public void LoadSave(WorldSave save)
    {
        _save = save;
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