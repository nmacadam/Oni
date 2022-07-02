// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEditor;
using Oni.UI;
using UnityEngine;

namespace Oni.Editor
{
    [CustomEditor(typeof(GraphicRaycatcher))]
    public class GraphicRaycatcherEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            OniGUILayout.ScriptField((MonoBehaviour)target, GetType());
        }
    }
}