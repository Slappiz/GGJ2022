using System.Collections;

namespace StateMachine
{
    public class PlayerTurnState : StateHandler.AbstractState
    {
        public override void Enter()
        {
        }

        public override IEnumerator Enumerator()
        {
            yield return null;
            StateHandler.ChangeState<AiTurnState>();
        }

        public override void Exit()
        {
        }
    }
}