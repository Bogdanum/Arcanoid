using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DroppableBonusSettings", menuName = "GameObjectsConfiguration/Bonuses/DroppableBonusesConfig")]
public class DroppableBonusSettings : ScriptableObject
{
    [SerializeField] private float limitPositionY = -10;
    [SerializeField] private BonusParams[] allBonusParams;
    
    private Dictionary<BonusId, Settings> _settingsMap;

    public float LimitPositionY => limitPositionY;
    
    public void Init()
    {
        _settingsMap = new Dictionary<BonusId, Settings>();
        foreach (var bonusParams in allBonusParams)
        {
            _settingsMap.Add(bonusParams.bonusId, bonusParams.settings);
        }
    }

    public Sprite GetSprite(BonusId bonusId) => _settingsMap[bonusId].bonusSprite;

    public Settings GetDroppableBonusSettings(BonusId bonusId) => _settingsMap[bonusId];

    #region Serializable params
    
    [Serializable]
    internal class BonusParams
    {
        public BonusId bonusId;
        public Settings settings;
    }

    [Serializable]
    public class Settings
    {
        public Sprite bonusSprite;
        public Color particlesColor;
        public float gravityScale;
    }
    
    #endregion
}
