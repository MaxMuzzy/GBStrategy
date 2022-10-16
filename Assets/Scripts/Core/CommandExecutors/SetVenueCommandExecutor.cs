using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class SetVenueCommandExecutor : CommandExecutorBase<ISetVenueCommand>
    {
        public override async Task ExecuteSpecificCommand(ISetVenueCommand command)
        {
            GetComponent<MainBuilding>().Venue = command.Venue;
        }
    }
}