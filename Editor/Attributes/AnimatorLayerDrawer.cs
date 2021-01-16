// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEditor;
using Oni.Attributes;
using UnityEditor.Animations;
using System.Collections.Generic;

namespace Oni.Editor.Attributes
{
	/// <summary>
    /// Draws a popup list to select animator variables of the given type
    /// </summary>
    [CustomPropertyDrawer(typeof(AnimatorLayerAttribute))]
	public class AnimatorLayerDrawer : AnimatorBasedDrawer
	{
		private AnimatorController _animatorController;

		private GUIContent _reloadIcon = null;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (_reloadIcon == null) 
			{
				_reloadIcon = EditorGUIUtility.IconContent("Refresh");
			}

			if (_animatorController == null)
			{
				if (!TryGetAnimatorController(ref _animatorController))
				{
					EditorGUILayout.HelpBox("Failed to retrieve Animator Controller", MessageType.Error);
				}
			}

			if (_animatorController != null)
			{
				var popupRect = new Rect(position.x, position.y, position.width - position.height, position.height);
				var reloadRect = new Rect(position.x + popupRect.width, position.y, position.height, position.height);

				string[] layerNames = new string[_animatorController.layers.Length];
				for (int i = 0; i < layerNames.Length; i++) 
				{
					layerNames[i] = _animatorController.layers[i].name;
				}
		
				property.intValue = EditorGUI.Popup(popupRect, label.text, property.intValue, layerNames);

				GUIStyle style = new GUIStyle(EditorStyles.miniButton);
				style.padding = new RectOffset(1, 1, 1, 1);

				if (GUI.Button(reloadRect, _reloadIcon, style))
				{
					_animatorController = null;
					return;
				}
			}
		}
	}
}