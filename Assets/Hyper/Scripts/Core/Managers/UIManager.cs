using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI levelText;
    
    [SerializeField] private Slider slider;
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
        ScoreEvent.OnScoreUpdated += UpdateScoreUI;
        ScoreEvent.OnTimeUpdated += UpdateTimeUI;
        ScoreEvent.OnExpUpdated += UpdateExpBarUI;
    }

    void OnDisable()
    {
        ScoreEvent.OnScoreUpdated -= UpdateScoreUI;
        ScoreEvent.OnTimeUpdated -= UpdateTimeUI;
        ScoreEvent.OnExpUpdated -= UpdateExpBarUI;
    }

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    private void UpdateTimeUI(int newTime)
    {
        int minutes = Mathf.FloorToInt(newTime / 60);
        int seconds = Mathf.FloorToInt(newTime % 60);
        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void UpdateExpBarUI(int currentValue, int maxValue, int newLevel)
    {
        levelText.text = newLevel.ToString();
        if (maxValue == 0)
        {
            slider.value = 0f;
        }
        else
        {
            float fExpPercentage = (float)currentValue / maxValue; // Ép kiểu float
            int iExpPercentage = currentValue / maxValue;
            slider.value = fExpPercentage;
        }
    }
}
