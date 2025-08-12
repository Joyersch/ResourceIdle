using System.Collections.Generic;
using Joyersch.Monogame;
using Joyersch.Monogame.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.Menu.Buttons;

namespace ResourceIdle.Menu;

public sealed class MenuManager : IManageable
{
    private readonly Scene _scene;
    private readonly InteractHandler _interactHandler;
    private List<IUserInferface> _elements;

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
    }

    public void Update(GameTime gameTime)
    {
        foreach (var element in _elements)
            element.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var element in _elements)
            element.Draw(spriteBatch);
    }
}