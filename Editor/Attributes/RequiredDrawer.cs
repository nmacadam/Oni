// Dream Frontier, Copyright (c) DARUMA WORKS, All rights reserved.
// Author: Nathan MacAdam

using Oni.Attributes;
using UnityEditor;
using UnityEngine;

namespace Oni.Editor.Attributes
{
	[CustomPropertyDrawer(typeof(RequiredAttribute))]
	public class RequiredDrawer: PropertyDrawer 
	{
		private const int _heightPadding = 0;
		private const int _sizePadding = 2;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
		{
			var icon = EditorGUIUtility.IconContent("console.erroricon.sml");
			icon.tooltip = "This field is required";

			Rect iconPosition = new Rect(EditorGUIUtility.labelWidth, position.y + _heightPadding, icon.image.height + _sizePadding, icon.image.height + _sizePadding);

			EditorGUI.PropertyField(position, property);
			if (property.propertyType == SerializedPropertyType.ExposedReference || 
				property.propertyType == SerializedPropertyType.ManagedReference || 
				property.propertyType == SerializedPropertyType.ObjectReference)
			{
				if (property.objectReferenceValue == null)
				{
					EditorGUI.LabelField(iconPosition, icon);
				}
			}
		}
	}
}