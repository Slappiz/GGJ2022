using System.Collections;
using UnityEngine;

namespace StateMachine
{
    public class GameplayState : StateHandler.AbstractState
    {
        private bool _isPlaying = true;
        
        public override void Enter()
        {
            StateHandler.PlayerController.Enabled = true;
            StateHandler.GameLogic.GameWon += OnGameWon;
            StateHandler.GameLogic.GameLost += OnGameLost;
            
            StateHandler.PlayerUI.SetVisibility(true);
        }

        public override IEnumerator Enumerator()
        {
            while (_isPlaying)
            {
                yield return null;
            }
        }

        public override void Exit()
        {
            StateHandler.PlayerUI.SetVisibility(false);
            StateHandler.PlayerController.Enabled = false;
            StateHandler.GameLogic.GameWon -= OnGameWon;
            StateHandler.GameLogic.GameLost -= OnGameLost;
        }
        
        private void OnGameLost()
        {
            Debug.Log("Game Lost");
            _isPlaying = false;
            StateHandler.ChangeState<LoseState>();
        }

        private void OnGameWon()
        {
            Debug.Log("Game Won");
            _isPlaying = false;
            StateHandler.ChangeState<WinState>();
        }
    }
}