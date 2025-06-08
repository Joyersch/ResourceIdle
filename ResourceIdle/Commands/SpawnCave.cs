using System.Collections.Generic;
using Joyersch.Monogame.Console;
using Joyersch.Monogame.Storage;
using Joyersch.Monogame.Ui;
using ResourceIdle.World;

namespace ResourceIdle.Commands;

public class SpawnCave : ICommand
{
    [Command(Name = "spawncave", Description = "Spawns a cave. Debug command, use at own risk!")]
    public IEnumerable<string> Execute(DevConsole console, object[] options, ContextProvider context)
    {
        WorldManager manager = context.GetValue<WorldManager>("world_manager");
        var cave = manager.SpawnCave();

        var saveManager = context.GetValue<SettingsAndSaveManager<string>>("save_manager");
        var save = saveManager.GetSave<WorldSave>();
        save.CaveData.Add(cave.Data);

        return new[] { "Spawned Cave!" };
    }
}