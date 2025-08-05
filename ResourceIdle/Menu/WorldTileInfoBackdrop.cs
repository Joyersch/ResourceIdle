using Joyersch.Monogame;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Ui.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceIdle.Menu.Backdrops;
using ResourceIdle.World;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.Menu;

public class WorldTileInfo : IDrawable, IMoveable
{
    private Vector2 _position;
    private readonly float _scale;
    private Vector2 _size;
    private WorldTileInfoBackdrop _backdrop;

    public Rectangle Rectangle => new(_position.ToPoint(), _size.ToPoint());

    public bool Active { get; private set; }

    public WorldTileData Data { get; private set; }

    public WorldTileInfo(float scale)
    {
        _position = Vector2.Zero;
        _scale = scale;
        _size = new Vector2(128, 166) * scale;
        _backdrop = new WorldTileInfoBackdrop(scale);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Active)
            return;

        _backdrop.Draw(spriteBatch);
    }

    public Vector2 GetPosition()
        => _position;

    public Vector2 GetSize()
        => _size;

    public void Move(Vector2 newPosition)
        => _position = _size;

    public void Select(WorldTileData data)
    {
        Data = data;
        _backdrop.Move(data.Anchor);
    }

    public void Show()
        => Active = true;

    public void Hide()
        => Active = false;
}