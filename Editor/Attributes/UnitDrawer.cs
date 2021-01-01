// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEditor;
using UnityEngine;
using Oni.Attributes;

namespace Oni.Editor.Attributes
{
	/// <summary>
	/// Drawer for drawing unit attribute
	/// </summary>
	[CustomPropertyDrawer(typeof(UnitAttribute), true)]
	public class UnitDrawer : PropertyDrawer
	{
		private const int _rightPadding = 2;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			UnitAttribute unit = attribute as UnitAttribute;

			Rect unitPosition = new Rect(position.x, position.y, position.width - _rightPadding, position.height);

			GUIStyle unitStyle = new GUIStyle(EditorStyles.label);
			unitStyle.alignment = TextAnchor.MiddleRight;

			EditorGUI.PropertyField(position, property);

			GUI.enabled = false;
			EditorGUI.LabelField(unitPosition, unit.unitSymbol, unitStyle);
			GUI.enabled = true;
		}
	}
}