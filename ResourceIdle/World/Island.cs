using System;
using System.Collections.Generic;
using Joyersch.Monogame;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ResourceIdle.World;

public class Island<T> : IIsland where T : IIslandBackground
{
    private readonly Scene _scene;
    protected PlayerData PlayerData;
    protected T Background;
    protected List<WorldTile> Tiles;
    protected WorldTileSelect TileSelected;
    public Action<WorldTile> TriggeredMenu;


    public Rectangle Rectangle => Background.Rectangle;

    public Island(Scene scene, PlayerData playerData, T background)
    {
        Tiles = new List<WorldTile>();

        float tileScale = scene.Display.Scale * 4f * WorldTile.Single;
        Vector2 topLeft = scene.Camera.RealPosition;
        topLeft.X -= topLeft.X % tileScale;
        topLeft.Y += topLeft.Y % tileScale;

        TileSelected = new WorldTileSelect(topLeft - new Vector2(tileScale), scene.Display.Scale * 4f);

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
                    TileSelected.Move(t.Position);
                    TriggeredMenu?.Invoke(t);
                };
                Tiles.Add(tile);
            }
        }

        Background = background;
        Background.Move(topLeft);

        _scene = scene;
        PlayerData = playerData;
    }
    
    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        foreach (var tile in Tiles)
            tile.UpdateInteraction(gameTime, toCheck);
    }

    public virtual void LoadSave(ISave save)
    {
        throw new NotImplementedException();
    }

    public virtual void Update(GameTime gameTime)
    {
        TileSelected.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Background.Draw(spriteBatch);

        TileSelected.Draw(spriteBatch);
    }

    public Vector2 GetPosition()
        => Background.GetPosition();

    public Vector2 GetSize()
        => Background.GetSize();

    public void Move(Vector2 newPosition)
        => Background.Move(newPosition);
}