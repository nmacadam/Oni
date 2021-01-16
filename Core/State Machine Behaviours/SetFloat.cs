// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.Attributes;
using UnityEngine;

namespace Oni.Animation
{
	/// <summary>
	/// Sets a float value in the Animator's parameter list
	/// </summary>
	public class SetFloat : SetParameterValue
	{
		[SerializeField] [AnimatorVariable(AnimatorControllerParameterType.Float)]
    	private string _parameter = default;
		[SerializeField] private float _value = default;

		private float _initial;

		protected override void RecordInitialValue(Animator animator)
		{
			_initial = animator.GetFloat(_parameter);
		}

		protected override void SetValue(Animator animator)
		{
			animator.SetFloat(_parameter, _value);
		}

		protected override void ResetValue(Animator animator)
		{
			animator.SetFloat(_parameter, _initial);
		}
	}
}