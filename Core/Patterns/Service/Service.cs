// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Oni.Patterns
{
	/// <summary>
	/// Toolbox pattern-like access for singleton objects
	/// </summary>
	/// <typeparam name="TService">The type of the singleton object to create</typeparam>
	public abstract class Service<TService> : MonoBehaviour
		where TService : Service<TService>
	{
		private static Dictionary<Type, TService> _services = new Dictionary<Type, TService>();
		private static bool _quitting;
		private static readonly object _lock = new object();
		
		public static TService Get()
		{
			if (_quitting)
			{
				Debug.LogError($"Service objects cannot be retrieved while the application is quitting.");
				return null;
			}
			
			lock (_lock)
			{
				var type = typeof(TService);
				if (_services.ContainsKey(type))
				{
					return _services[type];
				}
			
				Debug.LogError($"Service of type {type} is not available.");
				return null;
			}
		}

		protected virtual bool DontDestroyOnLoad => false;

		protected void Awake()
		{
			var type = typeof(TService);
			if (_services.ContainsKey(type))
			{
				// Only allow one instance of each service
				Destroy(gameObject);
				return;
			}
			
			_services.Add(type, (TService) this);

			if (DontDestroyOnLoad)
			{
				DontDestroyOnLoad(gameObject);
			}
		}

		private void OnApplicationQuit()
		{
			_quitting = true;
		}
	}
}