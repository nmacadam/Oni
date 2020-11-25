// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.Transformation
{
    /// <summary>
    /// Makes transform look at main camera (or specified camera)
    /// </summary>
    public class LookAtCamera : ExecutableBehaviour
    {
        [SerializeField] private Transform _cameraTransform = default;
        [SerializeField] private bool _invertForward = false;

        public Transform Target { get => _cameraTransform; set => _cameraTransform = value; }
        public bool InvertForward { get => _invertForward; set => _invertForward = value; }

        private void Awake() 
        {
            if (_cameraTransform == null)
            {
                _cameraTransform = Camera.main.transform;
            }
        }

        public override void Execute()
        {
            Vector3 forward = _invertForward ? transform.position - _cameraTransform.position : _cameraTransform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        }
    }
}