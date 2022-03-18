using UnityEngine;
using Zenject;

public class LevelsMapUIController : MonoBehaviour
{
    [SerializeField] private UIPacksContainer packsContainer;
    private SceneLoader _sceneLoader;
    private LevelPacksManager _levelPacksManager;
    private PopupsManager _popupsManager;
    private bool isLoading;

    [Inject]
    public void Init(SceneLoader sceneLoader, LevelPacksManager levelPacksManager, PopupsManager popupsManager)
    {
        _sceneLoader = sceneLoader;
        _levelPacksManager = levelPacksManager;
        _popupsManager = popupsManager;
    }

    public void Start()
    {
        _popupsManager.HideAllWithoutAnimation();
        var packInfos = _levelPacksManager.GetPackInfos();
        packsContainer.RefreshContainer(packInfos);
        isLoading = false;
    }

    public void OpenScene(Scene scene)
    {
        _sceneLoader.LoadScene(scene);
    }

    public void OnPackClicked(string packID)
    {
        if (isLoading) return;
        
        _levelPacksManager.SetCurrentPack(packID);
        MessageBus.RaiseEvent<IPackActionHandler>(handler => handler.OnChoosingAnotherPack());
        _sceneLoader.LoadSceneAsync(Scene.GameScene);
        isLoading = true;
    }
}
