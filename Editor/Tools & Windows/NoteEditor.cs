// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System;
using Oni.Tools;

namespace Oni.Editor
{
	[CustomEditor(typeof(Note))]
	public class NoteEditor : UnityEditor.Editor 
	{
		private SerializedProperty _message;
		private SerializedProperty _type;

		private GUIContent _infoIcon;
		private GUIContent _warningIcon;
		private GUIContent _errorIcon;

		private void OnEnable()
		{
			_message = serializedObject.FindProperty("_message");
			_type = serializedObject.FindProperty("_type");

			_infoIcon = EditorGUIUtility.IconContent("console.infoicon.inactive.sml");
			_warningIcon = EditorGUIUtility.IconContent("console.warnicon.inactive.sml");
			_errorIcon = EditorGUIUtility.IconContent("console.erroricon.inactive.sml");
		}

		public override void OnInspectorGUI() 
		{
			EditorGUILayout.BeginVertical("box");

			EditorGUILayout.BeginHorizontal();

			DrawIcon();

			EditorGUILayout.LabelField(_message.stringValue, EditorStyles.wordWrappedLabel);

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.EndVertical();
		}

		private void DrawIcon()
		{
			switch ((Note.NoteType)_type.enumValueIndex)
			{
				case Note.NoteType.None: return;
				case Note.NoteType.Info: GUILayout.Label(_infoIcon); return;
				case Note.NoteType.Warning: GUILayout.Label(_warningIcon); return;
				case Note.NoteType.Error: GUILayout.Label(_errorIcon); return;
			}
		}
	}
}