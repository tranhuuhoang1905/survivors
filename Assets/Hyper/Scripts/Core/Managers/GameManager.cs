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
    private int timeSeconds = 0;
    
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
        StartCoroutine(startTime());
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
            case ScoreType.Score:
                score += scoreEntry.value; // Cộng vào điểm số
                ScoreEvent.RaiseScoreUpdated(score);
                break;
            case ScoreType.Experience:
                if (Character.Instance != null)
                {
                    Character.Instance.AddExp(scoreEntry.value);
                }
                break;
            case ScoreType.Health:
                if (Character.Instance != null)
                {
                    Character.Instance.AddHealth(scoreEntry.value);
                }
                break;
            case ScoreType.SwordUpgrade:
                if (Character.Instance != null)
                {
                    Character.Instance.AddSwordLevel(scoreEntry.value);
                }
                break;
            default:
                break;
        }
    }

    private IEnumerator startTime()
    {
        while (true)
        {
            timeSeconds +=1;
            UpdateTimerUI();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateTimerUI()
    {

        ScoreEvent.RaiseTimeUpdated(timeSeconds);
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
    public void SetPlayerType(int type)
    {
        playerType = type;
    }

    public int GetPlayerType()
    {
        return playerType;
    }
    public int GetScore()
    {
        return score;
    }
}
