// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.Attributes
{
	/// <summary>
	/// Adds a unit suffix to the value in the inspector (ex. cm, ft, %)
	/// </summary>
	public class UnitAttribute : PropertyAttribute
	{
		public string unitSymbol;

		public UnitAttribute(string unitSymbol)
		{
			this.unitSymbol = unitSymbol;
		}
	}
}