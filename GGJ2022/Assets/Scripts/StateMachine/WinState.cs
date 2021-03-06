using System.Collections;
using UnityEngine.SceneManagement;

namespace StateMachine
{
    public class WinState : StateHandler.AbstractState
    {
        
        private bool _waitingForInput = true;

        public override void Enter()
        {
            StateHandler.WinScreen.SetVisibility(true);
            StateHandler.LoseScreen.OnPlayAgain += HandlePlayAgain;
            StateHandler.LoseScreen.OnBackToMain += HandleBackToMain;
        }

        public override IEnumerator Enumerator()
        {
            while (_waitingForInput)
            {
                yield return null;
            }
        }

        public override void Exit()
        {
            StateHandler.LoseScreen.OnPlayAgain -= HandlePlayAgain;
            StateHandler.LoseScreen.OnBackToMain -= HandleBackToMain;
        }

        private void HandleBackToMain()
        {
            _waitingForInput = false;
            SceneManager.LoadScene("Menu");
        }

        private void HandlePlayAgain()
        {
            _waitingForInput = false;
            SceneManager.LoadScene("Game");
        }
    }
}