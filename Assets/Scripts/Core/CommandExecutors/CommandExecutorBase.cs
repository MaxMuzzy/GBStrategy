﻿using UnityEngine;
using System.Threading.Tasks;
using Abstractions.Commands;

namespace Core
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor<T> where T: ICommand
    {
        public async Task TryExecuteCommand(object command)
        {
            if (command is T specificCommand)
            {
                await ExecuteSpecificCommand(specificCommand);
            }
        }

        public abstract Task ExecuteSpecificCommand(T command);
    }
}