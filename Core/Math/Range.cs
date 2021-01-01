// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
    /// <summary>
    /// Defines a range of values from a minimum value to a maximum value
    /// </summary>
    [System.Serializable]
    public class Range
	{
		[SerializeField] private float _min;
		[SerializeField] private float _max;

        public float Min { get => _min; set => _min = value; }
        public float Max { get => _max; set => _max = value; }

        public Range(float min, float max)
        {
            _min = min;
            _max = max;
        }
    }
}