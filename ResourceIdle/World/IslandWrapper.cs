using Joyersch.Monogame;
using Microsoft.Xna.Framework;

namespace ResourceIdle.World;

public class IslandWrapper : IInteractable
{
    public Rectangle[] Hitbox => _island.Hitbox;

    private Island _island;

    public void SetIsland(Island island)
        => _island = island;

    public bool UpdateInteraction(GameTime gameTime, IHitbox toCheck)
        => _island.UpdateInteraction(gameTime, toCheck);
}