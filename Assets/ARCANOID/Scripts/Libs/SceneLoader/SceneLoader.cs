using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadScene(ScenesEnums.Scene scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public static void LoadSceneAsync(ScenesEnums.Scene scene)
    {
        SceneManager.LoadSceneAsync((int) scene);
    }
}