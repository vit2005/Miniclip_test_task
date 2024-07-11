using UnityEngine;

public enum Layers
{
    Tower = 6,
    Enemy = 7,

}

public class LayersHelper
{
    public static bool IsLayerInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
