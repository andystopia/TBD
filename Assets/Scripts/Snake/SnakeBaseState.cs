using HSM;
using UnityEngine;

namespace Snake
{
    public class SnakeBaseState : IState
    {
        private StateMachine _stateMachine;
        
        public StateMachine Machine => _stateMachine;
        
        public virtual void OnStateEnter(StateMachine machine)
        {
            _stateMachine = machine;
        }

        public virtual void OnStateExit()
        {
            _stateMachine = null;
        }

        public virtual void Tick()
        {
            
        }
    }
}