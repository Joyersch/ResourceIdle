using Joyersch.Monogame;
using Microsoft.Xna.Framework;
using Island0 = ResourceIdle.World.Island0;

namespace ResourceIdle.World;

public class IslandFactory
{
    private readonly Scene _scene;
    private readonly PlayerData _data;

    public IslandFactory(Scene scene, PlayerData data)
    {
        _scene = scene;
        _data = data;
    }

    public IIsland GetIsland(int number)
    {
        return number switch
        {
            _ => new Island0.Island(_scene, _data)
        };
    }
}