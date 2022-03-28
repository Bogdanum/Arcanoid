using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BombName", menuName = "GameObjectsConfiguration/Bonuses/Effects/Create new bomb config")]
public class BombBonusConfig : ScriptableObject
{
    [SerializeField] private float delay;
    [SerializeField] private int damage;
    [SerializeField] private BlockType[] destroyableBlocks;
    private HashSet<BlockType> _blocksSet;

    public float Delay => delay;
    public int Damage => damage;

    public void Init()
    {
        _blocksSet = new HashSet<BlockType>(destroyableBlocks);
    }

    public bool CanBeDestroyed(BlockType block) => _blocksSet.Contains(block);
}
