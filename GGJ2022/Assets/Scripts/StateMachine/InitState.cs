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
            StateHandler.PlayerUI?.Init(StateHandler.ResourceHandler);
            yield return StateHandler.Board.Build();
            StateHandler.ChangeState<PlayerTurnState>();
        }

        public override void Exit()
        {
            
        }
    }
}