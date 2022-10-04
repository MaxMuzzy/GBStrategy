using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Utils;
using Abstractions;

namespace UserControlSystem.CommandsRealization
{
    public sealed class AttackCommand : IAttackCommand
    {
        public IAttackable Target { get; }
        public AttackCommand(IAttackable target)
        {
            Target = target;
        }
    }
}
