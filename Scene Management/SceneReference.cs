/*
* Copyright (c) 2018 Adam Ramberg
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
* to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
* and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
* WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Oni.SceneManagement
{
    /// <summary>
    /// Struct to hold data about a scene.
    /// </summary>
    [Serializable]
    public struct SceneReference : ISerializationCallbackReceiver, IEquatable<SceneReference>
    {
        /// <summary>
        /// The scene asset.
        /// </summary>
        [SerializeField]
        private Object _sceneAsset;

        /// <summary>
        /// Name of the scene.
        /// </summary>
        [SerializeField]
        private string _sceneName;

        /// <summary>
        /// Path to the scene asset.
        /// </summary>
        [SerializeField]
        private string _scenePath;

        /// <summary>
        /// Build index.
        /// </summary>
        [SerializeField]
        private int _buildIndex;

        /// <summary>
        /// Scene name as a property.
        /// </summary>
        /// <value>Get the scene name.</value>
        public string SceneName { get { return _sceneName; } }
        /// <summary>
        /// Scene path as a property.
        /// </summary>
        /// <value>Get the scene path.</value>
        public string ScenePath { get { return _scenePath; } }
        /// <summary>
        /// Build index as a property.
        /// </summary>
        /// <value>Get the build index.</value>
        public int BuildIndex { get { return _buildIndex; } }

        /// <summary>
        /// Scene asset as a property.
        /// </summary>
        /// <value>Set the scene asset.</value>
        private Object SceneAsset { set { _sceneAsset = value; } } // NOTE: Needed in order to supress warning CS0649


        // makes it work with the existing Unity methods (LoadLevel/LoadScene)
        public static implicit operator string(SceneReference sceneField) { return sceneField.SceneName; }


        public int callbackOrder { get; }

        void Validate()
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlayingOrWillChangePlaymode
            || EditorApplication.isCompiling
            ) return;

            if (_sceneAsset == null)
            {
                _scenePath = "";
                _buildIndex = -1;
                _sceneName = "";
                return;
            }
            _buildIndex = SceneUtility.GetBuildIndexByScenePath(_scenePath);
            if (_sceneAsset != null && _buildIndex == -1)
            {
                /* Sadly its not easy to find which gameobject/component has this SceneField, at least not at this point */
                Debug.LogError($"A scene [{_sceneName}] you used as reference has no valid build Index", _sceneAsset);
                // Exit play mode - might be a bit too harsh?
#if UNITY_2019_1_OR_NEWER
                EditorApplication.ExitPlaymode();
#else
                EditorApplication.isPlaying = false;
#endif
            }
#endif
        }

        public void OnBeforeSerialize() { Validate(); }

        public void OnAfterDeserialize() { }

        /// <summary>
        /// Checks for equality between 2 `SceneField`s.
        /// </summary>
        /// <param name="other">The other `SceneFiled` to compare with.</param>
        /// <returns>`true` if they are equal, otherwise `false`.</returns>
        public bool Equals(SceneReference other)
        {
            return (this == null && other == null) || (this != null && other != null && this._sceneName == other._sceneName);
        }

        /// <summary>
        /// Checks for equality using `object`s.
        /// </summary>
        /// <param name="other">The other scene field as an `object` to compare with.</param>
        /// <returns>`true` if they are equal, otherwise `false`.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null && this != null) return false;

            SceneReference sf = (SceneReference)obj;
            return this != null && sf != null && this.Equals(sf);
        }

        /// <summary>
        /// Get an unique hash code for this `SceneField`.
        /// </summary>
        /// <returns>An unique hash.</returns>
        public override int GetHashCode()
        {
            if (this == null || this._sceneName == null) return 0;
            return this._sceneName.GetHashCode();
        }

        /// <summary>
        /// Equal operator.
        /// </summary>
        /// <param name="sf1">The first `SceneField` to compare.</param>
        /// <param name="sf2">The second `SceneField` to compare.</param>
        /// <returns>`true` if eqaul, otherwise `false`.</returns>
        public static bool operator ==(SceneReference sf1, SceneReference sf2)
        {
            return (sf1 == null && sf2 == null) || (sf1 != null && sf1.Equals(sf2));
        }

        /// <summary>
        /// None equality operator.
        /// </summary>
        /// <param name="sf1">The first `SceneField` to compare.</param>
        /// <param name="sf2">The second `SceneField` to compare.</param>
        /// <returns>`true` if not eqaul, otherwise `false`.</returns>
        public static bool operator !=(SceneReference sf1, SceneReference sf2)
        {
            return (sf1 == null && sf2 != null) || (sf1 != null && !sf1.Equals(sf2));
        }
    }
}
