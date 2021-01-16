// Dream Frontier, Copyright (c) DARUMA WORKS, All rights reserved.
// Author: Nathan MacAdam

using Oni.Attributes;
using UnityEngine;

namespace Oni.Animation
{
	/// <summary>
	/// Sets a parameter layer weight during a state machine event
	/// </summary>
	public class SetLayerWeight : SetParameterValue
	{
		[AnimatorLayer]
		[SerializeField] private int _layerIndex;
    	[SerializeField] private float _weight;

		private float _initial;

        protected override void RecordInitialValue(Animator animator)
        {
            _initial = animator.GetLayerWeight(_layerIndex);
        }

        protected override void ResetValue(Animator animator)
        {
            animator.SetLayerWeight(_layerIndex, _initial);
        }

        protected override void SetValue(Animator animator)
        {
            animator.SetLayerWeight(_layerIndex, _weight);
        }
    }
}