using Abstractions.Commands.CommandsInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserControlSystem.CommandsRealization;

namespace UserControlSystem
{
    public class SetVenueCommandCommandCreator : CancellableCommandCreatorBase<ISetVenueCommand, Vector3>
    {
        protected override ISetVenueCommand CreateCommand(Vector3 argument) => new SetVenueCommand(argument);
    }
}