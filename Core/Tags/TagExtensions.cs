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
	public static class TagExtensions
	{
		public static void AddTag(this GameObject entity, Tag tag)
		{
			tag.Add(entity.GetHashCode());
		}

		public static void AddTags(this GameObject entity, Tag[] tags)
		{
			int hash = entity.GetHashCode();

			for (int i = 0; i < tags.Length; i++)
			{
				var tag = tags[i];
				tag.Add(hash);
			}
		}

		public static void RemoveTag(this GameObject entity, Tag tag)
		{
			tag.Remove(entity.GetHashCode());
		}

		public static void RemoveTags(this GameObject entity, Tag[] tags)
		{
			int hash = entity.GetHashCode();

			for (int i = 0; i < tags.Length; i++)
			{
				var tag = tags[i];
				tag.Remove(hash);
			}
		}

		public static bool HasTag(this GameObject entity, Tag tag)
		{
			return tag.HasEntity(entity.GetHashCode());
		}

		public static bool HasTags(this GameObject entity, Tag[] tags, bool allRequired)
		{
			int hash = entity.GetHashCode();

			if (allRequired)
			{
				for (int i = 0; i < tags.Length; i++)
				{
					if (!tags[i].HasEntity(hash))
						return false;
				}

				return true;
			}
			else
			{
				for (int i = 0; i < tags.Length; i++)
				{
					if (tags[i].HasEntity(hash))
						return true;
				}

				return false;
			}
		}
	}
}