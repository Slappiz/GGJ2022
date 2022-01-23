using System.Collections;
using Game;

namespace StateMachine
{
    public class AiTurnState : StateHandler.AbstractState
    {
        public override void Enter()
        {
            StateHandler.ResourceHandler.AddResources(StateHandler.Board.Nodes, Team.Ai);
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