using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.World;

public class Cave : IDrawable
{
    private readonly float _scale;
    public static Texture2D Texture;

    public Rectangle Rectangle => new Rectangle(_position.ToPoint(), Point.Zero);
    private Vector2 _position = Vector2.Zero;

    public Cave(float scale)
    {
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
}