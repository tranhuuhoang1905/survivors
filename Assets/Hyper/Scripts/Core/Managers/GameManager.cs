using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ScoreSignal scoreSignal;
    private int currentLevelIndex = 0;
    private int playerType = 1;
    private int score = 0;
    private int exp = 0;
    private int maxExp = 10;
    private int playerLevel = 1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        ScoreSignal.OnAddScore += AddToScore; // Đăng ký sự kiện
        SceneSignal.OnSceneLoaded += RefreshUI;
    }

    void Start() 
    {
        Debug.Log($"check Start RaiseScoreUpdated score: {score}");
        SceneSignal.RaiseScoreUpdated(score);
    }
    void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        ScoreSignal.OnAddScore -= AddToScore;
        SceneSignal.OnSceneLoaded -= RefreshUI;
    }

    public void SetLevel(int newLevel)
    {
        playerLevel = newLevel;
    }

    public int GetLevel()
    {
        return  playerLevel;
    }

    public void AddToScore(ScoreData scoreData)
    {
        switch (scoreData.scoreType)
        {
            case 1:
                score += scoreData.value; // Cộng vào điểm số
                
                Debug.Log($"check AddToScore RaiseScoreUpdated score: {score}");
                SceneSignal.RaiseScoreUpdated(score);
                break;
            case 2:
                exp += scoreData.value; // Cộng vào kinh nghiệm
                
                Debug.Log($"check AddToScore RaiseScoreUpdated exp: {exp}");
                Debug.Log($"check AddToScore RaiseScoreUpdated maxExp: {maxExp}");
                SceneSignal.RaiseExpUpdated(exp,maxExp);
                break;
            // case 3:
            //     playerLevel += scoreData.value; // Cộng vào cấp độ
            //     break;
            default:
                Debug.LogWarning("Unknown scoreType: " + scoreData.scoreType);
                break;
        }
    }
    public void RefreshUI()
    {
        
        Debug.Log($"check GameManager RefreshUI: {score}");
        SceneSignal.RaiseScoreUpdated(score);
        SceneSignal.RaiseExpUpdated(exp,maxExp);
    }
    
}
