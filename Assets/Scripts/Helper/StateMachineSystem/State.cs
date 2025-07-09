namespace Helper.StateMachine
{
    public class State
    {
        private readonly int _stateId;
        private readonly EnterState _enter;
        private readonly UpdateState _update;
        private readonly ExitState _exit;
        private readonly bool _skipFirstUpdate;
        private bool _firstUpdateSkipped;

        public State(int aId, EnterState aEnter, UpdateState aUpdate, ExitState aExit, bool aSkipFirstUpdate)
        {
            _stateId = aId;
            _enter = aEnter;
            _update = aUpdate;
            _exit = aExit;
            _skipFirstUpdate = true;
        }

        public int GetStateId()
        {
            return _stateId;
        }

        public void Enter()
        {
            _enter?.Invoke();
        }

        public void Update()
        {
            if(_skipFirstUpdate && !_firstUpdateSkipped)
            {
                _firstUpdateSkipped = true;
                return;
            }

            _update?.Invoke();
        }

        public void Exit()
        {
            _exit?.Invoke();
        }

    }
}