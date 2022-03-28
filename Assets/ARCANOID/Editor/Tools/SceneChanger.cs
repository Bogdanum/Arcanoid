using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class SceneChanger : Editor
{
    [MenuItem("Game/OpenScene/HomeScene  sc -> 3"), Shortcut("Game/HomeScene", KeyCode.Alpha3)]
    private static void HomeScene()
    {
        EditorSceneManager.OpenScene("Assets/ARCANOID/Scenes/MenuScene.unity");
    }
    
    [MenuItem("Game/OpenScene/LevelSelection  sc -> 4"), Shortcut("Game/LevelSelectionScene", KeyCode.Alpha4)]
    private static void LevelSelectionScene()
    {
        EditorSceneManager.OpenScene("Assets/ARCANOID/Scenes/LevelSelection.unity");
    }
    
    [MenuItem("Game/OpenScene/GameScene  sc -> 5"), Shortcut("Game/GameScene", KeyCode.Alpha5)]
    private static void GameScene()
    {
        EditorSceneManager.OpenScene("Assets/ARCANOID/Scenes/GameScene.unity");
    }
}
