// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using UnityEngine;

namespace Oni.Attributes
{
	/// <summary>
	/// Displays a min-max slider in the given range
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class MinMaxAttribute : PropertyAttribute
	{
		private float _min;
		private float _max;

        public float Min { get => _min; set => _min = value; }
        public float Max { get => _max; set => _max = value; }

		public MinMaxAttribute(float min, float max)
		{
			_min = min;
			_max = max;
		}
    }
}