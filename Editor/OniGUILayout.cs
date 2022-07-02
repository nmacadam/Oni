// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using UnityEditor;
using UnityEngine;

namespace Oni.Editor
{
	/// <summary>
	/// More auto-layout GUI functions for the Unity Editor
	/// </summary>
	public static class OniGUILayout
	{
		/// <summary>
		/// Draws a horizontal divider bar
		/// </summary>
		public static void Divider()
		{
        	EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		}
		
		/// <summary>
		/// Draws the default 'Script' reference field
		/// </summary>
		/// <param name="target">The MonoBehaviour for this component</param>
		/// <param name="type">The script object type</param>
		public static void ScriptField(MonoBehaviour target, Type type)
		{
			using (new EditorGUI.DisabledScope(true))
			{
				EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(target), type, false);
			}
		}
	}
}