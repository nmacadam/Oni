// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.Attributes;
using UnityEngine;

namespace Oni.Animation
{
	/// <summary>
	/// Base class for constructing a StateMachineBehaviour that sets a parameter
	/// value during a state machine event
	/// </summary>
	public abstract class SetParameterValue : StateMachineBehaviour
	{
		[SerializeField] private StateMachineBehaviourSetValueOn _setOn = StateMachineBehaviourSetValueOn.Enter;

		[Tooltip("Should the value be set back to what it initially was on exit?")]
		[SerializeField] private bool _reset = false;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			RecordInitialValue(animator);

			if (_setOn.HasFlag(StateMachineBehaviourSetValueOn.Enter))
			{
				SetValue(animator);
			}
		}
		
		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (_setOn.HasFlag(StateMachineBehaviourSetValueOn.Update))
			{
				SetValue(animator);
			}
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
		{
			if (_setOn.HasFlag(StateMachineBehaviourSetValueOn.Exit))
			{
				SetValue(animator);
			}

			if (_reset)
			{
			    ResetValue(animator);
			}
		}

		protected abstract void RecordInitialValue(Animator animator);
		protected abstract void SetValue(Animator animator);
		protected abstract void ResetValue(Animator animator);

		private void OnValidate()
		{
			if (_setOn.HasFlag(StateMachineBehaviourSetValueOn.Exit) && _reset)
			{
				Debug.LogWarning("State machine behaviour will set on exit and reset to it's default value on exit!", this);
			}
		}
	}
}