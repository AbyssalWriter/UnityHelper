using UnityEngine;

namespace Helper.StateMachine
{
    public delegate void EnterState();
    public delegate void UpdateState();
    public delegate void FixedUpdateState();
    public delegate void ExitState();

    public class StateMachine
    {
        private BaseState[] _allStates;
        private int _previousStateIndex = -1;
        private int _currentStateIndex = -1;
        private int _incomingStateIndex = -1;

        private BaseState _currentState;

        private readonly int _stateMachineSize;
        public bool IsLocked { get; private set; }

        public StateMachine(int aSizeState)
        {
            _allStates = new BaseState[aSizeState];
            _stateMachineSize = aSizeState;
        }

        public void AddState(int aStateId, BaseState aState)
        {
            _allStates[aStateId] = aState;
        }

        public void AddState(int aStateId, EnterState aEnter, UpdateState aUpdate, FixedUpdateState aFixedUpdate, ExitState aExit)
        {
            _allStates[aStateId] = new FunctionState(aEnter, aUpdate, aFixedUpdate, aExit);
        }

        public bool ChangeState(int aStateId, bool _ignoreExitCondition = false)
        {
            if (aStateId >= _stateMachineSize) return false;
            if (aStateId == _currentStateIndex) return false;

            BaseState selectedState = _allStates[aStateId];

            if (!selectedState.CanEnter()) return false;
            if (!_ignoreExitCondition && _currentState != null && !_currentState.CanExit()) return false;

            _incomingStateIndex = aStateId;

            _currentState?.Exit();

            _previousStateIndex = _currentStateIndex;

            _currentState = selectedState;
            _currentStateIndex = aStateId;

            _currentState.Enter();
            _incomingStateIndex = -1;

            return true;
        }


        public int GetCurrentStateIndex()
        {
            return _currentStateIndex;
        }

        public int GetPreviousStateIndex()
        {
            return _previousStateIndex;
        }

        public int GetIncomingStateIndex()
        {
            return _incomingStateIndex;
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void FixedUptate()
        {
            _currentState?.FixedUpdate();
        }

        public void Dispose()
        {
            _allStates = null;
        }

    }
}