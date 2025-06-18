using System;
using Joyersch.Monogame;
using Joyersch.Monogame.Listener;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.Menu;
using ResourceIdle.Menu.Backdrops;
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

    public event Action<SettingsElement> SettingsChange;

    private WorldTileInfo _worldTileInfo;

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
        _settings.SettingsChange += settings =>
        {
            SettingsChange?.Invoke(settings);
        };

        _worldTileInfo = new WorldTileInfo(_scene.Display.Scale * 4f);
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
        
        _worldTileInfo.Draw(spriteBatch);

        if (_onSettings)
            _settings.Draw(spriteBatch);
    }

    private void ShowSettings()
    {
        Log.Information("Settings");
        _onSettings = !_onSettings;
    }

    public void HandleMenu(WorldTile tile)
    {
        _worldTileInfo.Select(tile.Data);
        _worldTileInfo.Show();
    }

    private void ToggleCaveView(Cave cave)
    {
        Log.Write(cave.Data.Generated.ToString());
    }


}