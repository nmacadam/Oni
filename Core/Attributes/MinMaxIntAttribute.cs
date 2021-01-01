// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using UnityEngine;

namespace Oni.Attributes
{
	/// <summary>
	/// Displays a min-max integer slider in the given range
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class MinMaxIntAttribute : PropertyAttribute
	{
		private int _min;
		private int _max;

        public int Min { get => _min; set => _min = value; }
        public int Max { get => _max; set => _max = value; }

		public MinMaxIntAttribute(int min, int max)
		{
			_min = min;
			_max = max;
		}
    }
}