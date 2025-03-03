using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Signals/SceneSignal")]
public class SceneSignal : ScriptableObject
{
    public UnityEvent<string> OnLoadScene;
    public UnityEvent OnSceneLoaded;

    public void LoadScene(string sceneName)
    {
        OnLoadScene?.Invoke(sceneName);
    }

    public void SceneLoaded()
    {
        Debug.Log("SceneSignalSO: Scene đã tải xong");
        OnSceneLoaded?.Invoke();
    }
}