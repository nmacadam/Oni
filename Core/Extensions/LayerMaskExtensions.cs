// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
    public static class LayerMaskExtensions
    {
        /// <summary>
        /// Returns whether the layer is contained in the layer mask
        /// </summary>
        /// <param name="layerMask">This layer mask</param>
        /// <param name="layer">The layer to check for</param>
        /// <returns>Whether the layer is contained in the layer mask</returns>
        public static bool Contains(this LayerMask layerMask, int layer)
        {
            int mask = 1 << layer;
            int tmp = layerMask & mask;
            return tmp != 0;
        }
    }
}
