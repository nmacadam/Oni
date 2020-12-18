// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEngine.Events;

namespace Oni.Hooks
{
    public class ColliderUnityEvent : UnityEvent<Collider> {}

    /// <summary>
    /// 
    /// </summary>
    public class OnTriggerHook : MonoBehaviour
    {
        [SerializeField] private ColliderUnityEvent _enterResponse = default;
        [SerializeField] private ColliderUnityEvent _stayResponse = default;
        [SerializeField] private ColliderUnityEvent _exitResponse = default;

        public ColliderUnityEvent EnterResponse { get => _enterResponse; set => _enterResponse = value; }
        public ColliderUnityEvent StayResponse { get => _stayResponse; set => _stayResponse = value; }
        public ColliderUnityEvent ExitResponse { get => _exitResponse; set => _exitResponse = value; }

        private void OnTriggerEnter(Collider other) 
        {
            EnterResponse.Invoke(other);
        }

        private void OnTriggerStay(Collider other) 
        {
            StayResponse.Invoke(other);
        }

        private void OnTriggerExit(Collider other) 
        {
            ExitResponse.Invoke(other);
        }
    }
}