using UnityEngine;

[CreateAssetMenu(fileName = "FieldSettings", menuName = "GameObjectsConfiguration/FieldSettings")]
public class FieldSettings : ScriptableObject
{
    [SerializeField] private float headerOffset = 0.2f;
    [SerializeField] private float sideOffset = 0.1f;
    [SerializeField] private float cellsMargin = 0.1f;
    [SerializeField] private float rectBlockAspect = 2f;

    public float HeaderOffset => headerOffset;
    public float SideOffset => sideOffset;
    public float CellsMargin => cellsMargin;
    public float RectBlockAspect => rectBlockAspect;
}
