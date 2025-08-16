using Joyersch.Monogame;
using Joyersch.Monogame.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ResourceIdle.Menu.Drawers;

public sealed class SettingsDrawer : IDrawer
{
    private SettingsBackdrop _backdrop;

    public Rectangle Rectangle => _backdrop.Rectangle;

    public Rectangle[] Hitbox => [_backdrop.Rectangle];

    public SettingsDrawer(Scene scene)
    {
        _backdrop = new SettingsBackdrop(scene.Display.Scale * 3);
        _backdrop.InRectangle(scene.Camera)
            .OnCenter()
            .Centered()
            .Apply();
    }

    public bool UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        return false;
    }

    public void SetData(IDrawerData data)
    {
        Log.Information("No data set for settings drawer!");
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _backdrop.Draw(spriteBatch);
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