namespace Helper.StateMachine
{
    public delegate void EnterState();
    public delegate void UpdateState();
    public delegate void ExitState();

    public class StateMachine
    {
        private readonly State[] _allStates;
        private State _previousState;
        private State _currentState;
        private State _incomingState;

        private readonly int _stateMachineSize;
        public bool IsLocked { get; private set; }

        public StateMachine(int aSizeState)
        {
            _allStates = new State[aSizeState];
            _stateMachineSize = aSizeState;
        }

        public void AddState(State aState)
        {
            _allStates[aState.GetStateId()] = aState;
        }

        public void AddState(int aStateId, EnterState aEnter, UpdateState aUpdate, ExitState aExit, bool aSkipFirstUpdate = false)
        {
            _allStates[aStateId] = new State(aStateId, aEnter, aUpdate, aExit, aSkipFirstUpdate);
        }

        public void ChangeState(int aStateId)
        {
            if (IsLocked || aStateId >= _stateMachineSize) return;
            if (_currentState == _allStates[aStateId]) return;

            _incomingState = _allStates[aStateId];

            _currentState?.Exit();

            _previousState = _currentState;
            _currentState = _allStates[aStateId];
            _currentState.Enter();
            _incomingState = null;
        }

        public void ChangeStateWithoutExitSignal(int aStateId)
        {
            if (IsLocked || aStateId >= _stateMachineSize) return;
            if (_currentState == _allStates[aStateId]) return;

            _previousState = _currentState;
            _currentState = _allStates[aStateId];
            _currentState.Enter();
        }

        public void ReturnToPreviousState()
        {
            if (!IsLocked && _previousState != null)
            {
                ChangeState(_previousState.GetStateId());
            }
        }

        public int GetCurrentState()
        {
            if (_currentState == null)
            {
                return -1;
            }

            return _currentState.GetStateId();
        }

        public int GetPreviousState()
        {
            return _previousState?.GetStateId() ?? -1;
        }

        public int GetIncomingState()
        {
            return _incomingState?.GetStateId() ?? -1;
        }

        private void Enter()
        {
            _currentState?.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        private void Exit()
        {
            _currentState?.Exit();
        }

    }
}