// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.Internal;
using UnityEngine;

namespace Oni.Transformations
{
    /// <summary>
    /// Lerps the object's transform position to the target's transform position
    /// </summary>
    public class LerpTo : ExecutableBehaviour
    {
        [SerializeField] private Transform _target = default;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _stoppingDistance = 0f;

        public Transform Target { get => _target; set => _target = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public float StoppingDistance { get => _stoppingDistance; set => _stoppingDistance = value; }

        public override void Execute()
        {
            if (_stoppingDistance == 0)
            {
                transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _speed);
            }
            else
            {
                if ((_target.position - transform.position).sqrMagnitude < _stoppingDistance * _stoppingDistance)
                {
                    transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _speed);
                }
            }
        }
    }
}