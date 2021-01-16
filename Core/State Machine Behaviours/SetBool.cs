// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.Attributes;
using UnityEngine;

namespace Oni.Animation
{
	/// <summary>
	/// Sets a boolean value in the Animator's parameter list
	/// </summary>
	public class SetBool : SetParameterValue
	{
		[SerializeField] [AnimatorVariable(AnimatorControllerParameterType.Bool)]
    	private string _parameter = default;
		[SerializeField] private bool _value = default;

		private bool _initial;

		protected override void RecordInitialValue(Animator animator)
		{
			_initial = animator.GetBool(_parameter);
		}

		protected override void SetValue(Animator animator)
		{
			animator.SetBool(_parameter, _value);
		}

		protected override void ResetValue(Animator animator)
		{
			animator.SetBool(_parameter, _initial);
		}
	}
}