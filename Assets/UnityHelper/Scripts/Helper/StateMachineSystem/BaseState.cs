namespace Helper.StateMachine
{
    public abstract class BaseState
    {
        protected EnterState _enter;
        protected UpdateState _update;
        protected FixedUpdateState _fixedUpdate;
        protected ExitState _exit;

        public void Enter()
        {
            _enter?.Invoke();
        }

        public void Update()
        {
            _update?.Invoke();
        }

        public void FixedUpdate()
        {
            _fixedUpdate?.Invoke();
        }

        public void Exit()
        {
            _exit?.Invoke();
        }

        public abstract bool CanEnter();
        public abstract bool CanExit();

    }
}