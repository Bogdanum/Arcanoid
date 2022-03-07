using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private FadingPanel fadingPanel;
    [SerializeField] private LoadingScreenConfig config;

    public async void LoadScene(Scene sceneName)
    {
        fadingPanel.FadeIn(config.FadeinTime, config.FadingEase, config.FadeinDelay);
        loadingScreen.ResetValues();
        
        var scene = SceneManager.LoadSceneAsync((int)sceneName);
        scene.allowSceneActivation = false;
        do 
        {
            await Task.Delay(config.DelayToUpdateProgressbarMS);
            loadingScreen.UpdateTargetProgress(scene.progress);
        } 
        while (scene.progress < 0.9f);
        scene.allowSceneActivation = true;
        fadingPanel.FadeOut(config.FadeOutTime, config.FadingEase, config.FadeOutDelay);
    }
}