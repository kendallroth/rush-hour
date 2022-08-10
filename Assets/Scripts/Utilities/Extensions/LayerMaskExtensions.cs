using UnityEngine;

public static class LayerMaskExtensions
{
    #region Custom Methods
    /// <summary>
    /// Invert a layer mask (for ignoring layers)
    /// </summary>
    /// <param name="layerMask">Source layer mask</param>
    /// <returns>Inverted layer mask</returns>
    public static LayerMask Invert(this LayerMask layerMask)
    {
        return ~layerMask;
    }

    /// <summary>
    /// Indicate whether a layermask contains a layer
    /// </summary>
    /// <param name="mask">Layermask</param>
    /// <param name="layer">Layer being checked</param>
    /// <returns>Whether layermask contains layer</returns>
    public static bool ContainsLayer(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
    #endregion
}

