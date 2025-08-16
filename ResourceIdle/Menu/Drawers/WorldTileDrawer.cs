using Joyersch.Monogame;
using Joyersch.Monogame.Ui.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.World;

namespace ResourceIdle.Menu.Drawers;

public sealed class WorldTileDrawer : IDrawer
{
    private readonly Scene _scene;

    private BasicText _typeDisplay;
    private BasicText[] _resourcesDisplay;

    private WorldTileInfoBackdrop _backdrop;

    public Rectangle Rectangle => _backdrop.Rectangle;
    public Rectangle[] Hitbox => [Rectangle];

    private float _textScale = 2f;

    public WorldTileDrawer(Scene scene)
    {
        _scene = scene;
        _backdrop = new WorldTileInfoBackdrop(scene.Display.Scale * 3);
        _typeDisplay = new BasicText(scene.Display.Scale * _textScale);
        _resourcesDisplay = [];
        SetUiElements();
    }

    public void Update(GameTime gameTime)
    {
        _typeDisplay.Update(gameTime);
        foreach (var display in _resourcesDisplay)
            display.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _backdrop.Draw(spriteBatch);
        _typeDisplay.Draw(spriteBatch);
        foreach (var display in _resourcesDisplay)
            display.Draw(spriteBatch);
    }


    public bool UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        return false;
    }

    public void SetData(IDrawerData data)
    {
        var tileData = (WorldTileData)data;
        _typeDisplay.ChangeText(tileData.Type.ToString());
        _resourcesDisplay = new BasicText[tileData.Resources.Count];
        int counter = 0;
        foreach (var resource in tileData.Resources)
        {
            _resourcesDisplay[counter++] = new BasicText($"{resource.Key} : {(resource.Value * 100f):F1}",
                _scene.Display.Scale * _textScale);
        }

        SetUiElements();
    }

    public Vector2 GetPosition()
        => _backdrop.GetPosition();

    public Vector2 GetSize()
        => _backdrop.GetSize();

    public void Move(Vector2 newPosition)
    {
        _backdrop.Move(newPosition);
        SetUiElements();
    }

    private void SetUiElements()
    {
        _typeDisplay.InRectangle(this)
            .OnX(0f)
            .OnY(0f)
            .With(_scene.Display.Scale * 4, _scene.Display.Scale * 4)
            .Apply();

        BasicText previous = _typeDisplay;
        foreach (var display in _resourcesDisplay)
        {
            display.GetAnchor(previous)
                .SetMainAnchor(AnchorCalculator.Anchor.BottomLeft)
                .SetSubAnchor(AnchorCalculator.Anchor.TopLeft)
                .SetDistanceY(4f * _scene.Display.Scale)
                .Apply();
            previous = display;
        }
    }
}