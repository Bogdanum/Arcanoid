using UnityEngine;

public class GameFieldCreationController : MonoBehaviour
{
     [SerializeField] private FieldBorders fieldBorders;
     [SerializeField] private Transform blocksContainer;
     [SerializeField] private FieldSizeController fieldSizeController;
     [SerializeField] private GridOfBlocks gridOfBlocks;
     [SerializeField] private CellsVisualization cellsVisualization;

     private CellsGrid _cellsGrid;

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

     private void Start()
     {
          OnGamePreparation();
     }

     private void OnGamePreparation()
     {
          _cellsGrid.Create(5, 8);
          gridOfBlocks.Fill(_cellsGrid);
          _cellsGrid.SendCreateBlocksRequest();
     }
     
}
