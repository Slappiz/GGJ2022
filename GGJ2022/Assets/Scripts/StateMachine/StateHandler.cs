﻿using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Game;
using Ui;
using UnityEngine;

namespace StateMachine
{
    public class StateHandler : MonoBehaviour
    {
        [SerializeField] private Board.Board _board;
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private PlayerController _playerController;
        
        private Dictionary<Type, AbstractState> _states = new Dictionary<Type, AbstractState>();
        private AbstractState _activeState;
        private Coroutine _activeCoroutine;

        public Board.Board Board => _board;
        public GameLogic GameLogic { get; private set; }
        public PlayerUI PlayerUI => _playerUI;
        public PlayerController PlayerController => _playerController;
        
        private void Awake()
        {
            Init();
        }

        void Init()
        {
            GameLogic = new GameLogic(_board);

            GameLogic.GameWon += OnGameWon;
            GameLogic.GameLost += OnGameLost;
            
            ChangeState<InitState>();
        }

        public abstract class AbstractState
        {
            public StateHandler StateHandler { get; internal set; }
            
            public abstract void Enter();
            public abstract IEnumerator Enumerator();
            public abstract void Exit();
        }
        
        private void OnGameLost()
        {
            // change to lose state
            Debug.Log("Game Lost");
        }

        private void OnGameWon()
        {
            // change to win state
            Debug.Log("Game Won");
        }

        private void OnDestroy()
        {
            GameLogic.GameWon -= OnGameWon;
            GameLogic.GameLost -= OnGameLost;
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