// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEngine.Events;

namespace Oni.Hooks
{
    /// <summary>
    /// 
    /// </summary>
    public class OnKeyHook : MonoBehaviour
    {
        public enum KeyEventType
        {
            GetKey, GetKeyDown, GetKeyUp
        }

        [SerializeField] private KeyCode _key = default;
        [SerializeField] private KeyEventType _eventType = default;
        [SerializeField] private UnityEvent _response = default;

        public KeyCode Key { get => _key; set => _key = value; }
        public KeyEventType EventType { get => _eventType; set => _eventType = value; }
        public UnityEvent Response { get => _response; set => _response = value; }

        private void Update()
        {
            switch (_eventType)
            {
                case KeyEventType.GetKey:
                {
                    if (Input.GetKey(_key)) Response.Invoke();
                    break;
                }
                case KeyEventType.GetKeyDown:
                {
                    if (Input.GetKeyDown(_key)) Response.Invoke();
                    break;
                }
                case KeyEventType.GetKeyUp:
                {
                    if (Input.GetKeyUp(_key)) Response.Invoke();
                    break;
                }
            }
        }
    }
}