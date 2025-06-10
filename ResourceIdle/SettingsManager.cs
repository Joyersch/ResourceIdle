using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Storage;
using Joyersch.Monogame.UI;

namespace ResourceIdle;

public sealed class SettingsManager
{
    private readonly SettingsAndSaveManager<string> _saveManager;
    private readonly ScaleDeviceHandler _deviceHandler;
    private GeneralSettings _settings;

    public SettingsManager(SettingsAndSaveManager<string> saveManager, ScaleDeviceHandler deviceHandler)
    {
        _saveManager = saveManager;
        _deviceHandler = deviceHandler;
        _settings = _saveManager.GetSetting<GeneralSettings>();
    }

    public void Load()
    {
        if (!_saveManager.LoadSettings())
            _saveManager.SaveSettings();
        _settings = _saveManager.GetSetting<GeneralSettings>();

        if (_settings.Fullscreen)
            _deviceHandler.Fullscreen();
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