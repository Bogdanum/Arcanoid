using UnityEngine;
using Zenject;

public class GameFieldCreationController : MonoBehaviour, ILocalGameStateHandler
{
     private LevelPacksManager _levelPacksManager;
     [SerializeField] private FieldBorders fieldBorders;
     [SerializeField] private Transform blocksContainer;
     [SerializeField] private FieldSizeController fieldSizeController;
     [SerializeField] private GridOfBlocks gridOfBlocks;
     [SerializeField] private CellsVisualization cellsVisualization;

     private CellsGrid _cellsGrid;

     private void OnEnable() => MessageBus.Subscribe(this);
     private void OnDisable() => MessageBus.Unsubscribe(this);
     
     [Inject]
     public void Init(LevelPacksManager levelPacksManager)
     {
          _levelPacksManager = levelPacksManager;
          fieldSizeController.Init();
          _cellsGrid = new CellsGrid(fieldSizeController, cellsVisualization, blocksContainer);
          fieldBorders.Init();
     }

     public void OnPrepare()
     {
          _cellsGrid.Create(_levelPacksManager.GetCurrentLevelData());
          gridOfBlocks.Fill(_cellsGrid);
          var blocksCreatorProvider = new BlocksCreatorProvider(_cellsGrid);
          blocksCreatorProvider.SendCreateBlocksRequest();
     }

     public void OnStartGame() {}

     public void OnContinueGame() {}

     public void OnEndGame() {}
}
