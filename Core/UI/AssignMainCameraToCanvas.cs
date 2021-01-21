// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.UI
{
	[RequireComponent(typeof(Canvas))]
	public class AssignMainCameraToCanvas : MonoBehaviour
	{
		private void Awake() 
		{
			Canvas canvas = GetComponent<Canvas>();
			canvas.worldCamera = Camera.main;
		}
	}
}