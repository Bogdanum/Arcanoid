using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private FadingPanel loadingScreenPanel;
    [SerializeField] private FadingPanel blackScreenPanel;
    [SerializeField] private LoadingScreenConfig config;

    public async void LoadSceneAsync(Scene sceneName)
    {
        FadeIn(loadingScreenPanel);
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
        FadeOut(loadingScreenPanel);
    }

    public void LoadScene(Scene scene)
    {
        FadeIn(blackScreenPanel, () =>
        {
            SceneManager.LoadScene((int)scene);
            FadeOut(blackScreenPanel);
        });
    }

    private void FadeIn(FadingPanel panel, Action onComplete = null)
    {
        panel.FadeIn(config.FadeinTime, config.FadingEase, config.FadeinDelay, onComplete);
    }

    private void FadeOut(FadingPanel panel, Action onComplete = null)
    {
        panel.FadeOut(config.FadeOutTime, config.FadingEase, config.FadeOutDelay, onComplete);
    }
}