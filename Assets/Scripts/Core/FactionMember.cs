using UnityEngine;
using Abstractions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Core
{
    public class FactionMember : MonoBehaviour, IFactionMember
    {
        public int FactionId => _factionId;
        [SerializeField] private int _factionId;

        public static int FactionsCount
        {
            get
            {
                lock (_factionsCount)
                {
                    return _factionsCount.Count;
                }
            }
        }
        public static int GetWinner()
        {
            lock (_factionsCount)
                return _factionsCount.Keys.First();
        }
        private static Dictionary<int, List<int>> _factionsCount = new();

        private void Awake()
        {
            if (_factionId != 0)
            {
                Register();
            }
        }

        private void Register()
        {
            lock (_factionsCount)
            {
                if (!_factionsCount.ContainsKey(_factionId))
                {
                    _factionsCount.Add(_factionId, new List<int>());
                }
                if (!_factionsCount[_factionId].Contains(GetInstanceID()))
                {
                    _factionsCount[_factionId].Add(GetInstanceID());
                }
            }
        }

        public void SetFaction(int factionId)
        {
            _factionId = factionId;
            Register();
        }

        private void OnDestroy()
        {
            Unregister();
        }
        private void Unregister()
        {
            lock (_factionsCount)
            {
                if (_factionsCount[_factionId].Contains(GetInstanceID()))
                {
                    _factionsCount[_factionId].Remove(GetInstanceID());
                }
                if (_factionsCount[_factionId].Count == 0)
                {
                    _factionsCount.Remove(_factionId);
                }
            }
        }
    }
}
