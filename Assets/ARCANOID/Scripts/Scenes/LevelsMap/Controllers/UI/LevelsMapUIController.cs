using UnityEngine;
using Zenject;

public class LevelsMapUIController : MonoBehaviour
{
    [SerializeField] private UIPacksContainer packsContainer;
    private SceneLoader _sceneLoader;
    private LevelPacksManager _levelPacksManager;
    private PopupsManager _popupsManager;
    private EnergyManager _energyManager;
    private int _energyToStart;
    private bool _isLoading;

    [Inject]
    public void Init(SceneLoader sceneLoader, LevelPacksManager levelPacksManager, PopupsManager popupsManager, EnergyManager energyManager)
    {
        _sceneLoader = sceneLoader;
        _levelPacksManager = levelPacksManager;
        _popupsManager = popupsManager;
        _energyManager = energyManager;
    }

    public void Awake()
    {
        _popupsManager.HideAllWithoutAnimation();
        var packInfos = _levelPacksManager.GetPackInfos();
        packsContainer.RefreshContainer(packInfos);
        SetupLockers();
        _isLoading = false;
    }

    private void OnEnable()
    {
        _energyManager.OnEnergyChanged += UpdateLockers;
        UpdateLockers();
    }

    private void OnDisable() => _energyManager.OnEnergyChanged -= UpdateLockers;

    private void SetupLockers()
    {
        _energyToStart = _energyManager.GetEnergyActionValue(ActionWithEnergy.StartGame);
        packsContainer.SetupLockers(_energyToStart);
    }

    private void UpdateLockers()
    {
        if (_energyManager.IsEnoughEnergy(_energyToStart))
        {
            packsContainer.UnlockButtons();
            return;
        }
        var packInfos = _levelPacksManager.GetPackInfos();
        packsContainer.LockButtons(packInfos);
    }

    public void OpenScene(Scene scene)
    {
        _sceneLoader.LoadScene(scene);
    }

    public void OnPackClicked(string packID)
    {
        if (_isLoading) return;
        
        _levelPacksManager.SetCurrentPack(packID);
        MessageBus.RaiseEvent<IPackActionHandler>(handler => handler.OnChoosingAnotherPack());
        _energyManager.RemoveEnergy(ActionWithEnergy.StartGame);
        _sceneLoader.LoadSceneAsync(Scene.GameScene);
        _isLoading = true;
    }
}
