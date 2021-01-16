// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.Attributes;
using UnityEngine;

namespace Oni.Animation
{
	/// <summary>
	/// Sets a trigger in the Animator's parameter list
	/// </summary>
	public class SetTrigger : StateMachineBehaviour
	{
		[SerializeField] [AnimatorVariable(AnimatorControllerParameterType.Trigger)]
    	private string _name = default;
		[SerializeField] private StateMachineBehaviourSetValueOn _setOn = StateMachineBehaviourSetValueOn.Enter;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
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
		}

		protected void SetValue(Animator animator)
		{
			animator.SetTrigger(_name);
		}
	}
}