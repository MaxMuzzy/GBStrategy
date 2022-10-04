using UnityEngine;

namespace Abstractions
{
    public interface ISelectable : IHealth
    {
        Transform StartPoint { get; }
        Sprite Icon { get; }
        void EnableOutline(bool var);
    }
}