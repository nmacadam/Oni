// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.Internal;
using UnityEngine;

namespace Oni.Transformations
{
    /// <summary>
    /// Slerps the object's transform rotation to the target's transform rotation
    /// </summary>
    public class SlerpTo : ExecutableBehaviour
    {
        [SerializeField] private Transform _target = default;
        [SerializeField] private float _speed = 1f;

        public Transform Target { get => _target; set => _target = value; }
        public float Speed { get => _speed; set => _speed = value; }

        public override void Execute()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _target.rotation, Time.deltaTime * _speed);
        }
    }
}