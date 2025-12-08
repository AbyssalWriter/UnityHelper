namespace Helper.StateMachine
{
    public class FunctionState : BaseState
    {
        public FunctionState(EnterState aEnter, UpdateState aUpdate, FixedUpdateState aFixedUpdate, ExitState aExit)
        {
            _enter = aEnter;
            _update = aUpdate;
            _fixedUpdate = aFixedUpdate;
            _exit = aExit;
        }

        public override bool CanEnter()
        {
            return true;
        }

        public override bool CanExit()
        {
            return true;
        }
    }
}