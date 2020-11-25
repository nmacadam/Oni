// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using UnityEngine;

namespace Oni.SceneManagement
{
    /// <summary>
    /// Base class for implementing a scene transition
    /// </summary>
    public abstract class SceneTransition : MonoBehaviour
    {
        /// <summary>
        /// Executed when scene transitions in
        /// </summary>
        public abstract void TransitionIn(Action onVisible);

        /// <summary>
        /// Executed when scene transitions out
        /// </summary>
        public abstract void TransitionOut(Action onObscured);
    }
}