using Joyersch.Monogame;

namespace ResourceIdle.Menu;

public interface IDrawer : IUserInferface
{
    public void SetData(IDrawerData data);
}