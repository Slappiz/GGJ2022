using System.Collections;
using Game;

namespace StateMachine
{
    public class AiTurnState : StateHandler.AbstractState
    {
        public override void Enter()
        {
            
        }

        public override IEnumerator Enumerator()
        {
            yield return null;
            StateHandler.ChangeState<PlayerTurnState>();
        }

        public override void Exit()
        {
            
        }
    }
}