using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core.CommandRealizationDupes
{
    public sealed class MoveCommand : IMoveCommand
    {
        public Vector3 Target { get; }

        public MoveCommand(Vector3 target)
        {
            Target = target;
        }
    }
}
