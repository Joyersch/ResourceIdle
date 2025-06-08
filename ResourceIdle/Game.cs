using Joyersch.Monogame;
using Joyersch.Monogame.Listener;
using Joyersch.Monogame.Ui;
using Joyersch.Monogame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ResourceIdle.Menu;
using ResourceIdle.Menu.Backdrops;
using ResourceIdle.Menu.Buttons;
using ResourceIdle.World;

namespace ResourceIdle;

public sealed class Game : ExtendedGame
{
    ScaleDeviceHandler _scaleDeviceHandler;
    private bool _keyWasPressed;

    private MenuManager _menuManager;
    private WorldManager _worldManager;

    private Cursor _cursor;
    private MousePointer _mousePointer;
    private PositionListener _positionListener;

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
        _worldManager = new WorldManager(Scene, _menuManager);

        _cursor = new Cursor(Scene.Display.Scale * 4);
        _mousePointer = new MousePointer(Scene);
        _positionListener = new PositionListener();
        _positionListener.Add(_mousePointer, _cursor);
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        Cursor.Texture = Content.GetTexture("cursor");

        SettingsButton.Texture = Content.GetTexture("buttons/settings");

        SettingsBackdrop.Texture = Content.GetTexture("backdrops/settings");

        Cave.Texture = Content.GetTexture("world/cave");
    }

    protected override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _mousePointer.Update(gameTime);
            _cursor.Update(gameTime);
            _positionListener.Update(gameTime);

            _worldManager.UpdateInteraction(gameTime, _cursor);
            _worldManager.Update(gameTime);
            _menuManager.UpdateInteraction(gameTime, _cursor);
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

        SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
            transformMatrix: Scene.Camera.CameraMatrix);

        _worldManager.Draw(SpriteBatch);
        _menuManager.Draw(SpriteBatch);
        _cursor.Draw(SpriteBatch);

        SpriteBatch.End();

        base.Draw(gameTime);
        DrawConsole();
    }
}