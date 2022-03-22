using UnityEngine;

[CreateAssetMenu(fileName = "HealthViewGridConfig", menuName = "UI/Header/HealthViewGridConfig")]
public class HealthViewGridConfig : ScriptableObject
{
    [SerializeField, Min(1)] private int initHealthCount;
    [SerializeField, Min(1)] private int maxHealthCount;
    [SerializeField] private float durationOfAppearance;

    public int InitHealthCount => initHealthCount;
    public int MaxHealthCount => maxHealthCount;
    public float DurationOfAppearance => durationOfAppearance;
}

