using UnityEngine;
public class SceneSignal
{
    
    public static event System.Action<string> OnLoadScene;
    public static event System.Action OnSceneLoaded;
    public static event System.Action<int, int> OnExpUpdated;
    public static event System.Action<int> OnScoreUpdated;

    public static void LoadScene(string sceneName)
    {
        OnLoadScene?.Invoke(sceneName);
    }

    public static void SceneLoaded()
    {
        Debug.Log("check SceneSignal SceneLoaded");
        OnSceneLoaded?.Invoke();
    }
    
    public static void RaiseScoreUpdated(int newScore)
    {
        OnScoreUpdated?.Invoke(newScore);
    }

    public static void RaiseExpUpdated(int newScore,int maxScore)
    {
        OnExpUpdated?.Invoke(newScore,maxScore);
    }
}