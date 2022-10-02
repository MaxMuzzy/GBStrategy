﻿using System;
using System.Collections.Generic;
using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.View;
using Utils;

namespace UserControlSystem.UI.Presenter
{
    public sealed class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        [SerializeField] private AssetsContext _context;

        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectable.OnSelected += ONSelected;
            ONSelected(_selectable.CurrentValue);

            _view.OnClick += ONButtonClick;
        }

        private void ONSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
                return;
            }
            _currentSelectable = selectable;

            _view.Clear();
            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }

        private void ONButtonClick(ICommandExecutor commandExecutor)
        {
            var unitProducer = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;
            if (unitProducer != null)
            {
                unitProducer.ExecuteSpecificCommand(_context.Inject(new ProduceUnitCommandHeir()));
                return;
            }
            var moveCommand = commandExecutor as CommandExecutorBase<IMoveCommand>;
            if (moveCommand != null)
            {
                moveCommand.ExecuteSpecificCommand(_context.Inject(new MoveCommand()));
                return;
            }
            var attackCommand = commandExecutor as CommandExecutorBase<IAttackCommand>;
            if (attackCommand != null)
            {
                attackCommand.ExecuteSpecificCommand(_context.Inject(new AttackCommand()));
                return;
            }
            var patrolCommand = commandExecutor as CommandExecutorBase<IPatrolCommand>;
            if (patrolCommand != null)
            {
                patrolCommand.ExecuteSpecificCommand(_context.Inject(new PatrolCommand()));
                return;
            }
            var stopCommand = commandExecutor as CommandExecutorBase<IStopCommand>;
            if (stopCommand != null)
            {
                stopCommand.ExecuteSpecificCommand(_context.Inject(new StopCommand()));
                return;
            }
            throw new ApplicationException($"{nameof(CommandButtonsPresenter)}.{nameof(ONButtonClick)}: " +
                                           $"Unknown type of commands executor: {commandExecutor.GetType().FullName}!");
        }
    }
}