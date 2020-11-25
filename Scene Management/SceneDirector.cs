// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oni.SceneManagement
{
    /// <summary>
    /// Scriptable Object wrapper for the Unity SceneManager class to provide stricter access, more features, and customizability
    /// without MonoBehaviours or Singletons
    /// </summary>
    [CreateAssetMenu(fileName = "Scene Director", menuName = "Oni/Scene Director")]
    public class SceneDirector : ScriptableObject 
    {
        [SerializeField] private SceneReference _loadingScreenScene = default;
        [SerializeField] private LoadingScreen _loadingScreenUI = default;
        [SerializeField] private SceneTransition _defaultTransition = default;

        public SceneReference LoadingScreenScene => _loadingScreenScene;
        public LoadingScreen LoadingScreenUI { get => _loadingScreenUI; set => _loadingScreenUI = value; }
        public SceneTransition DefaultTransition { get => _defaultTransition; set => _defaultTransition = value; }

        private SceneTransition _activeTransition = null;

        private System.Action<Scene, LoadSceneMode> _onBeforeSceneLoad = delegate {};
        private System.Action<Scene, LoadSceneMode> _onSceneLoaded = delegate {};

        public System.Action<Scene, LoadSceneMode> OnBeforeSceneLoad { get => _onBeforeSceneLoad; set => _onBeforeSceneLoad = value; }
        public System.Action<Scene, LoadSceneMode> OnSceneLoaded { get => _onSceneLoaded; set => _onSceneLoaded = value; }


#region Unity Scene Manager Wrappers

        public int SceneCount => SceneManager.sceneCount;
        public int SceneCountInBuildSettings => SceneManager.sceneCountInBuildSettings;

        public Scene CreateScene(string sceneName)
        {
            return SceneManager.CreateScene(sceneName);
        }
        
        public Scene CreateScene(string sceneName, CreateSceneParameters parameters)
        {
            return SceneManager.CreateScene(sceneName, parameters);
        }

        public Scene GetActiveScene()
        {
            return SceneManager.GetActiveScene();
        }

        public Scene GetSceneAt(int index)
        {
            return SceneManager.GetSceneAt(index);
        }

        public Scene GetSceneByBuildIndex(int buildIndex)
        {
            return SceneManager.GetSceneByBuildIndex(buildIndex);
        }

        public Scene GetSceneByName(string name)
        {
            return SceneManager.GetSceneByName(name);
        }

        public Scene GetSceneByPath(string scenePath)
        {
            return SceneManager.GetSceneByPath(scenePath);
        }

#endregion

#region Scene Loading

        // public void TEST_LoadScene(int index)
        // {
        //     if (_activeTransition != null)
        //     {
        //         Debug.Log("Transitioning out");
        //         //_activeTransition.TransitionOut(() => { LoadSceneOperation(index, LoadSceneMode.Single); });
        //         _activeTransition.TransitionOut(() => { LoadSceneOperation(index, LoadSceneMode.Single); });
        //     }
        //     else
        //     {
        //         LoadSceneOperation(index, LoadSceneMode.Single);
        //     }
        // }

        /// <summary>
        /// Load the given scene immediately
        /// </summary>
        public void LoadSceneImmediate(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single, System.Action onTransitioned = null)
        {
            if (_activeTransition != null)
            {
                _activeTransition.TransitionOut(() => 
                { 
                    onTransitioned?.Invoke(); 
                    LoadSceneOperation(scene.BuildIndex, mode); 
                });
            }
            else
            {
                onTransitioned?.Invoke();
                LoadSceneOperation(scene.BuildIndex, mode);
            }
        }

        private void LoadSceneOperation(int buildIndex, LoadSceneMode mode)
        {
            _onBeforeSceneLoad.Invoke(GetSceneByBuildIndex(buildIndex), mode);
            SceneManager.LoadScene(buildIndex);
            _onSceneLoaded.Invoke(GetSceneByBuildIndex(buildIndex), mode);
        }

        /// <summary>
        /// Load the given scene asynchronously
        /// </summary>
        public /* AsyncOperation */ void LoadSceneAsync(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncLoadHandler asyncHandler = new GameObject().AddComponent<AsyncLoadHandler>();
            asyncHandler.StartCoroutine(LoadAsyncRoutine(scene, asyncHandler.gameObject));
            
            // _onBeforeSceneLoad.Invoke(GetSceneByBuildIndex(scene.BuildIndex), mode);
            // var operation = SceneManager.LoadSceneAsync(scene.BuildIndex);
            // operation.completed += (arg) => { _onSceneLoaded.Invoke(GetSceneByBuildIndex(scene.BuildIndex), mode); };

            // return operation;
        }

        /// <summary>
        /// Load the given scene with a loading screen
        /// </summary>
        public void LoadSceneWithLoadingScreen(SceneReference scene)
        {
            Debug.Log("Loading with screen");

            AsyncLoadHandler asyncHandler = new GameObject().AddComponent<AsyncLoadHandler>();
            asyncHandler.StartCoroutine(LoadAsyncWithLoadingScreenRoutine(scene, asyncHandler.gameObject));
        }

        private IEnumerator LoadAsyncRoutine(SceneReference scene, GameObject handler)
        {
            _onBeforeSceneLoad.Invoke(GetSceneByBuildIndex(scene.BuildIndex), LoadSceneMode.Single);
    
            // Transition out of current screen
            var screenOccluded = false;
            if (_activeTransition != null)
            {
                _activeTransition.TransitionOut(() => screenOccluded = true);

                // wait for transition to complete
                yield return new WaitUntil(() => screenOccluded);
            }

            // Without a frame-break the allowSceneActivation flag doesn't work! Not sure why!
            yield return null;

            // Start async scene load to target scene
            var operation = SceneManager.LoadSceneAsync(scene.BuildIndex);
            operation.allowSceneActivation = false;
            operation.completed += (arg) => { _onSceneLoaded.Invoke(GetSceneByBuildIndex(scene.BuildIndex), LoadSceneMode.Single); };

            // Wait until scene is loaded
            yield return new WaitUntil(() => operation.progress >= 0.9f);
            
            // Activate the scene
            operation.allowSceneActivation = true;

            // Destroy the coroutine managing object
            Destroy(handler);
        }

        private IEnumerator LoadAsyncWithLoadingScreenRoutine(SceneReference scene, GameObject handler)
        {
            _onBeforeSceneLoad.Invoke(GetSceneByBuildIndex(scene.BuildIndex), LoadSceneMode.Single);

            // Go to loading screen
            var screenOccluded = false;
            LoadSceneImmediate(_loadingScreenScene, LoadSceneMode.Single, () => screenOccluded = true);

            // Wait for transition to complete
            yield return new WaitUntil(() => screenOccluded);

            // Without a frame-break the allowSceneActivation flag doesn't work! Not sure why!
            yield return null;

            // Instantiate async operation dependent elements
            LoadingScreen ui = null;
            if (_loadingScreenUI != null)
            {
                ui = GameObject.Instantiate(_loadingScreenUI);
            }

            // Start async scene load to target scene
            var operation = SceneManager.LoadSceneAsync(scene.BuildIndex);
            operation.allowSceneActivation = false;
            operation.completed += (arg) => { _onSceneLoaded.Invoke(GetSceneByBuildIndex(scene.BuildIndex), LoadSceneMode.Single); };

            // Monitor async operation
            if (_loadingScreenUI != null && ui != null)
            {
                ui.Monitor(operation);
            }

            // Wait until scene is loaded
            yield return new WaitUntil(() => operation.progress >= 0.9f);

            yield return new WaitForSeconds(1f);

            // Transition out of loading screen
            screenOccluded = false;
            if (_activeTransition != null)
            {
                _activeTransition.TransitionOut(() => screenOccluded = true);

                // wait for transition to complete
                yield return new WaitUntil(() => screenOccluded);
            }
            
            // Activate the scene
            operation.allowSceneActivation = true;

            // Destroy the coroutine managing object
            Destroy(handler);
        }

#endregion

#region Unity Events

        private void OnEnable() 
        {
            SceneManager.sceneLoaded += OnSceneLoadedInvoke;
        }

        private void OnDisable() 
        {
            SceneManager.sceneLoaded -= OnSceneLoadedInvoke;
        }

        private void OnSceneLoadedInvoke(Scene scene, LoadSceneMode mode)
        {
            _onSceneLoaded.Invoke(scene, mode);

            if (_defaultTransition != null)
            {
                _activeTransition = GameObject.Instantiate(_defaultTransition);
                _activeTransition.name = "Transition";
                _activeTransition.TransitionIn(delegate {});
            }
        }
        
#endregion
    }
}