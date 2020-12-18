// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.Internal;
using UnityEngine;

namespace Oni.Transformations
{
    /// <summary>
    /// Slerps the object's transform rotation to the target's transform rotation
    /// </summary>
    public class MatchTransform : ExecutableBehaviour
    {
        [SerializeField] private Transform _target = default;
        [SerializeField] private bool _matchPosition = true;
        [SerializeField] private bool _matchRotation = true;
        [SerializeField] private bool _matchScale = true;

        public Transform Target { get => _target; set => _target = value; }

        public override void Execute()
        {
            if (_matchPosition) 
            {
                transform.position = _target.position;
            }
            if (_matchRotation) 
            {
                transform.rotation = _target.rotation;
            }
            if (_matchScale) 
            {
                transform.localScale = _target.localScale;
            }
        }
    }
}