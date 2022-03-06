using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlocksDesignProperties", menuName = "GameObjectsConfiguration/Blocks/BlocksDesignProperties")]
public class BlocksDesignProperties : ScriptableObject
{
     [SerializeField] private BlockHealth blockHealth;
     [SerializeField] private BlockRendererEntity[] blocksRendererSettings;
     
     private Dictionary<BlockRendererParamsID, BlockRendererParams> _blockRendererEntities;
     public BlockHealth BlockHealth => blockHealth;

     public void Init()
     {
          blockHealth.Init();

          _blockRendererEntities = new Dictionary<BlockRendererParamsID, BlockRendererParams>();
          foreach (var settings in blocksRendererSettings)
          {
               _blockRendererEntities.Add(settings.rendererParamsID, settings.blockRendererParams);
          }
     }

     public BlockRendererParams GetBlockRendererParamsByID(BlockRendererParamsID paramsID) => _blockRendererEntities[paramsID];
}

[System.Serializable]
public class BlockRendererEntity
{
     public BlockRendererParamsID rendererParamsID = BlockRendererParamsID.Stone;
     public BlockRendererParams blockRendererParams;
}

[System.Serializable]
public class BlockRendererParams
{
     public Sprite mainSprite;
     public Color particlesColor;
}