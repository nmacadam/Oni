// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System.Collections;
using UnityEngine;

namespace Oni
{
    public static class Coroutines
    {
        public static IEnumerator ScheduleAction(float delay, System.Action action, bool realTime = false)
        {
            if (!realTime) yield return new WaitForSeconds(delay);
            else yield return new WaitForSecondsRealtime(delay);
            action.Invoke();
        }

        public static IEnumerator ExecuteWhen(System.Func<bool> predicate, System.Action onConditionMet)
		{
			yield return new WaitUntil(predicate);
			onConditionMet();
		}

        public static IEnumerator ExecuteWhenOrTimeOut(System.Func<bool> predicate, float timeOutAfter, System.Action onConditionMet)
		{
			yield return new WaitUntilOrTimeOut(predicate, timeOutAfter);
			onConditionMet();
		}
    }

    public class WaitUntilOrTimeOut : CustomYieldInstruction
    {
        private System.Func<bool> _predicate;
        private float _timeOutTime;

        public override bool keepWaiting => Time.realtimeSinceStartup < _timeOutTime && !_predicate();

        /// <summary>
        /// Wait until predicate is true or the yield instruction times out
        /// </summary>
        /// <param name="predicate">The predicate to evaluate</param>
        /// <param name="timeOutAfter">The duration before the yield instruction times out and continues</param>
        public WaitUntilOrTimeOut(System.Func<bool> predicate, float timeOutAfter)
        {
            _predicate = predicate;
            _timeOutTime = Time.realtimeSinceStartup + timeOutAfter;
        }
    }

    public class WaitWhileOrTimeOut : CustomYieldInstruction
    {
        private System.Func<bool> _predicate;
        private float _timeOutTime;

        public override bool keepWaiting => Time.realtimeSinceStartup < _timeOutTime && _predicate();

        /// <summary>
        /// Wait while predicate is false or until the yield instruction times out
        /// </summary>
        /// <param name="predicate">The predicate to evaluate</param>
        /// <param name="timeOutAfter">The duration before the yield instruction times out and continues</param>
        public WaitWhileOrTimeOut(System.Func<bool> predicate, float timeOutAfter)
        {
            _predicate = predicate;
            _timeOutTime = Time.realtimeSinceStartup + timeOutAfter;
        }
    }
}