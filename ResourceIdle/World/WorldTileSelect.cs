using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ResourceIdle.World;

public sealed class WorldTileSelect : IManageable, IMoveable
{
    public static Texture2D Texture { get; set; }
    private Vector2 _position;
    private readonly float _scale;
    private Vector2 _size;
    private readonly AnimationProvider _animationProvider;

    public Rectangle Rectangle => new(_position.ToPoint(), _size.ToPoint());


    public WorldTileSelect(Vector2 position, float scale)
    {
        _position = position;
        _scale = scale;
        _size = new Vector2(16, 16) * scale;
        _animationProvider = new AnimationProvider(new Vector2(16, 16), 500f, 2, false);
    }

    public void Update(GameTime gameTime)
    {
        _animationProvider.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            _position,
            _animationProvider.ImageLocation,
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