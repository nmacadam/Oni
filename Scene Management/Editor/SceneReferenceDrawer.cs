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

using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oni.SceneManagement.Editor
{
    /// <summary>
    /// Customer property drawer for `SceneReference`.
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferenceDrawer : PropertyDrawer
    {
        private bool HasValidBuildIndex(SerializedProperty property)
        {
            var scene = property.FindPropertyRelative("_sceneAsset")?.objectReferenceValue;
            if (scene == null) return true;
            var scenePath = AssetDatabase.GetAssetPath(scene);
            var buildIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);
            return buildIndex != -1;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            OnPropertyGUI(position, property, label);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (HasValidBuildIndex(property)) return base.GetPropertyHeight(property, label);
            return base.GetPropertyHeight(property, label) * 2 + 5;
        }

        private void OnPropertyGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect lower;
            Rect buttonRect = new Rect();
            var scene = property.FindPropertyRelative("_sceneAsset")?.objectReferenceValue;

            if (scene == null)
            {
                // Update values cause the build index could've changed
                property.FindPropertyRelative("_sceneName").stringValue = "";
                property.FindPropertyRelative("_scenePath").stringValue = "";
                property.FindPropertyRelative("_buildIndex").intValue = -1;
            }

            position = IMGUIUtils.SnipRectV(position, EditorGUIUtility.singleLineHeight, out lower, 2f);
            if (HasValidBuildIndex(property))
            {
                if (scene != null)
                {
                    // Update values cause the build index could've changed
                    property.FindPropertyRelative("_sceneName").stringValue = scene.name;
                    property.FindPropertyRelative("_scenePath").stringValue = AssetDatabase.GetAssetPath(scene);
                    property.FindPropertyRelative("_buildIndex").intValue = SceneUtility.GetBuildIndexByScenePath(
                        property.FindPropertyRelative("_scenePath").stringValue
                    );
                }
            }
            else
            {
                position = IMGUIUtils.SnipRectH(position, position.width - 70, out buttonRect, 6f);
                property.FindPropertyRelative("_buildIndex").intValue = -1;
            }

            SceneAsset sceneAsset = scene as SceneAsset;
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            EditorGUI.BeginChangeCheck();
            scene = EditorGUI.ObjectField(position, scene, typeof(SceneAsset), false);
            if (EditorGUI.EndChangeCheck())
            {
                property.FindPropertyRelative("_sceneAsset").objectReferenceValue = scene;
                sceneAsset = scene as SceneAsset;
                if (sceneAsset != null)
                {
                    property.FindPropertyRelative("_sceneName").stringValue = scene.name;
                    property.FindPropertyRelative("_scenePath").stringValue = AssetDatabase.GetAssetPath(scene);
                    property.FindPropertyRelative("_buildIndex").intValue = SceneUtility.GetBuildIndexByScenePath(
                        property.FindPropertyRelative("_scenePath").stringValue
                    );
                }
            }

            if (property.FindPropertyRelative("_buildIndex").intValue != -1) return;

            if (scene != null && scene is SceneAsset)
            {
                EditorGUI.HelpBox(lower, "Scene is not added in the build settings", MessageType.Warning);
                if (GUI.Button(buttonRect, "Fix Now"))
                {
                    AddSceneToBuildSettings(sceneAsset);
                }
            }
        }

        private void AddSceneToBuildSettings(SceneAsset sceneAsset)
        {
            var scenePath = AssetDatabase.GetAssetPath(sceneAsset);

            var scenes = EditorBuildSettings.scenes.ToList();
            scenes.Add(new EditorBuildSettingsScene(scenePath, true));

            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }

    /// <summary>
    /// Utility methods for IMGUI.
    /// </summary>
    public static class IMGUIUtils
    {
        /// <summary>
        /// Snip a `Rect` horizontally.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="range">The range.</param>
        /// <returns>A new `Rect` snipped horizontally.</returns>

        private static Rect SnipRectH(Rect rect, float range)
        {
            if (range == 0) return new Rect(rect);
            if (range > 0)
            {
                return new Rect(rect.x, rect.y, range, rect.height);
            }

            return new Rect(rect.x + rect.width + range, rect.y, -range, rect.height);
        }

        /// <summary>
        /// Snip a `Rect` horizontally.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="range">The range.</param>
        /// <param name="rest">Rest rect.</param>
        /// <param name="gutter">Gutter</param>
        /// <returns>A new `Rect` snipped horizontally.</returns>
        public static Rect SnipRectH(Rect rect, float range, out Rect rest, float gutter = 0f)
        {
            if (range == 0) rest = new Rect();
            if (range > 0)
            {
                rest = new Rect(rect.x + range + gutter, rect.y, rect.width - range - gutter, rect.height);
            }
            else
            {
                rest = new Rect(rect.x, rect.y, rect.width + range + gutter, rect.height);
            }

            return SnipRectH(rect, range);
        }

        /// <summary>
        /// Snip a `Rect` vertically.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="range">The range.</param>
        /// <returns>A new `Rect` snipped vertically.</returns>
        private static Rect SnipRectV(Rect rect, float range)
        {
            if (range == 0) return new Rect(rect);
            if (range > 0)
            {
                return new Rect(rect.x, rect.y, rect.width, range);
            }

            return new Rect(rect.x, rect.y + rect.height + range, rect.width, -range);
        }

        /// <summary>
        /// Snip a `Rect` vertically.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="range">The range.</param>
        /// <param name="rest">Rest rect.</param>
        /// <param name="gutter">Gutter</param>
        /// <returns>A new `Rect` snipped vertically.</returns>
        public static Rect SnipRectV(Rect rect, float range, out Rect rest, float gutter = 0f)
        {
            if (range == 0) rest = new Rect();
            if (range > 0)
            {
                rest = new Rect(rect.x, rect.y + range + gutter, rect.width, rect.height - range - gutter);
            }
            else
            {
                rest = new Rect(rect.x, rect.y, rect.width, rect.height + range + gutter);
            }

            return SnipRectV(rect, range);
        }
    }
}
