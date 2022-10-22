using Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Core
{
    public class GameState : MonoBehaviour, IGameState
    {
        public IObservable<int> State => _state;
        private Subject<int> _state = new();

        private void Update()
        {
            ThreadPool.QueueUserWorkItem(CheckState);
        }

        private void CheckState(object state)
        {
            if (FactionMember.FactionsCount == 0)
                _state.OnNext(0);
            else if (FactionMember.FactionsCount == 1)
                _state.OnNext(FactionMember.GetWinner());
        }
    }
}