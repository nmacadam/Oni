// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine.UI;

namespace Oni.UI
{
    /// <summary>
    /// Catches UI raycasts so events will propagate, but without requiring an image
    /// </summary>
    public class GraphicRaycatcher : Graphic
    {
        public override void SetMaterialDirty() { }
        public override void SetVerticesDirty() { }
        protected override void OnPopulateMesh(VertexHelper vh) { vh.Clear(); }
    }
}