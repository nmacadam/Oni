// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
    /// <summary>
    /// Defines a range of integer values from a minimum value to a maximum value
    /// </summary>
    [System.Serializable]
    public class RangeInt
	{
		[SerializeField] private int _min;
		[SerializeField] private int _max;

        public int Min { get => _min; set => _min = value; }
        public int Max { get => _max; set => _max = value; }

        public RangeInt(int min, int max)
        {
            _min = min;
            _max = max;
        }
    }
}