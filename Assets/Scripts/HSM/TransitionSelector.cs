using System;
using System.Collections.Generic;
using System.Linq;

namespace HSM
{
    public class TransitionSelector
    {
        private readonly List<Type> attemptTransitions;


        public TransitionSelector()
        {
            attemptTransitions = new List<Type>();
        }
        
        public TransitionSelector Attempt<T>() where T : IState
        {
            attemptTransitions.Add(typeof(T));
            return this;
        }

        public StateValidity Execute(StateMachine stateMachine)
        {
            return attemptTransitions
                .Select(stateMachine.Transition)
                .Any(transition => transition == StateValidity.Valid) 
                ? StateValidity.Valid : StateValidity.Invalid;
        }
    }
    
}