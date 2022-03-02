using UnityEngine;

[CreateAssetMenu(fileName = "FieldSettings", menuName = "GameObjectsConfiguration/FieldSettings")]
public class FieldSettings : ScriptableObject
{
    [SerializeField] private float headerOffset = 0.2f;
    [SerializeField] private float sideOffset;

    public float HeaderOffset => headerOffset;
    public float SideOffset => sideOffset;
}
