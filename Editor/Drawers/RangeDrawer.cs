// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEditor;

namespace Oni.Editor
{
	[CustomPropertyDrawer(typeof(Range))]
	[CustomPropertyDrawer(typeof(RangeInt))]
	public class RangeDrawer: PropertyDrawer 
	{
		public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label) 
		{
			label = EditorGUI.BeginProperty(pos, label, prop);

			var labels = new[] { new GUIContent("Min"), new GUIContent("Max") };
			var properties = new[] { prop.FindPropertyRelative("_min"), prop.FindPropertyRelative("_max") };
			OniGUI.DrawMultiFieldProperty(pos, label, labels, properties);
	
			EditorGUI.EndProperty();
		}
	}
}