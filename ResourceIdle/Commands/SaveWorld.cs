using System.Collections.Generic;
using Joyersch.Monogame.Console;
using Joyersch.Monogame.Storage;
using Joyersch.Monogame.Ui;

namespace ResourceIdle.Commands;

public class SaveWorld : ICommand
{
    [Command(Name = "save", Description = "Saves the current world to a save file")]
    public IEnumerable<string> Execute(DevConsole console, object[] options, ContextProvider context)
    {
        var save = context.GetValue<SettingsAndSaveManager<string>>("save_manager");
        save.SaveSave();
        return ["Saved game!"];
    }
}