// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

namespace Oni.Animation
{
    /// <summary>
    /// Enumerates StateMachineBehaviour events
    /// </summary>
    [System.Flags]
    public enum StateMachineBehaviourSetValueOn
    {
        Everything = ~0,
        Enter = 1,
        Update = 2,
        Exit = 4
    }
}