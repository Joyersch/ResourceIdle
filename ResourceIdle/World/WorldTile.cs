using Joyersch.Monogame;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Ui;
using Microsoft.Xna.Framework;

namespace ResourceIdle.World;

public sealed class WorldTile : IInteractable, IHitbox, IRectangle
{
    private readonly Scene _scene;
    private Vector2 _position;
    private readonly float _scale;
    private readonly MouseActionsMat _mouseActionsMat;

    public static float Single = 16;

    public Rectangle Rectangle => new(_position.ToPoint(), new Vector2(_scale * Single).ToPoint());
    public Rectangle[] Hitbox => [Rectangle];

    public WorldTile(Scene scene, Vector2 position, float scale)
    {
        _scene = scene;
        _position = position;
        _scale = scale;

        _mouseActionsMat = new MouseActionsMat(this);
        _mouseActionsMat.Enter += delegate { Log.Information($"Tile at: {_position}"); };
    }

    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        _mouseActionsMat.UpdateInteraction(gameTime, toCheck);
    }
}