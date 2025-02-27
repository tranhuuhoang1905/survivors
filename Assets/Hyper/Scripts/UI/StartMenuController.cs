using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor; // Thêm thư viện UnityEditor để dùng ExitPlaymode()
#endif

public class StartMenuManager : MonoBehaviour
{
    
    public void OnStartButtonPressed()
    {
        SceneSignal.LoadScene("BattleSelectScene");
    }

    public void OnExitButtonPressed()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode(); // Thoát hẳn Unity Editor khi nhấn Exit
        #else
            Application.Quit(); // Thoát game khi đã build
        #endif
    }
}