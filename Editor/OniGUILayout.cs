// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

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
	}
}