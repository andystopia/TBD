using System;
using System.Collections.Generic;
using System.Resources;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

namespace HSM
{
    public class StateMachine : IState
    {
        private class ActiveStateData
        {
            private IState state;
            private Type subtypeOf;

            public IState State => state;
            public Type SubtypeOf => subtypeOf;

            public ActiveStateData(IState state, Type subtypeOf)
            {
                this.state = state;
                this.subtypeOf = subtypeOf;
            }

            public void ReplaceWith(IState withState, Type withSubtypeOf)
            {
                state = withState;
                subtypeOf = withSubtypeOf;
            }
        }
        
        private readonly TwoCovarianceContainer<StateTransition> transitionTable = new TwoCovarianceContainer<StateTransition>();

        private StateMachine parentStateMachine;

        private ActiveStateData _activeStateData;
        public IState ActiveState => _activeStateData?.State;

        public StateMachine ParentStateMachine
        {
            get => parentStateMachine;
            set => parentStateMachine = value;
        }

        private readonly CovarianceContainer<IState> states = new CovarianceContainer<IState>();


        private StateValidity ValidateStateTransition(IState to, Type toType)
        {
            if (_activeStateData == null)
            {
                throw new NoActiveStateException("Attempted to validate a state transition with no actively " +
                                                 "enabled state. Due to transitioning *from no state* is a difficult" +
                                                 "thing to define, it has been decided that instead this case " +
                                                 "will raise an exception. Consider using a method to set" +
                                                 "the active state *before* attempting to transition to another state");
            }
            var transition = transitionTable.GetInstanceWhichVariesOverType(_activeStateData.SubtypeOf, toType);

            if (transition == null)
            {
                throw new NoSuchStateTransitionException(_activeStateData.SubtypeOf, toType);
            }
            return transition.Validate(_activeStateData.State, to);
        }
        
        internal void AddTransition<From, To>(StateTransition<From, To> transition) where From : IState where To: IState
        {
            transitionTable.Add<From, To>(transition);
        }
        
        internal void AddTransition<From, To>() where From : IState where To: IState
        {
            AddTransition(new UnboundedTransition<From, To>());
        }


        internal void AddState<S>(S state) where S : IState
        {
            states.Add(typeof(S), state);
        }

        

        [NotNull] internal  T GetState<T>() where T : IState
        {
            // this hurts
            var state = (T) states.GetInstanceWhichVariesOverType<T>();

            if (state == null)
            {
                throw new NoSuchStateException($"There exists no state to retrieve which satisfies {typeof(T)}.");
            }

            return state;
        }

        internal IState GetState(Type t)
        {
            return states.GetInstanceWhichVariesOverType(t);
        }

        public StateValidity Transition(Type to)
        {
            var transitionTo = GetState(to);
            return ChangeStates(transitionTo, to);
        }

        private StateValidity ChangeStates(IState transitionTo, Type transitionType)
        {
            var stateValidity = ValidateStateTransition(transitionTo, transitionType);

            if (stateValidity != StateValidity.Valid) return StateValidity.Invalid;

            // equip the new state before calling on state exit on the previous type.
            IState state = ActiveState;
            _activeStateData?.ReplaceWith(transitionTo, transitionType);
            state.OnStateExit();
            transitionTo.OnStateEnter(this);
            return StateValidity.Valid;
        }

        public StateValidity Transition<To>() where To: IState
        {
            var transitionTo = GetState<To>();
            return ChangeStates(transitionTo, typeof(To));
        }
        
        public StateValidity Transition<To>(Action<To> postExitActiveState, Action<To> postEnterTransitionedState) where To: IState
        {
            var transitionTo = GetState<To>();
            var stateValidity = ValidateStateTransition(transitionTo, typeof(To));
            
            if (stateValidity != StateValidity.Valid) return StateValidity.Invalid;
            
            // equip the new state before calling on state exit on the previous type.
            IState state = ActiveState;
            SetActiveState(transitionTo, typeof(To));
            state?.OnStateExit();
            postExitActiveState(transitionTo);
            transitionTo.OnStateEnter(this);
            postEnterTransitionedState(transitionTo);
            return StateValidity.Valid;
        }

        /// <summary>
        /// Attempts to transition to another state.
        ///
        /// If the state transition is successful,
        /// then invoke the passed action after
        /// <code>OnStateEnter</code> is invoked on the new
        /// state, so that the new state can be made
        /// aware of the transition details.
        /// </summary>
        /// <param name="onTransition">the method to call after the transition</param>
        /// <typeparam name="To"> target transition type must be covariant over this type </typeparam>
        /// <returns> a state validity object indicating whether this state transition was successful or not</returns>
        public StateValidity Transition<To>(Action<To> onTransition) where To : IState
        {
            return Transition<To>(_ => {}, onTransition);
        }

        public virtual void OnStateEnter(StateMachine machine = null)
        {
            ParentStateMachine = machine;
            if (_activeStateData == null)
            {
                throw new NoActiveStateException(
                    $"Attempted to call ${nameof(IState.OnStateEnter)} on a null active state.\nConsider transitioning to, or" 
                    + " setting an active state, prior to calling tick on the state machine.");
            }
            _activeStateData.State.OnStateEnter(this);
        }

        public virtual void OnStateExit()
        {
            ParentStateMachine = null;
        }

        internal void SetActiveState(IState state, Type stateType)
        {
            if (_activeStateData == null)
            {
                _activeStateData = new ActiveStateData(state, stateType);
            }
            else
            {
                _activeStateData.ReplaceWith(state, stateType);
            }
        }
        public virtual void Tick()
        {
            if (_activeStateData == null)
            {
                throw new NoActiveStateException(
                    $"Attempted to call ${nameof(IState.Tick)} on a null active state.\nConsider transitioning to, or" 
                    + " setting an active state prior to calling tick on the state machine.");
            }
            _activeStateData.State.Tick();
        }
    }

    internal class NoSuchStateTransitionException : Exception
    {
        public NoSuchStateTransitionException(Type subtypeOf, Type toType) 
            : base($"No transition found from `{subtypeOf}` -> `{toType}`.\n" +
                   "Consider adding such a transition when you construct the state machine, " +
                   "if applicable")
        {
        }
    }

    internal class NoSuchStateException : Exception
    {
        public NoSuchStateException(string state) : base(state)
        {
        }
    }

    public interface IState
    {
        void OnStateEnter(StateMachine machine);
        void OnStateExit();
        void Tick();
        
    }

    public enum StateValidity {
        Valid,
        Invalid,
    }
    


    public sealed class UnboundedTransition<From, To> : StateTransition<From, To> where From : IState where To : IState
    {
        protected override StateValidity Validate(From @from, To to) => StateValidity.Valid;
    }


    public abstract class StateTransition
    {
        public abstract StateValidity Validate(IState from, IState to);
    }

    public abstract class StateTransition<From, To> : StateTransition where From : IState where To : IState
    {
        protected abstract StateValidity Validate(From from, To to);
        
        public sealed override StateValidity Validate(IState @from, IState to) => Validate((From) @from, (To) to);
    }


    /// <summary>
    /// A class which only allows transitions if the predicate which is
    /// passed evaluates to true.
    /// </summary>
    /// <typeparam name="From"></typeparam>
    /// <typeparam name="To"></typeparam>
    public class BooleanTransitionPredicate<From, To> : StateTransition<From, To> where From : IState where To : IState
    {
        private readonly Func<From, To, bool> condition;

        public BooleanTransitionPredicate(Func<From, To, bool> condition)
        {
            this.condition = condition;
        }

        protected override StateValidity Validate(From @from, To to)
        {
            return condition(from, to) ? StateValidity.Valid : StateValidity.Invalid;
        }
    }
}