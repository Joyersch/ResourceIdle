using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.World;

namespace ResourceIdle;

public sealed class WorldManager : IManageable, IInteractable
{
    private readonly MenuManager _menuManager;
    public Rectangle Rectangle => Rectangle.Empty;
    private Cave _cave;

    public WorldManager(Scene scene, MenuManager menuManager)
    {
        _menuManager = menuManager;
        _cave = new Cave("test_id", scene.Display.Scale * 4);
        _cave.Clicked += cave => { _menuManager.ToggleCaveView(cave); };
    }

    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        _cave.UpdateInteraction(gameTime, toCheck);
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _cave.Draw(spriteBatch);
    }
}