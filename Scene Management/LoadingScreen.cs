// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System.Collections;
using UnityEngine;

namespace Oni.SceneManagement
{
    /// <summary>
    /// 
    /// </summary>
    public class LoadingScreen : MonoBehaviour, IAsyncOperationMonitor
    {
        public void Monitor(AsyncOperation operation)
        {
            StartCoroutine(UpdateScreen(operation));
        }

        protected virtual IEnumerator UpdateScreen(AsyncOperation operation)
        {
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);

                yield return null;
            }
        }
    }
}