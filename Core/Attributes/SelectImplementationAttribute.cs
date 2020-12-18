// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using UnityEngine;

namespace Oni.Attributes
{
	/// <summary>
	/// Displays a type picker/creator in editor for SerializeReference fields
	/// </summary>
	public class SelectImplementationAttribute : PropertyAttribute
	{
		public Type FieldType;

		public SelectImplementationAttribute(Type fieldType)
		{
			FieldType = fieldType;
		}
	}
}