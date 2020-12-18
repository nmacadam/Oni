// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEngine.Events;

namespace Oni.Hooks
{
    /// <summary>
    /// 
    /// </summary>
    public class AnyKeyHook : MonoBehaviour
    {
        public enum AnyKeyEventType
        {
            AnyKey, AnyKeyDown
        }

        [SerializeField] private AnyKeyEventType _eventType = default;
        [SerializeField] private UnityEvent _response = default;

        public AnyKeyEventType EventType { get => _eventType; set => _eventType = value; }
        public UnityEvent Response { get => _response; set => _response = value; }

        private void Update()
        {
            switch (_eventType)
            {
                case AnyKeyEventType.AnyKey:
                {
                    if (Input.anyKey) Response.Invoke();
                    break;
                }
                case AnyKeyEventType.AnyKeyDown:
                {
                    if (Input.anyKeyDown) Response.Invoke();
                    break;
                }
            }
        }
    }
}