﻿// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using UnityEditor;
using UnityEngine;

namespace Oni.Editor
{
	/// <summary>
	/// More GUI functions for the Unity Editor
	/// </summary>
	public static class OniGUI
	{
		#region Drawing

		/// <summary>
		/// Draws a property with multiple property fields for sub-properties
		/// </summary>
		/// <param name="label">The main property label</param>
		/// <param name="subLabels">The sub-property labels</param>
		/// <param name="properties">The serialized sub-properties</param>
		public static void DrawMultiFieldProperty(Rect pos, GUIContent label, GUIContent[] subLabels, SerializedProperty[] properties)
		{
			float subLabelSpacing = 4f;
			var contentRect = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);
			DrawMultiplePropertyFields(contentRect, subLabels, properties, subLabelSpacing);
		}

		/// <summary>
		/// Draws multiple property fields within one rect
		/// </summary>
		/// <param name="pos">Rect to draw in</param>
		/// <param name="subLabels">Labels for content</param>
		/// <param name="props">Content serialized properties</param>
		/// <param name="subLabelSpacing">Spacing between content</param>
		public static void DrawMultiplePropertyFields(Rect pos, GUIContent[] subLabels, SerializedProperty[] props, float subLabelSpacing = 4f) 
		{
			// backup gui settings
			var indent     = EditorGUI.indentLevel;
			var labelWidth = EditorGUIUtility.labelWidth;
		
			// draw properties
			var propsCount = props.Length;
			var width      = (pos.width - (propsCount - 1) * subLabelSpacing) / propsCount;
			var contentPos = new Rect(pos.x, pos.y, width, pos.height);
			EditorGUI.indentLevel = 0;
			for (var i = 0; i < propsCount; i++) 
			{
				EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(subLabels[i]).x;
				EditorGUI.PropertyField(contentPos, props[i], subLabels[i]);
				contentPos.x += width + subLabelSpacing;
			}
	
			// restore gui settings
			EditorGUIUtility.labelWidth = labelWidth;
			EditorGUI.indentLevel       = indent;
		}

		/// <summary>
		/// Draws a horizontal divider bar
		/// </summary>
		/// <param name="pos">Rect to draw in</param>
		public static void Divider(Rect pos)
		{
        	EditorGUI.LabelField(pos, "", GUI.skin.horizontalSlider);
		}

		/// <summary>
		/// Draws the default 'Script' reference field
		/// </summary>
		/// <param name="rect">The rect to draw this field in</param>
		/// <param name="target">The MonoBehaviour for this component</param>
		/// <param name="type">The script object type</param>
		public static void ScriptField(Rect rect, MonoBehaviour target, Type type)
		{
			using (new EditorGUI.DisabledScope(true))
			{
				EditorGUI.ObjectField(rect, "Script", MonoScript.FromMonoBehaviour(target), type, false);
			}
		}

		#endregion

		#region Formatting

		public static Rect GetRectByLineNumber(Rect original, int line)
		{
			float heightModifier = (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * (line - 1);

			return new Rect(
				original.x,
				original.y + heightModifier,
				original.width,
				EditorGUIUtility.singleLineHeight);
		}

		#endregion
	}
}