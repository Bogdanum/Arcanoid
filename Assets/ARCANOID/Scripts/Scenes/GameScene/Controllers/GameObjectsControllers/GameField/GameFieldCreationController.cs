using UnityEngine;
using Zenject;

public class GameFieldCreationController : MonoBehaviour, ILocalGameStateHandler
{
     [Inject] private LevelPacksManager levelPacksManager;
     [SerializeField] private FieldBorders fieldBorders;
     [SerializeField] private Transform blocksContainer;
     [SerializeField] private FieldSizeController fieldSizeController;
     [SerializeField] private GridOfBlocks gridOfBlocks;
     [SerializeField] private CellsVisualization cellsVisualization;

     private CellsGrid _cellsGrid;

     private void OnEnable() => MessageBus.Subscribe(this);
     private void OnDisable() => MessageBus.Unsubscribe(this);

     private void Awake()
     {
          Init();
     }

     public void Init()
     {
          fieldSizeController.Init();
          _cellsGrid = new CellsGrid(fieldSizeController, cellsVisualization, blocksContainer);
          fieldBorders.Init();
     }

     public void OnPrepare()
     {
          _cellsGrid.Create(levelPacksManager.GetCurrentLevelData());
          gridOfBlocks.Fill(_cellsGrid);
          _cellsGrid.SendCreateBlocksRequest();
     }

     public void OnStartGame() {}

     public void OnContinueGame() {}

     public void OnEndGame() {}
}
