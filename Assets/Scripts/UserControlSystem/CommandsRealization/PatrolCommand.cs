using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Utils;

namespace UserControlSystem.CommandsRealization
{
    public sealed class PatrolCommand : IPatrolCommand
    {
        public Vector3 To { get; }
        public Vector3 From { get; }
        public PatrolCommand(Vector3 to, Vector3 from)
        {
            To = to;
            From = from;
        }
    }
}
