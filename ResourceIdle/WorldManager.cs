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




    private PlayerData _playerData;
    public Action<WorldTile> TriggeredMenu;
    private IIsland _island;
    private IslandFactory _islandFactory;



    public WorldManager(Scene scene, WorldSave save)
    {
        _scene = scene;

        _islandFactory = new IslandFactory(scene, save.PlayerData);
        _island = _islandFactory.GetIsland(0);

        LoadSave(save);
    }


    public void LoadSave(WorldSave save)
    {
        _playerData = save.PlayerData;
    }

    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        _island.UpdateInteraction(gameTime, toCheck);
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