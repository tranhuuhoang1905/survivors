using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ScoreEvent ScoreEvent;
    private int playerType = 1;
    private int score = 0;
    
    protected SliderBar healthBar;
    public SceneSignal sceneSignal;


    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        ScoreEvent.OnAddScore += AddToScore; // Đăng ký sự kiện
        
        sceneSignal.OnSceneLoaded.AddListener(RefreshUI);
        
    }

    void Start() 
    {
        RefreshUI();
    }
    void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        ScoreEvent.OnAddScore -= AddToScore;
        sceneSignal.OnSceneLoaded.RemoveListener(RefreshUI);
    }


    public void AddToScore(ScoreEntry scoreEntry)
    {
        switch (scoreEntry.scoreType)
        {
            case 1:
                score += scoreEntry.value; // Cộng vào điểm số
                ScoreEvent.RaiseScoreUpdated(score);
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
        ScoreEvent.RaiseScoreUpdated(score);
        if (Character.Instance != null)
        {
            int exp = Character.Instance.exp;
            int level = Character.Instance.level;
            int maxExp = Character.Instance.GetMaxExp();
            ScoreEvent.RaiseExpUpdated(exp, maxExp, level);
        }
    }
    
}
