using UnityEngine;
public class SceneSignal
{
    
    public static event System.Action<string> OnLoadScene;
    public static void LoadScene(string sceneName)
    {
        OnLoadScene?.Invoke(sceneName);
    }
}