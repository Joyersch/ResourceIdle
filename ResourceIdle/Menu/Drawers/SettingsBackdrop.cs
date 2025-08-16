using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.Menu.Drawers;

public sealed class SettingsBackdrop : IDrawable, IMoveable
{
    public Rectangle Rectangle => new(_position.ToPoint(), _size.ToPoint());

    public static Texture2D Texture;

    private Vector2 _position;
    private Vector2 _size;
    private readonly float _scale;

    public SettingsBackdrop(float scale)
    {
        _size = new Vector2(278, 214) * scale;
        _position = Vector2.Zero;
        _scale = scale;
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