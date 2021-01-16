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
    [CustomPropertyDrawer(typeof(AnimatorVariableAttribute))]
	public class AnimatorVariableDrawer : AnimatorBasedDrawer
	{
		private AnimatorController _animatorController;

		private GUIContent _reloadIcon = null;

		private bool _hasParameters;
		private List<AnimatorControllerParameter> _parameters = new List<AnimatorControllerParameter>();
		private List<string> _options = new List<string>();
		private int _index = 0;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			AnimatorVariableAttribute variable = attribute as AnimatorVariableAttribute;

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
				if (!_hasParameters)
				{
					foreach (var parameter in _animatorController.parameters)
					{
						if (parameter.type == variable.ParameterType)
						{
							_options.Add(parameter.name);
							_parameters.Add(parameter);

							if (property.stringValue == parameter.name)
							{
								_index = _options.Count - 1;
							}
						}
					}
					_hasParameters = true;
				}

				if (_hasParameters)
				{
					var popupRect = new Rect(position.x, position.y, position.width - position.height, position.height);
					var reloadRect = new Rect(position.x + popupRect.width, position.y, position.height, position.height);
					_index = EditorGUI.Popup(popupRect, label.text, _index, _options.ToArray());

					GUIStyle style = new GUIStyle(EditorStyles.miniButton);
					style.padding = new RectOffset(1, 1, 1, 1);

					if (GUI.Button(reloadRect, _reloadIcon, style))
					{
						_animatorController = null;
					}

					if (_options.Count > 0)
					{
						property.stringValue = _options[_index];
					}
				}
			}
		}
	}
}