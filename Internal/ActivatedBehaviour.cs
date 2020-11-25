// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.Internal
{
    /// <summary>
    /// Activation Unity events
    /// </summary>
    public enum ActivationMethod
    {
        Awake,
        Start,
        OnEnable,
        External
    }

    /// <summary>
    /// 'Activate' method is called during the corresponding ActivationMethod event. 
    /// This class just encasulates the condition checking for each activation type to keep things clean
    /// </summary>
    public abstract class ActivatedBehaviour : MonoBehaviour 
    {
        [SerializeField] private ActivationMethod _activationMethod = default;

        public ActivationMethod ActivationMethod { get => _activationMethod; set => _activationMethod = value; }

        public abstract void Activate();

        private void Awake()
		{
            if (ActivationMethod == ActivationMethod.Awake)
            {
                Activate();
            }
		}

        private void Start() 
        {
            if (ActivationMethod == ActivationMethod.Start)
            {
                Activate();
            }
        }

        private void OnEnable() 
        {
            if (ActivationMethod == ActivationMethod.OnEnable)
            {
                Activate();
            }
        }
    }
}