using UnityEngine;
using Zenject;

public class MenuUiMediator : MonoBehaviour
{
    [Inject] private LocalizationManager _localizationManager;
    [Inject] private SceneLoader _sceneLoader;
    [SerializeField] private GameTitleLoopAnimation gameTitleAnim;

    private void Awake()
    {
        gameTitleAnim.Play();
    }

    public void GoToScene(Scene scene)
    {
        gameTitleAnim.Stop();
        _sceneLoader.LoadScene(scene);
    }

    public void SetUiLanguage(LanguagesEnums.Language language)
    {
        _localizationManager.SetCurrentLanguage(language);
    }
}
