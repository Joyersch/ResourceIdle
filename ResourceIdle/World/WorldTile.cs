using System;
using Joyersch.Monogame;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Ui;
using Microsoft.Xna.Framework;

namespace ResourceIdle.World;

public sealed class WorldTile : IInteractable, IHitbox, IRectangle
{
    public Vector2 Position { get; private set; }
    private readonly float _scale;
    private readonly MouseActionsMat _mouseActionsMat;

    public static float Single = 16;

    public Rectangle Rectangle => new(Position.ToPoint(), new Vector2(_scale * Single).ToPoint());
    public Rectangle[] Hitbox => [Rectangle];

    public event Action<object> Clicked;

    public WorldTileData Data { get; private set; }

    public WorldTile(WorldTileData data, Vector2 position, float scale)
    {
        Position = position;
        _scale = scale;

        Data = data;

        _mouseActionsMat = new MouseActionsMat(this);
        _mouseActionsMat.Click += delegate { Clicked?.Invoke(this); };
    }

    public bool UpdateInteraction(GameTime gameTime, IHitbox toCheck)
        => _mouseActionsMat.UpdateInteraction(gameTime, toCheck);
}