using System;
using UnityEngine;

namespace Variables
{
    public abstract class Variable<T> : ScriptableObject
    {
        [SerializeField] private T _defaultValue;
        public event Action<T> OnChanged;
        
        private T _runtimeValue;
        public T RuntimeValue
        {
            get => _runtimeValue;
            set
            {
                _runtimeValue = value;
                Raise();
            }
        }

        protected void OnEnable()
        {
            RuntimeValue = _defaultValue;
            _runtimeValue = _defaultValue;
        }

        public static implicit operator T(Variable<T> variable)
        {
            return variable.RuntimeValue;
        }

        private void Raise()
        {
            OnChanged?.Invoke(RuntimeValue);
        }
    }
}