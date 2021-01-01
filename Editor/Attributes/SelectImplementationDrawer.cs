// Original work by Aleksander Trępała
// https://medium.com/@trepala.aleksander/serializereference-in-unity-b4ee10274f48

// Modified for ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Oni.Attributes;

namespace Oni.Editor.Attributes
{
	/// <summary>
	/// Drawer for drawing SelectImplementation attribute
	/// </summary>
	[CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
	public class SelectImplementationDrawer : PropertyDrawer
	{
		private Type[] _implementations;
		private int _implementationTypeIndex;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var addIcon = EditorGUIUtility.IconContent("Toolbar Plus");
			addIcon.tooltip = "Change the object type";

			var refreshIcon = EditorGUIUtility.IconContent("TreeEditor.Refresh");
			refreshIcon.tooltip = "Refresh the list of implementations";
 
			GUILayout.BeginVertical(EditorStyles.helpBox);
			GUILayout.BeginHorizontal(EditorStyles.toolbar);

			if (_implementations == null || GUILayout.Button(refreshIcon, EditorStyles.toolbarButton, GUILayout.Width(25)))
			{
				_implementations = GetImplementations((attribute as SelectImplementationAttribute).FieldType)
					.Where(impl => !impl.IsSubclassOf(typeof(UnityEngine.Object))).ToArray();
			}

			_implementationTypeIndex = Array.FindIndex(_implementations, item => property.managedReferenceFullTypename.Contains(item.Name));
			
			int previousIndex = _implementationTypeIndex;
			_implementationTypeIndex = EditorGUILayout.Popup(_implementationTypeIndex, _implementations.Select(impl => impl.FullName).ToArray(), EditorStyles.toolbarPopup);

			if (_implementationTypeIndex != previousIndex)
			{
				property.managedReferenceValue = Activator.CreateInstance(_implementations[_implementationTypeIndex]);
			}

			GUILayout.EndHorizontal();

			if (property.hasChildren) EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(property, true);
			if (property.hasChildren) EditorGUI.indentLevel--;

			GUILayout.Space(2);

			GUILayout.EndVertical();
		}
		
		private static Type[] GetImplementations(Type interfaceType)
		{
			var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
			return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
		}
	}
}