
 using UnityEngine;

 public class TestSceneBtn : MonoBehaviour
 {
     public ScenesEnums.Scene Scene;

     public void GoToScene()
     {
         SceneLoader.LoadScene(Scene);
     }
 }
