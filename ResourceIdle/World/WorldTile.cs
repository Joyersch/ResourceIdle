using System;
using Joyersch.Monogame;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace ResourceIdle.World;

public class WorldTile : IDrawable, IInteractable, IHitbox
{
    private readonly Scene _scene;
    public readonly WorldState State;
    private Vector2 _position;
    public Rectangle Rectangle => new(_position.ToPoint(), new Vector2(_scale * Single).ToPoint());

    public static Texture2D Texture;

    public static float Single = 16;
    private float _scale;

    private int _type;

    private MouseActionsMat _mouseActionsMat;
    public Rectangle[] Hitbox => [Rectangle];

    public WorldTile(Scene scene, WorldState state, Vector2 position, float scale)
    {
        _scene = scene;
        State = state;
        _position = position;
        _scale = scale;

        _type = state.MapToLevel(position) * 16;
        _mouseActionsMat = new MouseActionsMat(this);
        _mouseActionsMat.Enter += delegate { Log.Information($"Tile at: {_position}"); };
    }

    public void UpdateTile()
    {
        _type = State.MapToLevel(_position) * 16;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            _position,
            new Rectangle(_type, State.Level * 16, 16, 16),
            Color.White,
            0F,
            Vector2.Zero,
            _scale,
            SpriteEffects.None,
            0);
    }

    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        _mouseActionsMat.UpdateInteraction(gameTime, toCheck);
    }
}