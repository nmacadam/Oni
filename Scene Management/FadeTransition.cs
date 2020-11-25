// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System.Collections;
using UnityEngine;

namespace Oni.SceneManagement.Transitions
{
    /// <summary>
    /// Fade in/out transition
    /// </summary>
    public class FadeTransition : SceneTransition
    {
        [SerializeField] private float _fadeDuration = 1f;
        [SerializeField] private CanvasGroup _canvasGroup = default;

        public CanvasGroup CanvasGroup { get => _canvasGroup; set => _canvasGroup = value; }

        public override void TransitionIn(System.Action onVisible)
        {
            _canvasGroup.alpha = 1;
            StartCoroutine(FadeRoutine(1, 0, _fadeDuration, onVisible));
        }

        public override void TransitionOut(System.Action onObscured)
        {
            _canvasGroup.alpha = 0;
            StartCoroutine(FadeRoutine(0, 1, _fadeDuration, onObscured));
        }

        private IEnumerator FadeRoutine(float initial, float fadeTo, float duration, System.Action onFinished)
        {
            if (duration == 0) 
            {
                onFinished.Invoke();
                yield break;
            }

            float t = Time.time;
            while (Time.time - t <= duration && _canvasGroup.alpha != fadeTo)
            {
                _canvasGroup.alpha = Mathf.Lerp(initial, fadeTo, (Time.time - t) / duration);
                yield return null;
            }
            
            _canvasGroup.alpha = fadeTo;
            onFinished.Invoke();
        }
    }
}