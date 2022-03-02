using UnityEngine;

public class GameFieldController : MonoBehaviour
{
     [SerializeField] private FieldBorders fieldBorders;

     private void Awake()
     {
          Init();
     }

     public void Init()
     {
          fieldBorders.Init();
     }
}
