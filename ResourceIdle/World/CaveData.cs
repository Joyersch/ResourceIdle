using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ResourceIdle.World;

public class CaveData
{
    public Vector2 Position { get; set; }

    public BigInteger Generated { get; set; }
}