using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.World;

namespace ResourceIdle.Menu.Drawers;

public sealed class WorldTileDrawer : IDrawer, IMoveable
{
    private readonly Scene _scene;
    private WorldTileInfoBackdrop _backdrop;
    public Rectangle Rectangle => _backdrop.Rectangle;
    public Rectangle[] Hitbox => [Rectangle];

    private WorldTileData _worldTileData;
    public WorldTileDrawer(Scene scene)
    {
        _scene = scene;
        _backdrop = new WorldTileInfoBackdrop(scene.Display.Scale * 3);
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _backdrop.Draw(spriteBatch);
    }


    public bool UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        return false;
    }

    public void SetData(IDrawerData data)
    {
        _worldTileData = (WorldTileData)data;
    }

    public Vector2 GetPosition()
        => _backdrop.GetPosition();

    public Vector2 GetSize()
        => _backdrop.GetSize();

    public void Move(Vector2 newPosition)
    {
        _backdrop.Move(newPosition);
    }
}