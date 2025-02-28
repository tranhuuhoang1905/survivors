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
    private int playerType = 1;
    private int score = 0;
    private int exp = 0;
    private int playerLevel = 1;
    private int[] expToLevelUp = { 0, 10, 20, 35, 50, 70, 95, 120, 150, 185, 225 };


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

    public void AddToScore(ScoreEntry scoreEntry)
    {
        switch (scoreEntry.scoreType)
        {
            case 1:
                score += scoreEntry.value; // Cộng vào điểm số
                
                Debug.Log($"check AddToScore RaiseScoreUpdated score: {score}");
                SceneSignal.RaiseScoreUpdated(score);
                break;
            case 2:
                exp += scoreEntry.value; // Cộng vào kinh nghiệm
                while (playerLevel < expToLevelUp.Length - 1 && exp >= GetMaxExp())
                {
                    exp -= GetMaxExp(); // Trừ đi exp đã dùng
                    playerLevel++; // Tăng cấp

                    Debug.Log($"🔼 Level Up! New Level: {playerLevel}, Remaining Exp: {exp}");
                }
                exp = Mathf.Min(exp,GetMaxExp());
                Debug.Log($"check AddToScore RaiseScoreUpdated exp: {exp}");
                Debug.Log($"check AddToScore RaiseScoreUpdated maxExp: {GetMaxExp()}");
                SceneSignal.RaiseExpUpdated(exp,GetMaxExp(),playerLevel);
                break;
            // case 3:
            //     playerLevel += scoreEntry.value; // Cộng vào cấp độ
            //     break;
            default:
                Debug.LogWarning("Unknown scoreType: " + scoreEntry.scoreType);
                break;
        }
    }
    
    public int GetMaxExp()
    {
        return  expToLevelUp[playerLevel];
    }

    public void RefreshUI()
    {
        
        Debug.Log($"check GameManager RefreshUI: {score}");
        SceneSignal.RaiseScoreUpdated(score);
        SceneSignal.RaiseExpUpdated(exp,GetMaxExp(),playerLevel);
    }
    
}
