using UnityEngine.SceneManagement;
using UnityEngine;

public class BattleSelectController : MonoBehaviour
{
    [SerializeField] private AudioClip BattleSelectScenebackgroundMusic; // Nhạc nền mặc định
    public SceneSignal sceneSignal;



    void Start()
    {
        if (BattleSelectScenebackgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(BattleSelectScenebackgroundMusic);
        }
    }
    public void OnBattleSelectButtonPressed(int level)
    {
        sceneSignal.LoadScene("BattleScene");
    }
    
    public void OnExitButtonPressed()
    {
        sceneSignal.LoadScene("StartScene");        // 🔥 Chuyển sang màn hình Loading trước khi vào game
        
    }
}
