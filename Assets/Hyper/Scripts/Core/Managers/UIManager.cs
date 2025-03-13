using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    // [SerializeField] private TextMeshProUGUI timeFinalText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject mainUI;
    
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
    void Start(){
        HideAllPopup();
    }
    public void HideAllPopup()
    {
        if (winUI) winUI.SetActive(false);
        if (mainUI) mainUI.SetActive(true);

        Time.timeScale = 1f;
    }
    void OnEnable()
    {
        ScoreEvent.OnScoreUpdated += UpdateScoreUI;
        ScoreEvent.OnTimeUpdated += UpdateTimeUI;
        ScoreEvent.OnExpUpdated += UpdateExpBarUI;
        GameEvents.OnGameOver += ShowWinUI;
    }

    void OnDisable()
    {
        ScoreEvent.OnScoreUpdated -= UpdateScoreUI;
        ScoreEvent.OnTimeUpdated -= UpdateTimeUI;
        ScoreEvent.OnExpUpdated -= UpdateExpBarUI;
        GameEvents.OnGameOver -= ShowWinUI;
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
    public void OnExitButtonReStartGame()
    {
        Destroy(GameManager.Instance.gameObject);
        Destroy(AudioManager.Instance.gameObject);
        Destroy(GameController.Instance.gameObject);
        HideAllPopup();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnExitButtonQuitPressed()
    {
        GameController.Instance.Quit();
    }

    public void ShowWinUI()
    {
        if (winUI) 
        {
            winUI.SetActive(true);
            TextMeshProUGUI timeFinalText = winUI.transform.Find("Popup/Time_Panel/Time/TimeValue")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreFinalText = winUI.transform.Find("Popup/ScorePanel/Score/ScoreValue")?.GetComponent<TextMeshProUGUI>();
            if(scoreFinalText) scoreFinalText.text = scoreText.text;
            if(timeFinalText) timeFinalText.text = timeText.text;
            
            
        }
        Time.timeScale = 0f;
    }
}
