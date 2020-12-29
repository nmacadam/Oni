// MIT License
// Original work Copyright (c) 2019 InnoGames GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES 
// OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
// https://github.com/innogames/ProjectWindowDetails

// Modified for ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Oni.Editor
{
    // Do not run this on the server because it could slow down tests and builds:
	#if !CONTINUOUS_INTEGRATION


	/// <summary>
	/// This class draws additional columns into the project window.
	/// </summary>
	[InitializeOnLoad]
	public static class ProjectWindowDetails
	{
		private static readonly List<ProjectWindowDetailBase> _details = new List<ProjectWindowDetailBase>();
		private static GUIStyle _rightAlignedStyle;

		private const int SpaceBetweenColumns = 10;
		private const int MenuIconWidth = 20;

		static ProjectWindowDetails()
		{
			EditorApplication.projectWindowItemOnGUI += DrawAssetDetails;

			foreach (var type in GetAllDetailTypes())
			{
				_details.Add((ProjectWindowDetailBase)Activator.CreateInstance(type));
			}
		}

		public static IEnumerable<Type> GetAllDetailTypes()
		{
			// Get all classes that inherit from ProjectViewDetailBase:
			var types = Assembly.GetExecutingAssembly().GetTypes();
			foreach (var type in types)
			{
				if (type.BaseType == typeof(ProjectWindowDetailBase))
				{
					yield return type;
				}

			}
		}

		// [MenuItem("Assets/Details...")]
		// public static void Menu()
		// {
		// 	//Event.current.Use();
		// 	ShowContextMenu();
		// }

		private static void DrawAssetDetails(string guid, Rect rect)
		{
			if (Application.isPlaying)
			{
				return;
			}

			if (!IsMainListAsset(rect))
			{
				return;
			}

			var isSelected = Array.IndexOf(Selection.assetGUIDs, guid) >= 0;

			if (isSelected && 
				Event.current.type == EventType.MouseDown &&
				Event.current.button == 0 &&
				Event.current.mousePosition.x > rect.xMax - MenuIconWidth &&
				Event.current.mousePosition.y > rect.yMax - MenuIconWidth &&
				Event.current.mousePosition.y < rect.yMin + MenuIconWidth)
			{
				Event.current.Use();

				var contextRect = new Rect(rect);
				contextRect.x += rect.width;
				contextRect.x -= MenuIconWidth;
				ShowContextMenu(contextRect.position);
			}

			if (Event.current.type != EventType.Repaint)
			{
				return;
			}

			// Right align label and leave some space for the menu icon:
			rect.x += rect.width;
			rect.x -= MenuIconWidth;
			rect.width = MenuIconWidth;

			if (isSelected)
			{
				DrawMenuIcon(rect);
			}

			var assetPath = AssetDatabase.GUIDToAssetPath(guid);
			if (AssetDatabase.IsValidFolder(assetPath))
			{
				return;
			}

			var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
			if (asset == null)
			{
				// this entry could be Favourites or Packages. Ignore it.
				return;
			}

			for (var i = _details.Count - 1; i >= 0; i--)
			{
				var detail = _details[i];
				if (!detail.Visible)
				{
					continue;
				}

				rect.width = detail.ColumnWidth;
				rect.x -= detail.ColumnWidth + SpaceBetweenColumns;
				GUI.Label(rect, new GUIContent(detail.GetLabel(guid, assetPath, asset), detail.Name),
					GetStyle(detail.Alignment));
			}
		}

		private static void DrawMenuIcon(Rect rect)
		{
			//rect.y += 4;
			var icon = EditorGUIUtility.IconContent("d_align_vertically_center");
			EditorGUI.LabelField(rect, icon);
		}

		private static GUIStyle GetStyle(TextAlignment alignment)
		{
			return alignment == TextAlignment.Left ? EditorStyles.label : RightAlignedStyle;
		}

		private static GUIStyle RightAlignedStyle
		{
			get
			{
				if (_rightAlignedStyle == null)
				{
					_rightAlignedStyle = new GUIStyle(EditorStyles.label);
					_rightAlignedStyle.alignment = TextAnchor.MiddleRight;
				}

				return _rightAlignedStyle;
			}
		}

		private static void ShowContextMenu(Vector2 position)
		{
			var menu = new GenericMenu();
			foreach (var detail in _details)
			{
				menu.AddItem(new GUIContent(detail.Name), detail.Visible, ToggleMenu, detail);
			}
			menu.AddSeparator("");
			menu.AddItem(new GUIContent("None"), false, HideAllDetails);
			menu.DropDown(new Rect(position, Vector2.zero));
		}

		private static void HideAllDetails()
		{
			foreach (var detail in _details)
			{
				detail.Visible = false;
			}
		}

		public static void ToggleMenu(object data)
		{
			var detail = (ProjectWindowDetailBase) data;
			detail.Visible = !detail.Visible;
		}

		private static bool IsMainListAsset(Rect rect)
		{
			// Don't draw details if project view shows large preview icons:
			if (rect.height > 20)
			{
				return false;
			}

			// Don't draw details if this asset is a sub asset:
			if (rect.x > 16)
			{
				return false;
			}

			return true;
		}
	}

	#endif
}