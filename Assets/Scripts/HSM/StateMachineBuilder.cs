namespace HSM
{
    public class StateMachineBuilder
    {
        // no constructing!
        private StateMachineBuilder()
        {
            
        }
        
        public static StateMachineBuilder<U> WithMachine<U>(U stateMachine) where U : StateMachine
        {
            return new StateMachineBuilder<U>(stateMachine);
        }

        public static StateMachineBuilder<StateMachine> WithDefaultMachine()
        {
            return new StateMachineBuilder<StateMachine>(new StateMachine());
        }
    }
    public class StateMachineBuilder<T> where T : StateMachine
    {
        private readonly T stateMachine;

        
        public StateMachineBuilder(T stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        
        public StateMachineBuilder<T> AddTransition<From, To>(StateTransition<From, To> transition) where From : IState where To: IState
        {
            stateMachine.AddTransition(transition);
            return this;
        }
        
        public StateMachineBuilder<T> AddTransition<From, To>() where From : IState where To: IState
        {
            return AddTransition(new UnboundedTransition<From, To>());
        }


        public StateMachineBuilder<T> AddState<S>(S state) where S : IState
        {
            stateMachine.AddState(state);
            return this;
        }
        
        // public T Build()
        // {
        //     return stateMachine;
        // }

        public T StartWith<S>() where S : IState
        {
            IState state = stateMachine.GetState<S>();
            stateMachine.SetActiveState(state, typeof(S));
            return stateMachine;
        }
    }
}