// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.SceneManagement
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsyncOperationMonitor
    {
        void Monitor(AsyncOperation operation);
    }
}
