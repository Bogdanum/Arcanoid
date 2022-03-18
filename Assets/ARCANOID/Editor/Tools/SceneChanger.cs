using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class SceneChanger : Editor
{
    [MenuItem("Tools/OpenScene/HomeScene  sc -> 3"), Shortcut("Tools/HomeScene", KeyCode.Alpha3)]
    private static void HomeScene()
    {
        EditorSceneManager.OpenScene("Assets/ARCANOID/Scenes/MenuScene.unity");
    }
    
    [MenuItem("Tools/OpenScene/LevelSelection  sc -> 4"), Shortcut("Tools/LevelSelectionScene", KeyCode.Alpha4)]
    private static void LevelSelectionScene()
    {
        EditorSceneManager.OpenScene("Assets/ARCANOID/Scenes/LevelSelection.unity");
    }
    
    [MenuItem("Tools/OpenScene/GameScene  sc -> 5"), Shortcut("Tools/GameScene", KeyCode.Alpha5)]
    private static void GameScene()
    {
        EditorSceneManager.OpenScene("Assets/ARCANOID/Scenes/GameScene.unity");
    }
}
