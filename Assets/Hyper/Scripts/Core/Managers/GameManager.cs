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
    
    protected SliderBar healthBar;


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
        RefreshUI();
    }
    void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        ScoreSignal.OnAddScore -= AddToScore;
        SceneSignal.OnSceneLoaded -= RefreshUI;
    }


    public void AddToScore(ScoreEntry scoreEntry)
    {
        switch (scoreEntry.scoreType)
        {
            case 1:
                score += scoreEntry.value; // Cộng vào điểm số
                SceneSignal.RaiseScoreUpdated(score);
                break;
            case 2:
                if (Character.Instance != null)
                {
                    Character.Instance.AddExp(scoreEntry.value);
                }
                break;
            case 3:
                if (Character.Instance != null)
                {
                    Character.Instance.AddHealth(scoreEntry.value);
                }
                break;
            default:
                break;
        }
    }

    public void RefreshUI()
    {
        SceneSignal.RaiseScoreUpdated(score);
        int exp = Character.Instance.exp;
        int level = Character.Instance.level;
        int maxExp = Character.Instance.GetMaxExp();
        SceneSignal.RaiseExpUpdated(exp, maxExp, level);
    }
    
}
