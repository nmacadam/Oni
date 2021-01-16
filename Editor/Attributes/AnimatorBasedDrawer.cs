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
	public abstract class AnimatorBasedDrawer : PropertyDrawer
	{
		public virtual bool TryGetAnimatorController(ref AnimatorController controller)
		{
			if (Selection.assetGUIDs.Length == 0)
			{
				return false;
			}

			var guid = Selection.assetGUIDs[0];
			var path = AssetDatabase.GUIDToAssetPath(guid);

			controller = (AnimatorController)AssetDatabase.LoadAssetAtPath(path, typeof(AnimatorController));

			if (controller == null)
			{
				return false;
			}

			return true;
		}
	}
}