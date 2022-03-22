using UnityEngine;

[CreateAssetMenu(fileName = "BonusName", menuName = "GameObjectsConfiguration/Bonuses/Effects/Create New SimpleEffect config")]
public class SimpleTemporaryBonusConfig : ScriptableObject
{
    [SerializeField, Min(0.1f)] private float effectTime = 5;

    public float EffectTime => effectTime;
}
