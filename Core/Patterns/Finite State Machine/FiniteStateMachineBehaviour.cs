// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using System.Collections.Generic;
using Oni.Internal;
using UnityEngine;

namespace Oni.Patterns
{
	/// <summary>
    /// MonoBehaviour wrapper of <see cref="FiniteStateMachine"/>
    /// </summary>
	public abstract class FiniteStateMachineBehaviour<TStateEnumeration, TStateType> : ExecutableBehaviour
		where TStateEnumeration : Enum 
		where TStateType : IState<TStateEnumeration>
	{
		[SerializeField] private FiniteStateMachine<TStateEnumeration, TStateType> _fsm;

		protected abstract Dictionary<TStateEnumeration, TStateType> states { get; }
		protected abstract TStateEnumeration entryState { get; }

		private void Start()
		{
			_fsm = new FiniteStateMachine<TStateEnumeration, TStateType>(entryState, states);
		}

        public override void Execute()
        {
            _fsm.Evaluate();
        }
	}
}