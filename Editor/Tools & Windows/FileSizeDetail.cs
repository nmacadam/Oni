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

using UnityEditor;
using UnityEngine;
using System.IO;

namespace Oni.Editor
{
    /// <summary>
    /// Draws the file size of assets into the project window.
    /// </summary>
    public class FileSizeDetail : ProjectWindowDetailBase
	{
		public FileSizeDetail()
		{
			Name = "File Size";
			Alignment = TextAlignment.Right;
			ColumnWidth = 80;
		}

		public override string GetLabel(string guid, string assetPath, Object asset)
		{
			return EditorUtility.FormatBytes(GetFileSize(assetPath));
		}

		private long GetFileSize(string assetPath)
		{

			string fullAssetPath =
				string.Concat(Application.dataPath.Substring(0, Application.dataPath.Length - 7), "/", assetPath);
			long size = new FileInfo(fullAssetPath).Length;
			return size;
		}
	}
}