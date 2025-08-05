using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.World.Island0;

public sealed class Background : IIslandBackground
{
    public static Texture2D Texture { get; set; }
    private Vector2 _position;
    private readonly float _scale;
    private Vector2 _size;
    public Rectangle Rectangle => new(_position.ToPoint(), _size.ToPoint());

    public Background(float scale)
    {
        _position = Vector2.Zero;
        _scale = scale;
        _size = new Vector2(320, 192) * scale;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
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
        => _position = newPosition;
}