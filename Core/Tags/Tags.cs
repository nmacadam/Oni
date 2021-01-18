// MIT License
// 
// Copyright (c) 2020 Vladislav Kinyashov
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

// Modified for ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
	[DisallowMultipleComponent, DefaultExecutionOrder(-9000)]
	public sealed class Tags : MonoBehaviour
	{
		[SerializeField] private Tag[] _tags = default;

		private int _hash = 0;

		private void Awake() 
		{
			_hash = gameObject.GetHashCode();
			AddAll();
		}

		private void OnDestroy()
		{
			RemoveAll();
		}

		private void AddAll()
		{
			for (int i = 0; i < _tags.Length; i++)
			{
				_tags[i].Add(_hash);
			}
		}

		private void RemoveAll()
		{
			for (int i = 0; i < _tags.Length; i++)
			{
				_tags[i].Remove(_hash);
			}
		}
	}
}