using Joyersch.Monogame;
using Joyersch.Monogame.Listener;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.Menu;
using ResourceIdle.Menu.Buttons;
using IDrawable = Joyersch.Monogame.IDrawable;
using IUpdateable = Joyersch.Monogame.IUpdateable;

namespace ResourceIdle;

public sealed class MenuManager : IUpdateable, IDrawable
{
    public Rectangle Rectangle => Rectangle.Empty;

    private readonly Scene _scene;
    private readonly Cursor _cursor;
    private readonly MousePointer _mousePointer;
    private readonly PositionListener _positionListener;

    private SettingsButton _settingsButton;
    private Settings _settings;

    public MenuManager(Scene scene)
    {
        _scene = scene;
        _cursor = new Cursor(scene.Display.Scale * 4);
        _mousePointer = new MousePointer(scene);
        _positionListener = new PositionListener();
        _positionListener.Add(_mousePointer, _cursor);

        _settingsButton = new SettingsButton(_scene.Display.Scale * 2);
        _settingsButton.Move(Vector2.One);
        _settingsButton.InRectangle(scene.Camera)
            .OnX(0.975f)
            .OnY(0.95f)
            .Centered()
            .Apply();

        _settingsButton.Click += _ => ShowSettings();

        _settings = new Settings(scene);
    }

    public void Update(GameTime gameTime)
    {
        _mousePointer.Update(gameTime);
        _cursor.Update(gameTime);
        _positionListener.Update(gameTime);

        _settingsButton.UpdateInteraction(gameTime, _cursor);
        _settingsButton.Update(gameTime);

        _settings.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
            transformMatrix: _scene.Camera.CameraMatrix);

        _settingsButton.Draw(spriteBatch);

        _settings.Draw(spriteBatch);

        _cursor.Draw(spriteBatch);

        spriteBatch.End();
    }

    private void ShowSettings()
    {
        Log.Information("Settings");
    }
}