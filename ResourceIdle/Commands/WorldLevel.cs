using System.Collections.Generic;
using Joyersch.Monogame.Console;
using Joyersch.Monogame.Storage;
using Joyersch.Monogame.Ui;
using ResourceIdle.World;

namespace ResourceIdle.Commands;

public class WorldLevel : ICommand
{
    [Command(Name = "level", Description = "Set current level")]
    public IEnumerable<string> Execute(DevConsole console, object[] options, ContextProvider context)
    {
        var world = context.GetValue<WorldManager>("world_manager");

        if (options.Length != 1)
            return ["Bad Options given!"];

        if (!int.TryParse(options[0].ToString(), out int state))
            return ["Bad Options given!"];

        world.WorldState.Level = state;
        world.UpdateTiles();
        return ["Set state!"];
    }
}