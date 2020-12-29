// Original work by Aleksander Trępała
// https://medium.com/@trepala.aleksander/serializereference-in-unity-b4ee10274f48

// Modified for ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
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