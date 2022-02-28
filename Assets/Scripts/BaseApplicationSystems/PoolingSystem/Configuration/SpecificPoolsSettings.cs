using UnityEngine;

[CreateAssetMenu(fileName = "PoolsSettings", menuName = "Pooling System/PoolsSettings")]
public class SpecificPoolsSettings : ScriptableObject
{
    [SerializeField] private SpecificPoolSettings[] allPoolsSettings;

    public SpecificPoolSettings[] AllPoolsSettings => allPoolsSettings;
}

[System.Serializable]
public class SpecificPoolSettings
{
    public AbstractPoolItemFactory factory;
    public PoolItem itemPrefab;
    public ScriptableObject itemSettings;
    [Range(1, 1000)]
    public int initialCount = 5;
}