using UnityEngine;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;

public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
{
    public override void ExecuteSpecificCommand(IPatrolCommand command)
        => Debug.Log($"{name} is patrolling from {command.From} to {command.To}");
}
