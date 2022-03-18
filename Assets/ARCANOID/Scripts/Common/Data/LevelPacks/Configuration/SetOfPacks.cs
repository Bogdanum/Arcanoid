using UnityEngine;

[CreateAssetMenu(fileName = "SetOfPacks", menuName = "Data/LevelPacks/New set of packs", order = 2)]
public class SetOfPacks : ScriptableObject
{
    [SerializeField] private LevelPack[] set;

    public LevelPack[] Set => set;
}
