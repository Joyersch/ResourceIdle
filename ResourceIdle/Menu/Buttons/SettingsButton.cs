using System;
using Joyersch.Monogame;
using Joyersch.Monogame.Collision;
using Joyersch.Monogame.Ui;
using Joyersch.Monogame.Ui.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IUpdateable = Joyersch.Monogame.IUpdateable;

namespace ResourceIdle.Menu.Buttons;

public sealed class SettingsButton : IButton
{
    private Vector2 _position;

    private readonly float _initialScale;
    private float _extendedScale = 1F;
    public float Scale => _initialScale * _extendedScale;
    private Vector2 _size;
    private Vector2 _drawingScale;
    private Color _color;
    private Rectangle _imageLocation;
    private Rectangle _baseImageLocation = new(0, 0, (int)ImageSize.X, (int)ImageSize.Y);

    private MouseActionsMat _mouseMat;
    private HitboxProvider _hitbox;

    public Rectangle[] Hitbox => _hitbox.Hitbox;
    private Rectangle _rectangle;
    public Rectangle Rectangle => _rectangle;

    public float Layer { get; set; }

    public bool IsHover => _mouseMat.IsHover;

    public event Action<object> Leave;
    public event Action<object> Enter;
    public event Action<object> Click;

    public static Texture2D Texture;
    private static readonly Vector2 ImageSize = new(24, 24);

    public static float DefaultScale { get; set; } = 4F;

    public SettingsButton() : this(Vector2.Zero)
    {
    }

    public SettingsButton(float scale) : this(Vector2.Zero, scale)
    {
    }

    public SettingsButton(Vector2 position) : this(position, DefaultScale)
    {
    }

    public SettingsButton(Vector2 position, float initialScale)
    {
        _position = position;
        _initialScale = initialScale;
        _size = ImageSize * Scale;
        _drawingScale = Vector2.One * Scale;
        _color = Color.White;

        var hitbox = new[]
        {
            new Rectangle(3, 4, 17, 19),
        };

        _imageLocation = new Rectangle(24, 0, 24, 24);

        _hitbox = new HitboxProvider(this, hitbox, _drawingScale);
        _rectangle = this.GetRectangle();

        _mouseMat = new MouseActionsMat(this);
        _mouseMat.Leave += _ => Leave?.Invoke(this);
        _mouseMat.Enter += _ => Enter?.Invoke(this);
        _mouseMat.Click += _ => Click?.Invoke(this);
    }

    public bool UpdateInteraction(GameTime gameTime, IHitbox toCheck)
        => _mouseMat.UpdateInteraction(gameTime, toCheck);

    public void Update(GameTime gameTime)
    {
        _hitbox.Update(gameTime);
        _rectangle = this.GetRectangle();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsHover)
        {
            spriteBatch.Draw(
                Texture,
                _position,
                _baseImageLocation,
                _color,
                0F,
                Vector2.Zero,
                _drawingScale,
                SpriteEffects.None,
                Layer);
            return;
        }

        spriteBatch.Draw(
            Texture,
            _position,
            _imageLocation,
            _color,
            0F,
            Vector2.Zero,
            _drawingScale,
            SpriteEffects.None,
            Layer);
    }

    public Vector2 GetPosition()
        => _position;


    public Vector2 GetSize()
        => _size;

    public void Move(Vector2 newPosition)
    {
        _position = newPosition;
        _rectangle = this.GetRectangle();
    }

    public void ChangeColor(Color[] input)
        => _color = input[0];


    public int ColorLength()
        => 1;


    public Color[] GetColor()
        => [_color];

    public void SetScale(float scale)
    {
        _extendedScale = scale;
        _size = ImageSize * Scale;
        _drawingScale = Vector2.One * Scale;
        _rectangle = this.GetRectangle();
        _hitbox.SetScale(_drawingScale);
    }
}