// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.Internal;
using UnityEngine;

namespace Oni
{
    /// <summary>
    /// Sets the transform's parent to null when activated
    /// </summary>
    public class Unparent : ActivatedBehaviour
    {
        public override void Activate()
        {
            transform.parent = null;
        }
    }
}