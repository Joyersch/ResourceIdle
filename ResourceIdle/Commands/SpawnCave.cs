using System.Collections.Generic;
using Joyersch.Monogame.Console;
using Joyersch.Monogame.Ui;

namespace ResourceIdle.Commands;

public class SpawnCave : ICommand
{
    [Command(Name = "spawncave", Description = "Spawns a cave. Debug command, use at own risk!")]
    public IEnumerable<string> Execute(DevConsole console, object[] options, ContextProvider context)
    {
        WorldManager manager = context.GetValue<WorldManager>("world_manager");
        manager.SpawnCave();
        return new[] { "Spawned Cave!" };
    }
}