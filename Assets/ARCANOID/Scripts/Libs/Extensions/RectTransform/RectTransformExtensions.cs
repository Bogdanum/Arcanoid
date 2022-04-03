using UnityEngine;

public static class RectTransformExtensions
{
    public static void RefreshScaleAndPosition(this RectTransform rectTransform)
    {
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.sizeDelta = Vector2.zero;
    }

    public static void SetLocalPositionAndRotation(this RectTransform rectTransform, Vector3 localPosition, Quaternion rotation)
    {
        rectTransform.localPosition = localPosition;
        rectTransform.rotation = rotation;
    }
}
