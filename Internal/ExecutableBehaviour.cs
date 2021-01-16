// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.Internal
{
    /// <summary>
    /// Update Unity events
    /// </summary>
    public enum UpdateMethod
    {
        Default,
        Fixed,
        Late,
        External
    }

    /// <summary>
    /// 'Execute' method is called during the corresponding UpdateMethod event. 
    /// This class just encasulates the condition checking for each update type to keep things clean
    /// </summary>
    public abstract class ExecutableBehaviour : MonoBehaviour 
    {
        [SerializeField] private UpdateMethod _updateMethod = default;

        public UpdateMethod UpdateMethod { get => _updateMethod; set => _updateMethod = value; }

        public abstract void Execute();

        private void Update()
        {
            if (UpdateMethod == UpdateMethod.Default) 
            {
                Execute();
            }
        }

        private void FixedUpdate()
        {
            if (UpdateMethod == UpdateMethod.Fixed) 
            {
                Execute();
            }
        }

        private void LateUpdate() 
        {
            if (UpdateMethod == UpdateMethod.Late)
            {
                Execute();
            }
        }
    }
}