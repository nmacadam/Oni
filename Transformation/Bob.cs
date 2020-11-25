// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.Transformation
{
    /// <summary>
	/// Applies sinusoidal 'bobbing' motion in an arbitrary direction
	/// </summary>
    public class Bob : ExecutableBehaviour
    {
        [SerializeField] private float _frequency;
		[SerializeField] private float _amplitude;
		[SerializeField] private Vector3 _bobDirection = Vector3.up;
		[SerializeField] private bool _remapTo01 = false;
		[SerializeField] private bool _randomOffset = false;
		[SerializeField] private float _offset = 0;
		[SerializeField] private bool _inLocalSpace = false;

        public float Frequency { get => _frequency; set => _frequency = value; }
        public float Amplitude { get => _amplitude; set => _amplitude = value; }
        public Vector3 BobDirection { get => _bobDirection; set => _bobDirection = value; }
        public bool RemapTo01 { get => _remapTo01; set => _remapTo01 = value; }
        public bool RandomOffset { get => _randomOffset; set => _randomOffset = value; }

		private Vector3 _initalPosition;
		private float time => Time.time + _offset;

        /// <summary>
		/// Set the origin position for the bob-motion to offset from
		/// </summary>
		public void SetOrigin(Vector3 position)
		{
			_initalPosition = position;
		}

        public override void Execute()
        {
            Vector3 offset = Vector3.zero;
			
			if (_remapTo01)
			{
				offset = _bobDirection * (Mathf.Sin(time * _frequency) * .5f + .5f) * _amplitude;
			}
			else
			{
				offset = _bobDirection * Mathf.Sin(time * _frequency) * _amplitude;
			}

			if (_inLocalSpace)
			{
				transform.localPosition = _initalPosition + offset;
			}
			else
			{
				transform.position = _initalPosition + offset;
			}
        }

        private void Start()
		{
			_initalPosition = _inLocalSpace ? transform.localPosition : transform.position;

			if (RandomOffset)
			{
				_offset = Random.Range(-2 * Mathf.PI, 2 * Mathf.PI);
			}
		}
    }
}
    
