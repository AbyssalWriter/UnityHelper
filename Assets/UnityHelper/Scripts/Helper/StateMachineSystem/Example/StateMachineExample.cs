using Helper.Shared;
using UnityEngine;

namespace Helper.StateMachineSystem.Example
{
    public class StateMachineExample : MonoBehaviour
    {
        private enum States
        {
            Idle,
            Walk,
            Run,


            Count // will return 3 
        }

        private StateMachine.StateMachine _stateMachine;

        private void Awake()
        {
            InitStateMachine();
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine.StateMachine(States.Count.ToInt());

            _stateMachine.AddState(States.Idle.ToInt(), IdleEnterState, IdleUpdateState, null, IdleExitState);
            _stateMachine.AddState(States.Walk.ToInt(), null, WalkUpdate, null, null);
            _stateMachine.AddState(States.Run.ToInt(), RunEnterState, null, null, null);

            _stateMachine.ChangeState(States.Idle.ToInt());
        }

        private void Update()
        {
            _stateMachine.Update();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _stateMachine.ChangeState(States.Idle.ToInt());
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _stateMachine.ChangeState(States.Walk.ToInt());
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _stateMachine.ChangeState(States.Run.ToInt());
            }

        }

        private void IdleEnterState()
        {
            UnityEngine.Debug.Log("Idle Enter State");
        }

        private void IdleUpdateState()
        {
            UnityEngine.Debug.Log("Idle Update State");
        }

        private void IdleExitState()
        {
            UnityEngine.Debug.Log("Idle Exit State");
        }

        private void WalkUpdate()
        {
            UnityEngine.Debug.Log("Walk Update State");
        }

        private void RunEnterState()
        {
            UnityEngine.Debug.Log("Run Enter State");
        }
    }
}