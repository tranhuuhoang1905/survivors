using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    
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
        SceneSignal.OnScoreUpdated += UpdateScoreUI;
        SceneSignal.OnExpUpdated += UpdateExpBarUI;
    }

    void OnDisable()
    {
        SceneSignal.OnScoreUpdated -= UpdateScoreUI;
        SceneSignal.OnExpUpdated -= UpdateExpBarUI;
    }

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
    public void UpdateExpBarUI(int currentValue, int maxValue)
    {
        
                Debug.Log($"check UpdateExpBarUI currentValue exp: {currentValue}");
                Debug.Log($"check UpdateExpBarUI currentValue maxExp: {maxValue}");
        if (maxValue == null || maxValue == 0)
        {
            Debug.Log("check UpdateExpBarUI currentValue exp: true");
            slider.value = 0f;
        }
        else
        {
            
            float fExpPercentage = (float)currentValue / maxValue; // Ép kiểu float
            int iExpPercentage = currentValue / maxValue;
            Debug.Log($"check UpdateExpBarUI currentValue fExpPercentage: false{fExpPercentage}");
            Debug.Log($"check UpdateExpBarUI currentValue iExpPercentage: false{iExpPercentage}");

            slider.value = fExpPercentage;
        }
    }


}
