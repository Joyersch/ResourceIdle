using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.World;

public class Background : IDrawable
{
    private readonly Scene _scene;
    private readonly float _scale;
    public Rectangle Rectangle => Rectangle.Empty;
    public static Texture2D Texture;

    private float _single = 16;
    public float Single => _single * _scale;

    public Background(Scene scene, float scale)
    {
        _scene = scene;
        _scale = scale;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            _scene.Camera.Position,
            null,
            Color.White,
            0F,
            Vector2.Zero,
            _scale,
            SpriteEffects.None,
            0);
    }
}