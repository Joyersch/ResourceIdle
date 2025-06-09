using Joyersch.Monogame;
using Joyersch.Monogame.Listener;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.Menu;
using ResourceIdle.Menu.Buttons;
using ResourceIdle.World;
using IDrawable = Joyersch.Monogame.IDrawable;
using IUpdateable = Joyersch.Monogame.IUpdateable;

namespace ResourceIdle;

public sealed class MenuManager : IUpdateable, IInteractable, IDrawable
{
    public Rectangle Rectangle => Rectangle.Empty;

    private readonly Scene _scene;

    private SettingsButton _settingsButton;
    private Settings _settings;
    private bool _onSettings;

    public MenuManager(Scene scene)
    {
        _scene = scene;

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

    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        _settingsButton.UpdateInteraction(gameTime, toCheck);
    }

    public void Update(GameTime gameTime)
    {
        _settingsButton.Update(gameTime);
        if (_onSettings)
            _settings.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _settingsButton.Draw(spriteBatch);

        if (_onSettings)
            _settings.Draw(spriteBatch);
    }

    private void ShowSettings()
    {
        Log.Information("Settings");
        _onSettings = !_onSettings;
    }

    public void HandleMenuEvent(WorldMenuElement @event, object sender)
    {
        switch (@event)
        {
            case WorldMenuElement.Cave:
                ToggleCaveView((Cave)sender);
                break;
            default:
                Log.Error($"No Handling for the given event {@event}");
                break;
        }
    }

    private void ToggleCaveView(Cave cave)
    {
        Log.Write(cave.Data.Generated.ToString());
    }
}