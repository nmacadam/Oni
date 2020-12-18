// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)
// Inspired by https://github.com/innogames/ProjectWindowDetails

/*
* MIT License
* Copyright (c) 2019 InnoGames GmbH
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

using UnityEditor;
using UnityEngine;

namespace Oni.Editor
{
    public abstract class ProjectWindowDetailBase
	{
		private const string ShowPrefsKey = "ProjectWindowDetails.Show.";
		public int ColumnWidth = 100;
		public string Name = "Base";
		public TextAlignment Alignment = TextAlignment.Left;

		public bool Visible
		{
			get
			{
				return EditorPrefs.GetBool(string.Concat(ShowPrefsKey, Name));
			}

			set
			{
				EditorPrefs.SetBool(string.Concat(ShowPrefsKey, Name), value);
			}
		}

		public abstract string GetLabel(string guid, string assetPath, Object asset);
	}
}