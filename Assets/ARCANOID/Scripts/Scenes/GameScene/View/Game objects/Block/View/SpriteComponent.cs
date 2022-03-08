using UnityEngine;

public class SpriteComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mainSpriteRenderer;
    [SerializeField] private BonusRendererParams bonusRendererParams;

    public void RefreshScale()
    {
        if (bonusRendererParams.isBonusRenderer)
        {
            mainSpriteRenderer.size = bonusRendererParams.bonusSpriteScale;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        mainSpriteRenderer.enabled = true;
        mainSpriteRenderer.sprite = sprite;
        RefreshScale();
    }

    public void Disable()
    {
        mainSpriteRenderer.enabled = false;
    }
}

[System.Serializable]
public struct BonusRendererParams
{
    public bool isBonusRenderer;
    public Vector2 bonusSpriteScale;
}
