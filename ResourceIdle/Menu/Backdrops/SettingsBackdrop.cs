using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.Menu.Backdrops;

public sealed class SettingsBackdrop : IDrawable, IMoveable
{
    public Rectangle Rectangle => new(_position.ToPoint(), _size.ToPoint());

    public static Texture2D Texture;

    private readonly Rectangle _leftTopCorner = new(0, 0, 16, 16);
    private readonly Rectangle _top = new(16, 0, 16, 16);
    private readonly Rectangle _rightTopCorner = new(32, 0, 16, 16);

    private readonly Rectangle _left = new(0, 16, 16, 16);
    private readonly Rectangle _center = new(16, 16, 16, 16);
    private readonly Rectangle _right = new(32, 16, 16, 16);

    private readonly Rectangle _leftBottomCorner = new(0, 32, 16, 16);
    private readonly Rectangle _bottom = new(16, 32, 16, 16);
    private readonly Rectangle _rightBottomCorner = new(32, 32, 16, 16);

    private float _unit = 16;
    private float _width = 16;
    private float _height = 9;

    private Vector2 _position;
    private Vector2 _size;
    private readonly float _scale;

    public SettingsBackdrop(float scale)
    {
        _size = new Vector2(_width, _height) * _unit * scale;
        _position = Vector2.Zero;
        _scale = scale;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int y = 0; y < _height; y++)
        {
            spriteBatch.Draw(
                Texture,
                _position + new Vector2(0, y * _scale * _unit),
                y == 0 ? _leftTopCorner : y + 1 == (int)_height ? _leftBottomCorner : _left,
                Color.White,
                0F,
                Vector2.Zero,
                _scale,
                SpriteEffects.None,
                0);

            for (int x = 1; x < _width - 1; x++)
            {
                spriteBatch.Draw(
                    Texture,
                    _position + new Vector2(_unit * x , _unit * y) * _scale,
                    y == 0 ? _top : y + 1 == (int)_height ? _bottom : _center,
                    Color.White,
                    0F,
                    Vector2.Zero,
                    _scale,
                    SpriteEffects.None,
                    0);
            }

            spriteBatch.Draw(
                Texture,
                _position + new Vector2(_width - 1, y) * _scale * _unit,
                y == 0 ? _rightTopCorner : y + 1 == (int)_height ? _rightBottomCorner : _right,
                Color.White,
                0F,
                Vector2.Zero,
                _scale,
                SpriteEffects.None,
                0);
        }
    }

    public Vector2 GetPosition()
        => _position;

    public Vector2 GetSize()
        => _size;

    public void Move(Vector2 newPosition)
        => _position = newPosition;
}