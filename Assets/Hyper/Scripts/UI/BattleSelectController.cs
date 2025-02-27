using UnityEngine.SceneManagement;
using UnityEngine;

public class BattleSelectController : MonoBehaviour
{
    [SerializeField] private AudioClip BattleSelectScenebackgroundMusic; // Nhạc nền mặc định
    void Start()
    {
        if (BattleSelectScenebackgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(BattleSelectScenebackgroundMusic);
        }
    }
    public void OnBattleSelectButtonPressed(int level)
    {
        GameManager.Instance.SetLevel(level); // nên tạo gameManager hơn, nhưng hiện tại gamemanager chưa chứa bất cứ thứ gì nên tạm vậy
        SceneSignal.LoadScene("BattleScene");
    }
    
    public void OnExitButtonPressed()
    {
        SceneSignal.LoadScene("StartScene");        // 🔥 Chuyển sang màn hình Loading trước khi vào game
    }
}
