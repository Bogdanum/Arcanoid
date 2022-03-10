using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthViewGridConfig", menuName = "UI/Header/HealthViewGridConfig")]
public class HealthViewGridConfig : ScriptableObject
{
    [SerializeField, Min(1)] private int initHealthCount;
    [SerializeField, Min(1)] private int maxHealthCount;
    [SerializeField] private float durationOfAppearance;
    [Space(20)]
    [SerializeField] private HealthGridSizes[] healthGridSizesMap;

    public int InitHealthCount => initHealthCount;
    public int MaxHealthCount => maxHealthCount;
    public float DurationOfAppearance => durationOfAppearance;
    
    public Dictionary<int, float> GetGridSizes()
    {
        var gridSizes = new Dictionary<int, float>();
        foreach (var stage in healthGridSizesMap)
        {
            gridSizes.Add(stage.Count, stage.Size);
        }
        return gridSizes;
    }
}

[System.Serializable]
public struct HealthGridSizes
{
    public int Count;
    public float Size;
}

