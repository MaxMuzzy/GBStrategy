using UnityEngine;

namespace Abstractions
{
    public interface ISelectable : IHealth, IIconHolder
    {
        Transform StartPoint { get; }
        void EnableOutline(bool var);
    }
}