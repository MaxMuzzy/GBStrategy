using UnityEngine;
using Abstractions;
using Zenject;
using Abstractions.Commands.CommandsInterfaces;
using UniRx;
using Abstractions.Commands;
using Core.CommandRealizationDupes;

namespace Core
{
    public class UnitCommandQueue : MonoBehaviour, ICommandQueue
    {
        [Inject] CommandExecutorBase<IMoveCommand> _moveCommandExecutor;
        [Inject] CommandExecutorBase<IAttackCommand> _attackCommandExecutor;
        [Inject] CommandExecutorBase<IPatrolCommand> _patrolCommandExecutor;
        [Inject] CommandExecutorBase<IStopCommand> _stopCommandExecutor;

        private ReactiveCollection<ICommand> _commands = new();

        [Inject]
        private void Init()
        {
            _commands.ObserveAdd().Subscribe(OnNewCommand).AddTo(this);
        }

        private async void ExecuteCommand(ICommand command)
        {
            await _moveCommandExecutor.TryExecuteCommand(command);
            await _patrolCommandExecutor.TryExecuteCommand(command);
            await _attackCommandExecutor.TryExecuteCommand(command);
            await _stopCommandExecutor.TryExecuteCommand(command);

            if (_commands.Count > 0)
                _commands.RemoveAt(0);
            CheckTheQueue();
        }

        private void OnNewCommand(ICommand command, int index)
        {
            if (index == 0)
            {
                ExecuteCommand(command);
            }
        }

        public void AddCommandToQueue(object wrappedCommand)
        {
            var command = wrappedCommand as ICommand;
            _commands.Add(command);
        }
        private void CheckTheQueue()
        {
            if (_commands.Count > 0)
            {
                ExecuteCommand(_commands[0]);
            }
        }

        public void Clear()
        {
            _commands.Clear();
            _stopCommandExecutor.ExecuteSpecificCommand(new StopCommand());
        }
    }
}