// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEngine.Events;

namespace Oni.Hooks
{
    public class CollisionUnityEvent : UnityEvent<Collision> {}

    /// <summary>
    /// 
    /// </summary>
    public class OnCollisionHook : MonoBehaviour
    {
        [SerializeField] private CollisionUnityEvent _enterResponse = default;
        [SerializeField] private CollisionUnityEvent _stayResponse = default;
        [SerializeField] private CollisionUnityEvent _exitResponse = default;

        public CollisionUnityEvent EnterResponse { get => _enterResponse; set => _enterResponse = value; }
        public CollisionUnityEvent StayResponse { get => _stayResponse; set => _stayResponse = value; }
        public CollisionUnityEvent ExitResponse { get => _exitResponse; set => _exitResponse = value; }

        private void OnCollisionEnter(Collision other)
        {
            EnterResponse.Invoke(other);
        }

        private void OnCollisionStay(Collision other) 
        {
            StayResponse.Invoke(other);
        }

        private void OnCollisionExit(Collision other) 
        {
            ExitResponse.Invoke(other);
        }
    }
}