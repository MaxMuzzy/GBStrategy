using UnityEngine;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;

public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
{
    public override void ExecuteSpecificCommand(IAttackCommand command)
    {
        Debug.Log("Attack command executed");
    }
}
