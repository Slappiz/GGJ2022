using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Ui;
using UnityEngine;

namespace StateMachine
{
    public class StateHandler : MonoBehaviour
    {
        [SerializeField] private Board.Board _board;
        [SerializeField] private PlayerUI _playerUI;
        
        private Dictionary<Type, AbstractState> _states = new Dictionary<Type, AbstractState>();
        private AbstractState _activeState;
        private Coroutine _activeCoroutine;

        public Board.Board Board => _board;
        public ResourceHandler ResourceHandler { get; private set; }
        public PlayerUI PlayerUI => _playerUI;
        
        private void Awake()
        {
            Init();
        }

        void Init()
        {
            ResourceHandler = new ResourceHandler(0);
            ChangeState<InitState>();
        }
        
        public abstract class AbstractState
        {
            public StateHandler StateHandler { get; internal set; }
            
            public abstract void Enter();
            public abstract IEnumerator Enumerator();
            public abstract void Exit();
        }

        public void ChangeState<T>() where T : AbstractState, new()
        {
            Exit(_activeState);
            _activeState = GetState<T>();
            Enter(_activeState);
        }
        
        private void Enter(AbstractState state)
        {
            if (state == null) return;
            state.Enter();
            _activeCoroutine = StartCoroutine(state.Enumerator());
        }
        
        private void Exit(AbstractState state)
        {
            if (state == null) return;
            state.Exit();
            StopCoroutine(_activeCoroutine);
        }
        
        private AbstractState GetState<T>() where T : AbstractState, new()
        {
            var type = typeof(T);

            if (!_states.ContainsKey(type))
            {
                _states[type] = new T
                {
                    StateHandler = this
                };
            }

            return _states[type];
        }
    }
}