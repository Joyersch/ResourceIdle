using Joyersch.Monogame;
using Joyersch.Monogame.Storage;

namespace ResourceIdle.World;

public interface IIsland : IMoveable, IManageable, IInteractable
{
    public void LoadSave(ISave save);
}