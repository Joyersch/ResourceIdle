using Joyersch.Monogame;
using Joyersch.Monogame.Ui;
using Joyersch.Monogame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ResourceIdle.Menu;
using ResourceIdle.Menu.Backdrops;
using ResourceIdle.Menu.Buttons;

namespace ResourceIdle;

public sealed class Game : ExtendedGame
{
    ScaleDeviceHandler _scaleDeviceHandler;
    private bool _keyWasPressed;

    private MenuManager _menuManager;

    public Game()
    {
        IsConsoleEnabled = true;
        IsFixedTimeStep = false;
    }

    protected override void Initialize()
    {
        Graphics.PreferredBackBufferWidth = 1280;
        Graphics.PreferredBackBufferHeight = 720;
        Graphics.ApplyChanges();
        _scaleDeviceHandler = new ScaleDeviceHandler(Graphics, GraphicsAdapter.DefaultAdapter);
        _scaleDeviceHandler.ScaleToScreen();
        _scaleDeviceHandler.Fullscreen();

        base.Initialize();

        _menuManager = new MenuManager(Scene);
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        Cursor.Texture = Content.GetTexture("cursor");

        SettingsButton.Texture = Content.GetTexture("buttons/settings");

        SettingsBackdrop.Texture = Content.GetTexture("backdrops/settings");
    }

    protected override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _menuManager.Update(gameTime);
        }

        var keyboardState = Keyboard.GetState();
        var keyIsPressed = keyboardState[Keys.F10] == KeyState.Down;
        if (!_keyWasPressed && keyIsPressed)
        {
            ToggleConsole();
        }

        _keyWasPressed = keyIsPressed;
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _menuManager.Draw(SpriteBatch);

        base.Draw(gameTime);
        DrawConsole();
    }
}