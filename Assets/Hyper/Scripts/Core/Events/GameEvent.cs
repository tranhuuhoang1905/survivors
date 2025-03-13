using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static event Action OnGameOver;

    public static void GameOver()
    {
        OnGameOver?.Invoke();
    }
}
