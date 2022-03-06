using UnityEngine;

public class BlockCracksRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    private BlockHealth _blockHealth;
    
    public void Init(BlockHealth blockHealth)
    {
        _blockHealth = blockHealth;
    }

    public void Refresh()
    {
        renderer.enabled = true;
        renderer.sprite = null;
    }

    public void Disable() => renderer.enabled = false;

    public void ShowCracksByHealth(int healthPoints)
    {
        Sprite cracksState = _blockHealth.GetCracksByHealth(healthPoints);
        renderer.sprite = cracksState;
        renderer.size = Vector2.one;
    }
    
}
