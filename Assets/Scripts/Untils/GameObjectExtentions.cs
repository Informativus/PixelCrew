using UnityEngine;

namespace PixelCrew.Untils
{
    public static class GameObjectExtentions
    {
        public static bool IsInLayer(this GameObject go, LayerMask layer)
        {
            return layer == (layer | 1 << go.layer);
        }
    }
}
