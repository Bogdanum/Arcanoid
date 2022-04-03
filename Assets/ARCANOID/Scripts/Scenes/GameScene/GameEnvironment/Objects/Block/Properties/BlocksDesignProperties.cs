using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlocksDesignProperties", menuName = "GameObjectsConfiguration/Blocks/BlocksDesignProperties")]
public class BlocksDesignProperties : ScriptableObject
{
     [SerializeField] private BlockHealth blockHealth;
     [SerializeField] private BlockRendererEntity[] blocksRendererSettings;
     
     private Dictionary<BlockRendererParamsID, BlockDesignParams> _blockRendererEntities;
     public BlockHealth BlockHealth => blockHealth;

     public void Init(BlockRendererParamsID paramsID)
     {
          _blockRendererEntities = new Dictionary<BlockRendererParamsID, BlockDesignParams>();
          foreach (var settings in blocksRendererSettings)
          {
               _blockRendererEntities.Add(settings.rendererParamsID, settings.blockDesignParams);
          }
          blockHealth.Init(_blockRendererEntities[paramsID].blockHealth);
     }

     public BlockDesignParams GetBlockRendererParamsByID(BlockRendererParamsID paramsID) => _blockRendererEntities[paramsID];
}

[System.Serializable]
public class BlockRendererEntity
{
     public BlockRendererParamsID rendererParamsID = BlockRendererParamsID.Stone;
     public BlockDesignParams blockDesignParams;
}

[System.Serializable]
public class BlockDesignParams
{
     public int blockHealth;
     public Sprite mainSprite;
     public Color mainColor;
     public Color accentColor;
}