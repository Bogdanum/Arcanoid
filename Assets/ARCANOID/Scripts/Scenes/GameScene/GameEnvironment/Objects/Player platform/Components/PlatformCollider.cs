using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    private Transform _transform;

    public void Init()
    {
        _transform = transform;
    }
    
    public void SetNewSize(float size)
    {
        _transform.localScale = new Vector3(size, _transform.localScale.y, 1);
    }
}
