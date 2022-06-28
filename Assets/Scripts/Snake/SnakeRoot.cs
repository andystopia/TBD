using System.Collections.Generic;
using HSM;
using SnakeSegment;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(SnakeStateFactory))]
    public class SnakeRoot : MonoBehaviour
    {
        [SerializeField]
        private SnakeBody _snakeBody;

        [SerializeField] private float _movementScalar;

        public float MovementScalar => _movementScalar;

        public SnakeBody Body => _snakeBody;

        private StateMachine _stateMachine;
        
        protected virtual void Start()
        {
            var factory = GetComponent<SnakeStateFactory>();

            _stateMachine = new StateMachineBuilder<StateMachine>(new StateMachine())
                .AddState(factory.MakePlayState())
                .StartWith<SnakePlayState>();
        }

        protected virtual void Update()
        {
            _stateMachine.Tick();
        }
    }
}