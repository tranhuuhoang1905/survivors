using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
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
        ScoreSignal.OnScoreUpdated += UpdateScoreUI;
    }

    void OnDisable()
    {
        ScoreSignal.OnScoreUpdated -= UpdateScoreUI;
    }

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = newScore.ToString();
    }


}
