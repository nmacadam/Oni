// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using NUnit.Framework;
using UnityEngine;

namespace Oni.TestUtilities
{
    public class WaitWhileOrAssertFailAfter : CustomYieldInstruction
    {
        private System.Func<bool> _predicate;
        private float _timeOutTime;

		public override bool keepWaiting
        {
            get
            {
                bool valid = Time.realtimeSinceStartup < _timeOutTime && _predicate(); 

                if (!valid)
                {
                    Assert.Fail($"Coroutine timed out and failed after {_timeOutTime}s");
                }

                return valid;
            }
            
        }

        /// <summary>
        /// Wait while predicate is false or until the yield instruction times out and fails
        /// </summary>
        /// <param name="predicate">The predicate to evaluate</param>
        /// <param name="timeOutAfter">The duration before the yield instruction times out and fails</param>
        public WaitWhileOrAssertFailAfter(System.Func<bool> predicate, float timeOutAfter)
        {
            _predicate = predicate;
            _timeOutTime = Time.realtimeSinceStartup + timeOutAfter;
        }
    }

    public class WaitUntilOrAssertFailAfter : CustomYieldInstruction
    {
        private System.Func<bool> _predicate;
        private float _timeOutTime;

        public override bool keepWaiting
        {
            get
            {
                bool valid = Time.realtimeSinceStartup < _timeOutTime && !_predicate(); 

                if (!valid)
                {
                    Assert.Fail($"Coroutine timed out and failed after {_timeOutTime}s");
                }

                return valid;
            }
            
        }

        /// <summary>
        /// Wait until predicate is true or the yield instruction times out and fails
        /// </summary>
        /// <param name="predicate">The predicate to evaluate</param>
        /// <param name="timeOutAfter">The duration before the yield instruction times out and fails</param>
        public WaitUntilOrAssertFailAfter(System.Func<bool> predicate, float timeOutAfter)
        {
            _predicate = predicate;
            _timeOutTime = Time.realtimeSinceStartup + timeOutAfter;
        }
    }
}