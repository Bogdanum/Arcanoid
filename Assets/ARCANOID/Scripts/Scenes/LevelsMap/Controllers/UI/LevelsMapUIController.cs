using UnityEngine;
using Zenject;

public class LevelsMapUIController : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    [Inject]
    public void Init(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    
    public void OpenScene(Scene scene)
    {
        _sceneLoader.LoadScene(scene);
    }
}
