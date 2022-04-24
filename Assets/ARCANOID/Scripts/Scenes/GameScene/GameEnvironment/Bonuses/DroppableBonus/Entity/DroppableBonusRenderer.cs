using UnityEngine;

public class DroppableBonusRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bonusSprite;
    [SerializeField] private ParticleSystem particles;

    public void Init(Sprite sprite, Color particlesColor)
    {
        bonusSprite.sprite = sprite;
        var settings = particles.main;
        settings.startColor = particlesColor;
    }
}
