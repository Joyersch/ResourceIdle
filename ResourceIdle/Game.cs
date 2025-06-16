using System;
using Joyersch.Monogame;
using Joyersch.Monogame.Listener;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Storage;
using Joyersch.Monogame.Ui;
using Joyersch.Monogame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ResourceIdle.Menu.Backdrops;
using ResourceIdle.Menu.Buttons;
using ResourceIdle.World;

namespace ResourceIdle;

public sealed class Game : ExtendedGame
{
    private bool _keyWasPressed;

    private ScaleDeviceHandler _scaleDeviceHandler;
    private MenuManager _menuManager;
    private WorldManager _worldManager;
    private SettingsManager _settingsManager;

    private Cursor _cursor;
    private MousePointer _mousePointer;
    private PositionListener _positionListener;

    public Game()
    {
        IsConsoleEnabled = true;
        IsFixedTimeStep = false;
        SaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                        "/Joyersch/ResourceIdle/";
        SaveFile = "debug";
        SavePrefix = "";
        SaveType = "idle";
    }

    protected override void Initialize()
    {
        Graphics.PreferredBackBufferWidth = 1280;
        Graphics.PreferredBackBufferHeight = 720;
        Graphics.ApplyChanges();

        base.Initialize();

        _scaleDeviceHandler = new ScaleDeviceHandler(Scene, Graphics, GraphicsAdapter.DefaultAdapter);
        _scaleDeviceHandler.ScaleChanged += delegate
        {
            Console = new DevConsole(Global.CommandProcessor, Scene, Console);
            Log.Out.UpdateReference(Console);
        };

        _settingsManager = new SettingsManager(SettingsAndSaveManager, _scaleDeviceHandler);
        _settingsManager.Load();

        Scene.Camera.Calculate();

        _menuManager = new MenuManager(Scene);
        _menuManager.SettingsChange += _settingsManager.SettingsChange;

        var save = SettingsAndSaveManager.GetSave<WorldSave>();
        _worldManager = new WorldManager(Scene, save);
        _worldManager.TriggeredMenu += _menuManager.HandleMenu;

        _cursor = new Cursor(Scene.Display.Scale * 4);
        _mousePointer = new MousePointer(Scene);
        _positionListener = new PositionListener();
        _positionListener.Add(_mousePointer, _cursor);

        Console.Context.RegisterContext("world_manager", _worldManager);
        Console.Context.RegisterContext("save_manager", SettingsAndSaveManager);
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        Cursor.Texture = Content.GetTexture("cursor");

        SettingsButton.Texture = Content.GetTexture("buttons/settings");

        SettingsBackdrop.Texture = Content.GetTexture("backdrops/settings");

        WorldBackground.Island0 = Content.GetTexture("world/island0");
        WorldTileSelect.Texture = Content.GetTexture("world/select");
        Cave.Texture = Content.GetTexture("world/cave");
    }

    protected override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            Scene.Update(gameTime);
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

        SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp,
            transformMatrix: Scene.Camera.CameraMatrix);

        _worldManager.Draw(SpriteBatch);
        _menuManager.Draw(SpriteBatch);
        _cursor.Draw(SpriteBatch);

        SpriteBatch.End();

        base.Draw(gameTime);
        DrawConsole();
    }
}