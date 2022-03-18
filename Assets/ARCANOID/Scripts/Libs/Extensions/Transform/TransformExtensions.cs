using UnityEngine;

public static class TransformExtensions
{
    public static void SetDefaultParams(this Transform transform, Transform parent = null)
    {
        transform.SetParent(parent);
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}
