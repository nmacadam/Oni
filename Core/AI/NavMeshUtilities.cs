// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEngine.AI;

namespace Oni.AI
{
	public static class NavMeshUtilities
	{
		/// <summary>
		/// Trys to locate a random point on a NavMesh within a spherical space
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="distance">The radius of the sphere</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="areaMask">A mask that specifies the NavMesh areas allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointInSphere(Vector3 origin, float distance, out Vector3 randomPosition, int areaMask = NavMesh.AllAreas) 
		{
			randomPosition = Vector3.zero;

            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
            randomDirection += origin;
           
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, distance, areaMask))
			{
				randomPosition = hit.position;
				return true;
			}
           
			return false;
        }

		/// <summary>
		/// Trys to locate a random point on a NavMesh within a spherical space
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="distance">The radius of the sphere</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="filter">A filter specifying which NavMesh areas are allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointInSphere(Vector3 origin, float distance, out Vector3 randomPosition, NavMeshQueryFilter filter) 
		{
			randomPosition = Vector3.zero;

            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
            randomDirection += origin;
           
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, distance, filter))
			{
				randomPosition = hit.position;
				return true;
			}
           
			return false;
        }

		/// <summary>
		/// Trys to locate a random point on a NavMesh within a spherical space over multiple iterations
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="distance">The radius of the sphere</param>
		/// <param name="maxIterations">Max number of times to check for a position</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="areaMask">A mask that specifies the NavMesh areas allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointInSphere(Vector3 origin, float distance, int maxIterations, out Vector3 randomPosition, int areaMask = NavMesh.AllAreas) 
		{
			randomPosition = Vector3.zero;

			for (int i = 0; i < maxIterations; i++)
			{
				if (TryRandomPointInSphere(origin, distance, out Vector3 hit, areaMask))
				{
					randomPosition = hit;
					return true;
				}
			}
            
			return false;
        }

		/// <summary>
		/// Trys to locate a random point on a NavMesh within a spherical space over multiple iterations
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="distance">The radius of the sphere</param>
		/// <param name="maxIterations">Max number of times to check for a position</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="filter">A filter specifying which NavMesh areas are allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointInSphere(Vector3 origin, float distance, int maxIterations, out Vector3 randomPosition, NavMeshQueryFilter filter) 
		{
			randomPosition = Vector3.zero;

			for (int i = 0; i < maxIterations; i++)
			{
				if (TryRandomPointInSphere(origin, distance, out Vector3 hit, filter))
				{
					randomPosition = hit;
					return true;
				}
			}
            
			return false;
        }



		/// <summary>
		/// Trys to locate a random point on a NavMesh on the shell of a spherical space
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="radius">The radius of the sphere</param>
		/// <param name="maxSampleDistance">The max distance to sample the NavMesh with from the edge of the spherical shell</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="areaMask">A mask that specifies the NavMesh areas allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointOnSphereShell(Vector3 origin, float radius, float maxSampleDistance, out Vector3 randomPosition, int areaMask = NavMesh.AllAreas) 
		{
			randomPosition = Vector3.zero;

            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere.normalized * radius;
            randomDirection += origin;
           
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, maxSampleDistance, areaMask))
			{
				randomPosition = hit.position;
				return true;
			}
           
			return false;
        }

		/// <summary>
		/// Trys to locate a random point on a NavMesh within a spherical space
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="radius">The radius of the sphere</param>
		/// <param name="maxSampleDistance">The max distance to sample the NavMesh with from the edge of the spherical shell</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="filter">A filter specifying which NavMesh areas are allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointOnSphereShell(Vector3 origin, float radius, float maxSampleDistance, out Vector3 randomPosition, NavMeshQueryFilter filter) 
		{
			randomPosition = Vector3.zero;

            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere.normalized * radius;
            randomDirection += origin;
           
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, maxSampleDistance, filter))
			{
				randomPosition = hit.position;
				return true;
			}
           
			return false;
        }

		/// <summary>
		/// Trys to locate a random point on a NavMesh within a spherical space over multiple iterations
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="radius">The radius of the sphere</param>
		/// <param name="maxSampleDistance">The max distance to sample the NavMesh with from the edge of the spherical shell</param>
		/// <param name="maxIterations">Max number of times to check for a position</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="areaMask">A mask that specifies the NavMesh areas allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointOnSphereShell(Vector3 origin, float distance, float maxSampleDistance, int maxIterations, out Vector3 randomPosition, int areaMask = NavMesh.AllAreas) 
		{
			randomPosition = Vector3.zero;

			for (int i = 0; i < maxIterations; i++)
			{
				if (TryRandomPointOnSphereShell(origin, distance, maxSampleDistance, out Vector3 hit, areaMask))
				{
					randomPosition = hit;
					return true;
				}
			}
            
			return false;
        }

		/// <summary>
		/// Trys to locate a random point on a NavMesh within a spherical space over multiple iterations
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="radius">The radius of the sphere</param>
		/// <param name="maxSampleDistance">The max distance to sample the NavMesh with from the edge of the spherical shell</param>
		/// <param name="maxIterations">Max number of times to check for a position</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="filter">A filter specifying which NavMesh areas are allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointOnSphereShell(Vector3 origin, float distance, float maxSampleDistance, int maxIterations, out Vector3 randomPosition, NavMeshQueryFilter filter) 
		{
			randomPosition = Vector3.zero;

			for (int i = 0; i < maxIterations; i++)
			{
				if (TryRandomPointOnSphereShell(origin, distance, maxSampleDistance, out Vector3 hit, filter))
				{
					randomPosition = hit;
					return true;
				}
			}
            
			return false;
        }


		/// <summary>
		/// Trys to locate a random point on a NavMesh on the shell of a spherical space
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="radius">The radius of the sphere</param>
		/// <param name="maxSampleDistance">The max distance to sample the NavMesh with from the edge of the spherical shell</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="areaMask">A mask that specifies the NavMesh areas allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointOnCircleShell(Vector3 origin, float radius, float maxSampleDistance, out Vector3 randomPosition, int areaMask = NavMesh.AllAreas) 
		{
			randomPosition = Vector3.zero;

			Vector2 random2DDirection = UnityEngine.Random.insideUnitCircle.normalized * radius;
            Vector3 randomDirection = new Vector3(random2DDirection.x, 0, random2DDirection.y);
            randomDirection += origin;
           
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, maxSampleDistance, areaMask))
			{
				randomPosition = hit.position;
				return true;
			}
           
			return false;
        }

		/// <summary>
		/// Trys to locate a random point on a NavMesh within a spherical space
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="radius">The radius of the sphere</param>
		/// <param name="maxSampleDistance">The max distance to sample the NavMesh with from the edge of the spherical shell</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="filter">A filter specifying which NavMesh areas are allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointOnCircleShell(Vector3 origin, float radius, float maxSampleDistance, out Vector3 randomPosition, NavMeshQueryFilter filter) 
		{
			randomPosition = Vector3.zero;

			Vector2 random2DDirection = UnityEngine.Random.insideUnitCircle.normalized * radius;
            Vector3 randomDirection = new Vector3(random2DDirection.x, 0, random2DDirection.y);
            randomDirection += origin;
           
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, maxSampleDistance, filter))
			{
				randomPosition = hit.position;
				return true;
			}
           
			return false;
        }

		/// <summary>
		/// Trys to locate a random point on a NavMesh within a spherical space over multiple iterations
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="radius">The radius of the sphere</param>
		/// <param name="maxSampleDistance">The max distance to sample the NavMesh with from the edge of the spherical shell</param>
		/// <param name="maxIterations">Max number of times to check for a position</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="areaMask">A mask that specifies the NavMesh areas allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointOnCircleShell(Vector3 origin, float distance, float maxSampleDistance, int maxIterations, out Vector3 randomPosition, int areaMask = NavMesh.AllAreas) 
		{
			randomPosition = Vector3.zero;

			for (int i = 0; i < maxIterations; i++)
			{
				if (TryRandomPointOnCircleShell(origin, distance, maxSampleDistance, out Vector3 hit, areaMask))
				{
					randomPosition = hit;
					return true;
				}
			}
            
			return false;
        }

		/// <summary>
		/// Trys to locate a random point on a NavMesh within a spherical space over multiple iterations
		/// </summary>
		/// <param name="origin">The center position of the sphere</param>
		/// <param name="radius">The radius of the sphere</param>
		/// <param name="maxSampleDistance">The max distance to sample the NavMesh with from the edge of the spherical shell</param>
		/// <param name="maxIterations">Max number of times to check for a position</param>
		/// <param name="randomPosition">The found random position</param>
		/// <param name="filter">A filter specifying which NavMesh areas are allowed when finding the nearest point.</param>
		/// <returns>Whether a position was found</returns>
		public static bool TryRandomPointOnCircleShell(Vector3 origin, float distance, float maxSampleDistance, int maxIterations, out Vector3 randomPosition, NavMeshQueryFilter filter) 
		{
			randomPosition = Vector3.zero;

			for (int i = 0; i < maxIterations; i++)
			{
				if (TryRandomPointOnCircleShell(origin, distance, maxSampleDistance, out Vector3 hit, filter))
				{
					randomPosition = hit;
					return true;
				}
			}
            
			return false;
        }
	}
}