using System.Collections;

namespace StateMachine
{
    public class WinState : StateHandler.AbstractState
    {
        public override void Enter()
        {
            
        }

        public override IEnumerator Enumerator()
        {
            yield return null;
        }

        public override void Exit()
        {
        }
    }
}