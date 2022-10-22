using UnityEngine;

namespace Abstractions
{
    public interface IAttackable : IHealth
    {
        void ReceiveDamage(int amount);
    }
}
