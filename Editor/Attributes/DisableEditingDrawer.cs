// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEditor;
using UnityEngine;
using Oni.Attributes;

namespace Oni.Editor.Attributes
{
	/// <summary>
	/// Drawer for drawing DisableEditing attribute
	/// </summary>
	[CustomPropertyDrawer(typeof(DisableEditingAttribute))]
	public class DisableEditingDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			GUI.enabled = false;

			EditorGUI.PropertyField(position, property, label);

			GUI.enabled = true;
		}
	}
}