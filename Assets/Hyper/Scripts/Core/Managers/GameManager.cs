using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int currentLevelIndex = 0;
    [SerializeField] int score = 0;
    [SerializeField] int exp = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() 
    {
        ScoreSignal.RaiseScoreUpdated(score);
    }

    void OnEnable()
    {
        ScoreSignal.OnAddScore += AddToScore; // Đăng ký sự kiện
    }

    void OnDisable()
    {
        ScoreSignal.OnAddScore -= AddToScore; // Hủy đăng ký sự kiện
    }


    public void SetLevel(int level)
    {
        currentLevelIndex = level;
    }
    public int GetLevel()
    {
        return  currentLevelIndex;
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        ScoreSignal.RaiseScoreUpdated(score);
    }
    
}
