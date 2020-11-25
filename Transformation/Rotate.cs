// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.Transformation
{
    /// <summary>
    /// Applies constant rotation to the object
    /// </summary>
    public class Rotate : ExecutableBehaviour
    {
        [SerializeField] private Vector3 _rotation;
		[SerializeField] private Space _relativeTo = Space.Self;

        public Vector3 Rotation { get => _rotation; set => _rotation = value; }
        public Space RelativeTo { get => _relativeTo; set => _relativeTo = value; }

        public override void Execute()
        {
            transform.Rotate(_rotation * Time.deltaTime, RelativeTo);
        }
    }
}