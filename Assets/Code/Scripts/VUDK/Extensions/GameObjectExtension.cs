namespace VUDK.Extensions
{
    using UnityEngine;

    public static class GameObjectExtension
    {
        /// <summary>
        /// Check if game object is in layer mask.
        /// </summary>
        /// <param name="gameObject">GameObject to check.</param>
        /// <param name="layerMask">Layer mask.</param>
        /// <returns>True if it is in layer mask, False if not.</returns>
        public static bool IsInLayerMask(this GameObject gameObject, LayerMask layerMask)
        {
            if(gameObject == null)
                return false;

            return layerMask == (layerMask | (1 << gameObject.layer));
        }
    }
}