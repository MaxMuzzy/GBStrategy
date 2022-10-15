using UnityEngine;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading;
using System.Threading.Tasks;
namespace Core.CommandExecutors
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public override async Task ExecuteSpecificCommand(IStopCommand command)
            => CancellationTokenSource?.Cancel();
    }
}