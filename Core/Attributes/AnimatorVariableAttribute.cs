// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.Attributes
{
	/// <summary>
    /// Draws a popup list to select animator variables of the given type
    /// </summary>
    public class AnimatorVariableAttribute : PropertyAttribute
	{
		public AnimatorControllerParameterType ParameterType;

		/// <summary>
		/// Draws a popup list to select animator variables of the given type
		/// </summary>
		public AnimatorVariableAttribute(AnimatorControllerParameterType type)
		{
			this.ParameterType = type;
		}
	}
}