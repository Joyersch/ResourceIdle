using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Island0 = ResourceIdle.World.Island0;

namespace ResourceIdle.World;

public class IslandFactory
{
    private readonly Scene _scene;
    private readonly WorldSave _worldSave;

    public IslandFactory(Scene scene, WorldSave worldSave)
    {
        _scene = scene;
        _worldSave = worldSave;
    }

    public IIsland GetIsland(int number)
    {
        return number switch
        {
            _ => new Island0.Island(_scene, _worldSave)
        };
    }
}