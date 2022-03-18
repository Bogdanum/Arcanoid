using UnityEngine;
using Zenject;

public class MenuUiMediator : MonoBehaviour
{
    [SerializeField] private GameTitleLoopAnimation gameTitleAnim;
    private LocalizationManager _localizationManager;
    private SceneLoader _sceneLoader;
    private LevelPacksManager _levelPacksManager;
    private bool isLoading;

    [Inject]
    private void Init(LocalizationManager localizationManager, SceneLoader sceneLoader, LevelPacksManager levelPacksManager)
    {
        _localizationManager = localizationManager;
        _sceneLoader = sceneLoader;
        _levelPacksManager = levelPacksManager;
        gameTitleAnim.Play();
        isLoading = false;
    }

    public void GoToScene(Scene scene)
    {
        if (!isLoading)
        {
            isLoading = true;
            bool firstTimePlay = _levelPacksManager.IsFirsTimePlay();
            gameTitleAnim.Stop();

            if (firstTimePlay)
            {
                _levelPacksManager.SetupFirstPack();
                MessageBus.RaiseEvent<IPackActionHandler>(handler => handler.OnChoosingAnotherPack());
                _sceneLoader.LoadSceneAsync(Scene.GameScene);
            }
            else
            {
                _sceneLoader.LoadScene(scene);
            }
        }
    }
    
    public void SetUiLanguage(LanguagesEnums.Language language)
    {
        _localizationManager.SetCurrentLanguage(language);
    }
}
