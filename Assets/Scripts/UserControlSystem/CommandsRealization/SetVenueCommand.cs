using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace UserControlSystem.CommandsRealization
{
    public sealed class SetVenueCommand : ISetVenueCommand
    {
        public Vector3 Venue { get; }

        public SetVenueCommand(Vector3 venue)
        {
            Venue = venue;
        }
    }
}