using UnityEngine;

public static class RectTransformExtensions
{
    public static void RefreshScaleAndPosition(this RectTransform rectTransform)
    {
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.sizeDelta = Vector2.zero;
    }
}
