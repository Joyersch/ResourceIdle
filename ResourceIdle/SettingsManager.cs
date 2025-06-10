using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Storage;
using Joyersch.Monogame.UI;

namespace ResourceIdle;

public sealed class SettingsManager
{
    private readonly SettingsAndSaveManager<string> _saveManager;
    private readonly ScaleDeviceHandler _scaleHandler;
    private GeneralSettings _settings;

    public SettingsManager(SettingsAndSaveManager<string> saveManager, ScaleDeviceHandler scaleHandler)
    {
        _saveManager = saveManager;
        _scaleHandler = scaleHandler;
        _settings = _saveManager.GetSetting<GeneralSettings>();
    }

    public void Load()
    {
        if (!_saveManager.LoadSettings())
            _saveManager.SaveSettings();
        _settings = _saveManager.GetSetting<GeneralSettings>();

        _scaleHandler.ApplyResolution(_settings.Resolution);

        if (_settings.Fullscreen)
            _scaleHandler.Fullscreen();
    }

    public void SettingsChange(SettingsElement element)
    {
        switch (element)
        {
            case SettingsElement.Fullscreen:
                break;
            case SettingsElement.Resolution:
                break;
            case SettingsElement.Language:
                break;
            default:
                Log.Error("Unknown setting requested to change!");
                break;
        }

        _saveManager.SaveSettings();
    }
}