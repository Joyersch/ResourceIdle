using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.World;

public class WorldBackground : IDrawable, IMoveable
{
    public static Texture2D Island0 { get; set; }
    private Vector2 _position;
    private readonly WorldSave _save;
    private readonly float _scale;
    private Vector2 _size;
    public Rectangle Rectangle => new(_position.ToPoint(), _size.ToPoint());

    public WorldBackground(Vector2 position, WorldSave save, float scale)
    {
        _position = position;
        _save = save;
        _scale = scale;
        _size = new Vector2(320, 192) * scale;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            GetCurrentTexture(),
            _position,
            null,
            Color.White,
            0F,
            Vector2.Zero,
            _scale,
            SpriteEffects.None,
            0);
    }

    public Vector2 GetPosition()
        => _position;

    public Vector2 GetSize()
        => _size;

    public void Move(Vector2 newPosition)
        => _position = _size;

    private Texture2D GetCurrentTexture()
    {
        return _save.SelectedIsland switch
        {
            0 => Island0,
            _ => Island0
        };
    }
}