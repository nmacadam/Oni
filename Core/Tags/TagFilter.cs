// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System.Collections.Generic;
using UnityEngine;

namespace Oni
{
    [System.Serializable]
    public class TagFilter
	{
		private enum CheckType
		{
			All,
			Any
		}

		[SerializeField] private List<Tag> _checkForTags = new List<Tag>();
		[SerializeField] private CheckType _require = default;

		public bool Check(GameObject gameObject)
		{
			if (_checkForTags.Count == 0)
			{
				return true;
			}

            return gameObject.HasTags(_checkForTags.ToArray(), _require == CheckType.All);
		}
	}
}