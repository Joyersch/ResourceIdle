using Joyersch.Monogame;
using Microsoft.Xna.Framework;

namespace ResourceIdle.World;

public class IslandWrapper : IInteractable
{
    public Rectangle[] Hitbox => _island.Hitbox;

    private IIsland _island;

    public IslandWrapper(IIsland island)
    {
        _island = island;
    }

    public void SetIsland(IIsland island)
        => _island = island;

    public bool UpdateInteraction(GameTime gameTime, IHitbox toCheck)
        => _island.UpdateInteraction(gameTime, toCheck);
}