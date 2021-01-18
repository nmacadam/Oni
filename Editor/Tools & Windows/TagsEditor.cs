// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Oni.Editor
{
	[CustomEditor(typeof(Tags))]
	public class TagsEditor : UnityEditor.Editor 
	{
		private SerializedProperty _tags;
		private SerializedProperty _type;

		private ReorderableList _list; 
		private void OnEnable()
		{
			_tags = serializedObject.FindProperty("_tags");
			_list = new ReorderableList(serializedObject, _tags, true, false, true, true);

			_list.drawElementCallback = DrawListItems;
			_list.headerHeight = 10;
		}

		public override void OnInspectorGUI() 
		{
			serializedObject.Update();
			_list.DoLayoutList();
			serializedObject.ApplyModifiedProperties();
		}

		void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
		{
			SerializedProperty element = _list.serializedProperty.GetArrayElementAtIndex(index);
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
				element, 
				GUIContent.none
			);
		}
	}
}