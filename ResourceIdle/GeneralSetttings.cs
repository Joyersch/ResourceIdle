using Joyersch.Monogame.Storage;

namespace ResourceIdle;

public class GeneralSettings : ISettings
{
    public bool Fullscreen { get; set; } = false;
    public Resolution Resolution { get; set; } = new(1280, 720);
}