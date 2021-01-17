// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.Tools
{
	public class Note : MonoBehaviour
	{
#if UNITY_EDITOR
		public enum NoteType
		{
			None,
			Info,
			Warning,
			Error
		}

#pragma warning disable 414
		[SerializeField] private string _message = default;
		[SerializeField] private NoteType _type = default;
#pragma warning restore 414
#endif
    }
}