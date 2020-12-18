/* // ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using Oni.SceneManagement;
using UnityEngine;

namespace Oni.Demos
{
    /// <summary>
    /// 
    /// </summary>
    public class AsyncLoadTest : MonoBehaviour
    {
        [SerializeField] private SceneDirector _director = default;
        [SerializeField] private SceneReference _nextScene = default;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _director.LoadSceneImmediate(_nextScene);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _director.LoadSceneAsync(_nextScene);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _director.LoadSceneWithLoadingScreen(_nextScene);
            }
        }
    }
}
 */