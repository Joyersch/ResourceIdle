using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.World;

public class Background : IDrawable
{
    public Rectangle Rectangle => Rectangle.Empty;
    public static Texture2D Texture;

    public void Draw(SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }
}