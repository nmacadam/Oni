// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using System.Collections.Generic;
using Oni.Internal;

namespace Oni.Patterns
{
    /// <summary>
    /// Generic class for creating enumeration based finite state machines
    /// </summary>
    public abstract class FiniteStateMachine<TStateEnumeration, TStateType> : ExecutableBehaviour where TStateEnumeration : Enum where TStateType : IState<TStateEnumeration>
	{
		// Properties
		public TStateType ActiveState => _activeState;

		// Callbacks
		/// <summary>
		/// Fired before a state transition occurs
		/// </summary>
		public Action<TStateEnumeration, TStateEnumeration> OnBeforeStateTranstion = delegate { };
		/// <summary>
		/// Fired after a state transition has occured
		/// </summary>
		public Action<TStateEnumeration, TStateEnumeration> OnPostStateTranstion = delegate { };

		// Require default values in concrete implementation
		protected abstract Dictionary<TStateEnumeration, TStateType> states { get; }
		protected abstract TStateEnumeration entryState { get; }

		// Internal types and data structures
		private TStateType _activeState;

		private struct Transition 
		{
			public TStateEnumeration FromState;
			public TStateEnumeration ToState;
			public Func<TStateEnumeration, bool> Condition;
		}

        private struct AnyTransition 
		{
			public TStateEnumeration ToState;
			public Func<TStateEnumeration, bool> Condition;
		}

		private List<Transition> _transitions = new List<Transition>();
        private List<AnyTransition> _anyTransitions = new List<AnyTransition>();

		/// <summary>
		/// Force a transition to the given state
		/// </summary>
		/// <param name="stateType">The enumeration value representing the requested state</param>
		public void TransitionTo(TStateEnumeration stateType)
		{
			var from = _activeState.State;

			OnBeforeStateTranstion.Invoke(from, stateType);
			_activeState = states[stateType];
			OnPostStateTranstion.Invoke(from, stateType);
		}

		/// <summary>
		/// Defines an automated transition condition from a given state to a given state
		/// </summary>
		/// <param name="fromState">The enumeration value representing the state to transition from</param>
		/// <param name="toState">The enumeration value representing the state to transition to</param>
		/// <param name="condition">Boolean expression that specifies if transition conditions are met</param>
		public void DefineTransition(TStateEnumeration fromState, TStateEnumeration toState, Func<TStateEnumeration, bool> condition)
		{
			_transitions.Add(new Transition {
				FromState = fromState,
				ToState = toState,
				Condition = condition,
			});
		}

		/// <summary>
		/// Defines an automated transition condition from any state to a given state
		/// </summary>
		/// <param name="toState">The enumeration value representing the state to transition to</param>
		/// <param name="condition">Boolean expression that specifies if transition conditions are met</param>
        public void DefineAnyTransition(TStateEnumeration toState, Func<TStateEnumeration, bool> condition)
		{
			_anyTransitions.Add(new AnyTransition {
				ToState = toState,
				Condition = condition,
			});
		}

		/// <summary>
		/// Checks defined transition conditions and updates the state if any are valid
		/// </summary>
		/// <remarks>
		/// This method will typically be automatically executed based on the <see cref="UpdateMethod"/>.
		/// Call manually if <see cref="UpdateMethod"/> is set to External
		/// </remarks>
        public override void Execute()
        {
			// Check for from-to transitions
            foreach (var transition in _transitions)
            {
				// generics prevent just saying 'transition.FromState != _activeState.State'
				if (!EqualityComparer<TStateEnumeration>.Default.Equals(transition.FromState, _activeState.State))
				{
					continue;
				}
                else if (transition.Condition.Invoke(_activeState.State))
				{
					TransitionTo(transition.ToState);
					break;
				}
            }

			// check for any transitions
            foreach (var anyTransition in _anyTransitions)
            {
                if (anyTransition.Condition.Invoke(_activeState.State))
				{
					TransitionTo(anyTransition.ToState);
					break;
				}
            }
        }

        protected virtual void Awake() 
		{
			// Set the state machine to the entry state
			_activeState = states[entryState];
		}
	}
}