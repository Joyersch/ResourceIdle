using System.Collections.Generic;
using Joyersch.Monogame.Console;
using Joyersch.Monogame.Storage;
using Joyersch.Monogame.Ui;
using ResourceIdle.World;

namespace ResourceIdle.Commands;

public class LoadSave : ICommand
{
    [Command(Name = "load", Description = "Loads the current world")]
    public IEnumerable<string> Execute(DevConsole console, object[] options, ContextProvider context)
    {
        var save = context.GetValue<SettingsAndSaveManager<string>>("save_manager");
        save.LoadSaves();
        var world = context.GetValue<WorldManager>("world_manager");
        world.LoadSave(save.GetSave<WorldSave>());
        return ["Loaded save!"];
    }
}