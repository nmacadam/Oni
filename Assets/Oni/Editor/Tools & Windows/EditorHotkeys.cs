// Modified shortcuts from https://github.com/nowsprinting/blender-like-sceneview-hotkeys

/*
* MIT License
* Copyright (c) 2020 Koji Hasegawa
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
* to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
* and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES 
* OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using UnityEngine;
using UnityEditor;

namespace Oni.Editor
{
	public class EditorHotkeys
	{
		[InitializeOnLoadMethod]
        private static void InitializeSceneViewOnGuiDelegate()
        {
#if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui += OnGUI;
#else
            SceneView.onSceneGUIDelegate += OnGUI;
#endif
        }

		private static void OnGUI(SceneView sceneView)
        {
            var e = Event.current;
            if (e.type != EventType.KeyDown)
            {
                return;
            }

            var key = e.keyCode;

			switch (key)
			{
				case KeyCode.Keypad1:
                    FrontView(sceneView);
                    break;
				case KeyCode.Keypad3:
					LeftView(sceneView);
                    break;
				case KeyCode.Keypad7:
					TopView(sceneView);
                    break;

				case KeyCode.Keypad5:
					ToggleOrthographic(sceneView);
                    break;
			}

            e.Use();
        }

		[MenuItem("Tools/Oni/View/Toggle Orthographic")]
		private static void ToggleOrthographic()
		{
			ToggleOrthographic(SceneView.lastActiveSceneView);
		}

		private static void ToggleOrthographic(SceneView view)
		{
			view.orthographic = !view.orthographic;
		}

		[MenuItem("Tools/Oni/View/Front View")]
		private static void FrontView()
		{
			FrontView(SceneView.lastActiveSceneView);
		}

		private static void FrontView(SceneView view)
		{
			view.LookAt(
				view.pivot, 
				Quaternion.LookRotation(Vector3.forward, Vector3.up), 
				view.size, 
				true
			);
		}

		[MenuItem("Tools/Oni/View/Left View")]
		private static void LeftView()
		{
			LeftView(SceneView.lastActiveSceneView);
		}

		private static void LeftView(SceneView view)
		{
			view.LookAt(
				view.pivot, 
				Quaternion.LookRotation(Vector3.left, Vector3.up), 
				view.size, 
				true
			);
		}

		[MenuItem("Tools/Oni/View/Top View")]
		private static void TopView()
		{
			TopView(SceneView.lastActiveSceneView);
		}

		private static void TopView(SceneView view)
		{
			view.LookAt(
				view.pivot, 
				Quaternion.LookRotation(Vector3.down, Vector3.forward), 
				view.size, 
				true
			);
		}
	}
}