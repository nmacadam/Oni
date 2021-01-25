// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEditor;
using UnityEngine;
using Oni.Attributes;
using System.Collections.Generic;

namespace Oni.Editor.Attributes
{
	/// <summary>
	/// Drawer for drawing Unwrap attribute
	/// </summary>
	[CustomPropertyDrawer(typeof(UnwrapAttribute))]
	public class UnwrapDrawer: PropertyDrawer 
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
		{
			EditorGUI.LabelField(position, label);

			var children = property.GetChildren();

			EditorGUI.indentLevel++;
			foreach (var child in children)
			{
				EditorGUILayout.PropertyField(child);
			}
			EditorGUI.indentLevel--;
		}
	}
}