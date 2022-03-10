using UnityEngine;

public class GameFieldCreationController : MonoBehaviour, ILocalGameStateHandler
{
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
          _cellsGrid.Create(5, 8);
          gridOfBlocks.Fill(_cellsGrid);
          _cellsGrid.SendCreateBlocksRequest();
     }

     public void OnStartGame() {}

     public void OnContinueGame() {}

     public void OnEndGame() {}
}
