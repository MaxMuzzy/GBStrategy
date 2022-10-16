using UnityEngine;

namespace Abstractions.Commands.CommandsInterfaces
{
    public interface ISetVenueCommand : ICommand
    {
        Vector3 Venue { get; }
    }
}