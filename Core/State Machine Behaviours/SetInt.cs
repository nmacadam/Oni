// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.Attributes;
using UnityEngine;

namespace Oni.Animation
{
	/// <summary>
	/// Sets a integer value in the Animator's parameter list
	/// </summary>
	public class SetInt : SetParameterValue
	{
		[SerializeField] [AnimatorVariable(AnimatorControllerParameterType.Int)]
    	private string _parameter = default;
		[SerializeField] private int _value = default;

		private int _initial;

		protected override void RecordInitialValue(Animator animator)
		{
			_initial = animator.GetInteger(_parameter);
		}

		protected override void SetValue(Animator animator)
		{
			animator.SetInteger(_parameter, _value);
		}

		protected override void ResetValue(Animator animator)
		{
			animator.SetInteger(_parameter, _initial);
		}
	}
}