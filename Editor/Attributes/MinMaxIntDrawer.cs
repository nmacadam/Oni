// MIT License
// 
// Copyright (c) 2017 Denis Rizov
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

// Modified for ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEditor;
using Oni.Attributes;
using Oni.Editor;

namespace Oni.Attributes.Editor
{
	[CustomPropertyDrawer(typeof(MinMaxIntAttribute))]
	public class MinMaxIntDrawer: PropertyDrawer 
	{
		private const float _horizontalSpacing = 2.0f;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
		{
			MinMaxIntAttribute minMaxAttribute = (MinMaxIntAttribute)attribute;

			EditorGUI.BeginProperty(position, label, property);

			if (!(OniEditorUtility.GetTargetObjectOfProperty(property) is RangeInt)) 
            {
                EditorGUILayout.HelpBox("This property is only valid on RangeInt fields", MessageType.Error);
				EditorGUI.EndProperty();
                return;
            }
            
            bool isInt = OniEditorUtility.GetTargetObjectOfProperty(property) is RangeInt;

			float indentLength = GetIndentLength(position);
			float labelWidth = EditorGUIUtility.labelWidth + _horizontalSpacing;
			float floatFieldWidth = EditorGUIUtility.fieldWidth;
			float sliderWidth = position.width - labelWidth - 2.0f * floatFieldWidth;
			float sliderPadding = 5.0f;

			Rect labelRect = new Rect(
				position.x,
				position.y,
				labelWidth,
				position.height);

			Rect sliderRect = new Rect(
				position.x + labelWidth + floatFieldWidth + sliderPadding - indentLength,
				position.y,
				sliderWidth - 2.0f * sliderPadding + indentLength,
				position.height);

			Rect minFloatFieldRect = new Rect(
				position.x + labelWidth - indentLength,
				position.y,
				floatFieldWidth + indentLength,
				position.height);

			Rect maxFloatFieldRect = new Rect(
				position.x + labelWidth + floatFieldWidth + sliderWidth - indentLength,
				position.y,
				floatFieldWidth + indentLength,
				position.height);

			// Draw the label
			EditorGUI.LabelField(labelRect, label.text);

			// Draw the slider
			EditorGUI.BeginChangeCheck();

            float sliderMinInput = property.FindPropertyRelative("_min").intValue;
            float sliderMaxInput = property.FindPropertyRelative("_max").intValue;
            
			EditorGUI.MinMaxSlider(sliderRect, ref sliderMinInput, ref sliderMaxInput, minMaxAttribute.Min, minMaxAttribute.Max);

            int min = (int)sliderMinInput;
            int max = (int)sliderMaxInput;

			min = EditorGUI.IntField(minFloatFieldRect, (int)min);
			min = Mathf.Clamp(min, minMaxAttribute.Min, Mathf.Min(minMaxAttribute.Max, max));

			max = EditorGUI.IntField(maxFloatFieldRect, (int)max);
			max = Mathf.Clamp(max, Mathf.Max(minMaxAttribute.Min, min), minMaxAttribute.Max);

			if (EditorGUI.EndChangeCheck())
			{
                property.FindPropertyRelative("_min").intValue = min;
                property.FindPropertyRelative("_max").intValue = max;
			}

			EditorGUI.EndProperty();
		}

		public static float GetIndentLength(Rect sourceRect)
		{
			Rect indentRect = EditorGUI.IndentedRect(sourceRect);
			float indentLength = indentRect.x - sourceRect.x;

			return indentLength;
		}
	}
}