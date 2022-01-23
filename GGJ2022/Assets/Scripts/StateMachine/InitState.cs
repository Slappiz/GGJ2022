using System.Collections;

namespace StateMachine
{
    public class InitState : StateHandler.AbstractState
    {
        public override void Enter()
        {
            
        }

        public override IEnumerator Enumerator()
        {
            StateHandler.Board.Build();
            yield return null;
            StateHandler.ChangeState<PlayerTurnState>();
        }

        public override void Exit()
        {
            
        }
    }
}