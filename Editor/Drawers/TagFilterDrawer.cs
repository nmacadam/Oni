// Dream Frontier, Copyright (c) DARUMA WORKS, All rights reserved.
// Author: Nathan MacAdam

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Oni.Editor
{
	[CustomPropertyDrawer(typeof(TagFilter))]
	public class TagFilterDrawer: PropertyDrawer 
	{
		private ReorderableList _list = null; 
		
		private SerializedProperty _requirments = null;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{ 
			return -2.0f; 
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
		{
			if (_requirments == null)
			{
				_requirments = property.FindPropertyRelative("_require");
			}

			if (_list == null)
			{
				_list = new ReorderableList(property.serializedObject, property.FindPropertyRelative("_checkForTags"), true, true, true, true);
				_list.drawElementCallback = DrawListItems;
				_list.drawHeaderCallback = DrawHeader;
			}

			_list.DoLayoutList();
		}

		void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
		{
			SerializedProperty element = _list.serializedProperty.GetArrayElementAtIndex(index);
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, rect.width, 2 + EditorGUIUtility.singleLineHeight),
				element,
				GUIContent.none
			);
		}

		void DrawHeader(Rect rect)
		{
			float requirementWidth = 60f;
			float requirementPercent = requirementWidth / rect.width;

			var labelRect = new Rect(rect.x, rect.y, rect.width * (1 - requirementPercent), rect.height);
			var requirementRect = new Rect((rect.width * (1 - requirementPercent)) + rect.x, rect.y, rect.width * requirementPercent, rect.height);

			EditorGUI.LabelField(labelRect, "Tag Filter");
			EditorGUI.PropertyField(requirementRect, _requirments, GUIContent.none);
		}
	}
}