// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 

using UnityEngine;
using Oni.Attributes;

namespace Oni.Demos
{
	public class AttributeExamples : MonoBehaviour
	{
		[Unit("s")] public float SecondsUntil = 5f;
		[LockInPlayMode] public string SomeValue = "testing...";
	}
}