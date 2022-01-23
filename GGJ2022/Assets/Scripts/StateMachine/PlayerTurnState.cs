using System.Collections;
using Game;

namespace StateMachine
{
    public class PlayerTurnState : StateHandler.AbstractState
    {
        public override void Enter()
        {
            StateHandler.ResourceHandler.AddResources(StateHandler.Board.Nodes, Team.Player);
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