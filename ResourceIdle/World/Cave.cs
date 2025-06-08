using System;
using Joyersch.Monogame;
using Joyersch.Monogame.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.World;

public class Cave : IDrawable, IInteractable, IHitbox
{
    public static Texture2D Texture;
    private static Vector2 _imageSize = new(8, 6);

    public event Action<Cave> Clicked;

    public Rectangle Rectangle => new(Data.Position.ToPoint(), _size.ToPoint());
    public Rectangle[] Hitbox => [Rectangle];
    public CaveData Data;

    private readonly float _scale;
    private MouseActionsMat _mouseMat;

    private Vector2 _size => _imageSize * _scale;


    public Cave(CaveData data, float scale)
    {
        Data = data;
        _scale = scale;
        _mouseMat = new MouseActionsMat(this);
        _mouseMat.Click += delegate { Clicked?.Invoke(this); };
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            Data.Position,
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