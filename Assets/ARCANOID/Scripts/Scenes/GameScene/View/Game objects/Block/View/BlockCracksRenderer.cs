using UnityEngine;

public class BlockCracksRenderer : MonoBehaviour
{ 
    [SerializeField] private SpriteRenderer cracksRenderer;
    private BlockHealth _blockHealth;
    
    public void Init(BlockHealth blockHealth)
    {
        _blockHealth = blockHealth;
    }

    public void Refresh()
    {
        cracksRenderer.enabled = true;
        cracksRenderer.sprite = null;
    }

    public void Disable() => cracksRenderer.enabled = false;

    public void ShowCracksByHealth(int healthPoints)
    {
        Sprite cracksState = _blockHealth.GetCracksByHealth(healthPoints);
        cracksRenderer.sprite = cracksState;
        cracksRenderer.size = Vector2.one;
    }
    
}
