using System;
using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.Menu.Backdrops;

namespace ResourceIdle.Menu;

public sealed class Settings : IManageable
{
    public Rectangle Rectangle => _backdrop.Rectangle;

    private SettingsBackdrop _backdrop;

    public event Action<SettingsElement> SettingsChange;

    public Settings(Scene scene)
    {
        _backdrop = new SettingsBackdrop(scene.Display.Scale * 3);
        _backdrop.InRectangle(scene.Camera)
            .OnCenter()
            .Centered()
            .Apply();
    }

    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _backdrop.Draw(spriteBatch);
    }
}