using UnityEngine;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;

public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
{
    public override void ExecuteSpecificCommand(IStopCommand command)
        => Debug.Log($"{name} has stoppped!");
}