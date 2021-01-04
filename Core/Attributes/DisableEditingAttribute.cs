// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using UnityEngine;

namespace Oni.Attributes
{
	/// <summary>
	/// Disables editing the field in the inspector but still displays it
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class DisableEditingAttribute : PropertyAttribute
	{ }
}