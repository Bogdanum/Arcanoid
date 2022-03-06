using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class SceneChanger : Editor
{
    [MenuItem("Tools/HomeScene   3"), Shortcut("Tools/HomeScene", KeyCode.Alpha3)]
    private static void HomeScene()
    {
        EditorSceneManager.OpenScene("Assets/ARCANOID/Scenes/MenuScene.unity");
    }
    
    [MenuItem("Tools/GameScene   4"), Shortcut("Tools/GameScene", KeyCode.Alpha4)]
    private static void GameScene()
    {
        EditorSceneManager.OpenScene("Assets/ARCANOID/Scenes/GameScene.unity");
    }
}
