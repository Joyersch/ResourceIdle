using System;
using System.Linq;
using Joyersch.Monogame;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.World;

public class Cave : IDrawable, IInteractable, IHitbox
{
    public readonly string Id;

    public static Texture2D Texture;
    private static Vector2 _imageSize = new(8, 6);

    public Rectangle Rectangle => new(_position.ToPoint(), _size.ToPoint());
    public Rectangle[] Hitbox => [Rectangle];

    private readonly float _scale;
    private MouseActionsMat _mouseMat;

    private Vector2 _position = Vector2.Zero;
    private Vector2 _size => _imageSize * _scale;

    public event Action<Cave> Clicked;

    public Cave(string id, float scale)
    {
        Id = id;
        _scale = scale;
        _mouseMat = new MouseActionsMat(this);
        _mouseMat.Click += delegate
        {
            Clicked?.Invoke(this);
        };
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

    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
       _mouseMat.UpdateInteraction(gameTime, toCheck);
    }


}