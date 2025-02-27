using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] int score = 0;
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
    void OnEnable()
    {
        ScoreSignal.OnEnemyKilled += AddToScore; // Đăng ký sự kiện
    }

    void OnDisable()
    {
        ScoreSignal.OnEnemyKilled -= AddToScore; // Hủy đăng ký sự kiện
    }


    void Start() 
    {
        ScoreSignal.RaiseScoreUpdated(score);
    }


    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        ScoreSignal.RaiseScoreUpdated(score);
    }

}
