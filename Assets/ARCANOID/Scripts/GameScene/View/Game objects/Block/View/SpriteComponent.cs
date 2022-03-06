using UnityEngine;

public class SpriteComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private BonusRendererParams bonusRendererParams;

    public void RefreshScale()
    {
        if (bonusRendererParams.isBonusRenderer)
        {
            renderer.size = bonusRendererParams.bonusSpriteScale;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        renderer.enabled = true;
        renderer.sprite = sprite;
        RefreshScale();
    }

    public void Disable()
    {
        renderer.enabled = false;
    }
}

[System.Serializable]
public struct BonusRendererParams
{
    public bool isBonusRenderer;
    public Vector2 bonusSpriteScale;
}
