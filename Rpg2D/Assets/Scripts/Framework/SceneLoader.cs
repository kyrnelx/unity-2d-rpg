using System;
using System.Collections;
using UnityEngine.Events;

public class SceneLoader
{
    private static SceneLoader m_instance;
    public static SceneLoader Instance => m_instance ?? (m_instance = new SceneLoader());

    public void LoadSceneAsync(string sceneName, UnityAction callback = null, float delayCallback = 0f)
    {
        CoroutineManager.Instance.StartCoroutine(IELoadSceneAsync(sceneName, callback, delayCallback));
    }

    public void LoadSceneAsync(SceneID sceneID, UnityAction callback = null, float delayCallback = 0f)
    {
        string sceneName = string.Empty;
        switch (sceneID)
        {
            case SceneID.MainMenuScene:
                sceneName = "MainMenuScene";
                break;
            case SceneID.MainGameScene:
                sceneName = "MainGameScene";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sceneID), sceneID, null);
        }

        CoroutineManager.Instance.StartCoroutine(IELoadSceneAsync(sceneName, callback, delayCallback));
    }

    private IEnumerator IELoadSceneAsync(string sceneName, UnityAction callback = null, float delayCallback = 0f)
    {
        var ao = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        yield return ao;
        yield return delayCallback;
        callback?.Invoke();
    }
}

public enum SceneID
{
    MainMenuScene,
    MainGameScene
}