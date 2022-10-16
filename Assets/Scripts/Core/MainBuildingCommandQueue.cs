using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Zenject;
namespace Core
{
    public class MainBuildingCommandQueue : MonoBehaviour, ICommandQueue
    {
        [Inject]
        CommandExecutorBase<IProduceUnitCommand> _produceUnitCommandExecutor;

        [Inject]
        CommandExecutorBase<ISetVenueCommand> _setVenueCommandExecutor;

        public async void AddCommandToQueue(object command)
        {
            await _produceUnitCommandExecutor.TryExecuteCommand(command);
            await _setVenueCommandExecutor.TryExecuteCommand(command);
        }

        public void Clear() { }
    }
}
