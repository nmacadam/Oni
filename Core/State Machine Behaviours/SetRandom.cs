// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.Attributes;
using UnityEngine;

namespace Oni.Animation
{
    /// <summary>
	/// Sets a value in the Animator's parameter list to a random number
	/// </summary>
    public class SetRandom : StateMachineBehaviour
    {
        [SerializeField] [AnimatorVariable(AnimatorControllerParameterType.Float)]
    	private string _name = default;
        [SerializeField] private StateMachineBehaviourSetValueOn _setOn = StateMachineBehaviourSetValueOn.Enter;
        [SerializeField] private float _min = 0;
        [SerializeField] private float _max = 1;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_setOn.HasFlag(StateMachineBehaviourSetValueOn.Enter))
            {
                animator.SetFloat(_name, Random.Range(_min, _max));
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (_setOn.HasFlag(StateMachineBehaviourSetValueOn.Update))
			{
				animator.SetFloat(_name, Random.Range(_min, _max));
			}
		}

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_setOn.HasFlag(StateMachineBehaviourSetValueOn.Exit))
            {
                animator.SetFloat(_name, Random.Range(_min, _max));
            }
        }
    }
}