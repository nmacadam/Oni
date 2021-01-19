// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.Attributes;
using UnityEditor;
using UnityEngine;

namespace Oni.Editor.Attributes
{
	[CustomPropertyDrawer(typeof(OptionalAttribute))]
	public class OptionalDrawer: PropertyDrawer 
	{
		private const int _heightPadding = 0;
		private const int _sizePadding = 2;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
		{
			var icon = EditorGUIUtility.IconContent("_Help");
			icon.tooltip = "This field is optional";

			Rect iconPosition = new Rect(EditorGUIUtility.labelWidth - 1, position.y + _heightPadding, icon.image.height + _sizePadding, icon.image.height + _sizePadding);

			EditorGUI.PropertyField(position, property);
			EditorGUI.LabelField(iconPosition, icon);
		}
	}
}