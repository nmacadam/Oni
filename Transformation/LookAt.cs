// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.Transformation
{
    /// <summary>
    /// Makes transform look at target
    /// </summary>
    public class LookAt : ExecutableBehaviour
    {
        [SerializeField] private Transform _target = default;

        public Transform Target { get => _target; set => _target = value; }

        public override void Execute()
        {
            transform.LookAt(_target.position);
        }
    }
}