using System;
using UnityEngine;

namespace UserControlSystem
{
    public abstract class ScriptableObjectValueBase<T> : ScriptableObject
    {
        public T CurrentValue { get; private set; }
        public Action<T> OnNewValue;
        public void SetValue(T value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }
    }
}
