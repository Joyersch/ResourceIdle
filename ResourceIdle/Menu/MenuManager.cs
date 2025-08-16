using System.Collections.Generic;
using Joyersch.Monogame;
using Joyersch.Monogame.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.Menu.Buttons;
using ResourceIdle.Menu.Drawers;
using ResourceIdle.World;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.Menu;

public sealed class MenuManager : IManageable
{
    private readonly Scene _scene;
    private readonly InteractHandler _interactHandler;
    private List<IUserInferface> _elements;
    private IDrawer _drawer;

    private Dictionary<DrawersEnum, IDrawer> _drawers;

    public Rectangle Rectangle => _scene.Camera.Rectangle;

    public MenuManager(Scene scene, InteractHandler interactHandler)
    {
        _scene = scene;
        _interactHandler = interactHandler;
        _elements = [];

        var button = new SettingsButton(2f * scene.Display.Scale);
        button.InRectangle(scene.Camera)
            .OnX(1)
            .OnY(1)
            .BySize(-1.25f)
            .Apply();
        _elements.Add(button);
        _interactHandler.AddInteractable(button, 100);
        button.Click += delegate { Log.Information("Settings clicked!"); };

        _drawers = [];
        _drawers.Add(DrawersEnum.Settings, new SettingsDrawer(_scene));
        _drawers.Add(DrawersEnum.TileInfo, new WorldTileDrawer(_scene));
    }

    public void Update(GameTime gameTime)
    {
        foreach (var element in _elements)
            element.Update(gameTime);
        _drawer?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var element in _elements)
            element.Draw(spriteBatch);
        _drawer?.Draw(spriteBatch);
    }

    public void ShowDrawer(DrawersEnum drawer, IDrawerData data)
    {
        switch (drawer)
        {
            case DrawersEnum.None:
                _drawer = null;
                return;
            case DrawersEnum.TileInfo:
                _drawer = _drawers[drawer];
                _drawer.Move(((WorldTileData)data).Anchor);
                _drawer.SetData(data);
                return;

            case DrawersEnum.Settings:
                Log.Information("Settings ShowDrawer is not being handled");
                return;
        }
    }
}