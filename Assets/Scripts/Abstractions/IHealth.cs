using UnityEngine;
namespace Abstractions
{
    public interface IHealth
    {
        float Health { get; }
        float MaxHealth { get; }
    }
}
