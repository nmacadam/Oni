// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEditor;
using UnityEngine;

namespace Oni.Attributes.Editor
{
	/// <summary>
	/// Drawer for drawing LockInPlayMode attribute
	/// </summary>
	[CustomPropertyDrawer(typeof(LockInPlayModeAttribute), true)]
	public class LockInPlayModeDrawer : PropertyDrawer
	{
		private const int _heightPadding = 4;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var lockOn = EditorGUIUtility.IconContent("LockIcon-On");
			var lockOff = EditorGUIUtility.IconContent("LockIcon");

			lockOn.tooltip = "This field is locked during Play Mode";
			lockOff.tooltip = "This field will be locked during Play Mode";

			GUIStyle lockStyle = new GUIStyle();
			lockStyle.padding = new RectOffset(0, 0, 0, 0);

			Rect iconPosition = new Rect(EditorGUIUtility.labelWidth, position.y + _heightPadding, lockOn.image.height, lockOn.image.height);

			if (EditorApplication.isPlaying)
			{
				GUI.enabled = false;
			}

			EditorGUI.PropertyField(position, property);

			// Unity handles lists differently than normal fields; using the LabelField approach doesn't work for lists.

			GUI.DrawTexture(iconPosition, EditorApplication.isPlaying ? lockOn.image : lockOff.image);
			//EditorGUI.LabelField(iconPosition, EditorApplication.isPlaying ? lockOn : lockOff, lockStyle);

			if (EditorApplication.isPlaying)
			{
				GUI.enabled = true;
			}
		}
	}
}