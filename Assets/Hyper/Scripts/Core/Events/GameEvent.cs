using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static event Action OnGameOver;
    public static event Action<Vector3, int> OnShowFloatingText;

    public static void GameOver()
    {
        OnGameOver?.Invoke();
    }
    
    public static void ShowFloatingText(Vector3 position,int value)
    {
        OnShowFloatingText?.Invoke(position,value);
    }
}
