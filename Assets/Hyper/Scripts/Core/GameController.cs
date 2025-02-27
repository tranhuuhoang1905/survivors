using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private string sceneToLoad; // Biến lưu tên scene cần load
    private int currentLevelIndex = 0;
    
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
        SceneSignal.OnLoadScene += LoadScene;
    }

    void OnDisable()
    {
        SceneSignal.OnLoadScene -= LoadScene;
    }

    void Start() 
    {
    }


    public void LoadScene(string sceneName)
    {
        if (sceneName == "BattleScene")
        {
            sceneToLoad = sceneName; // Lưu scene cần load
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public IEnumerator LoadTargetScene()
    {
        yield return new WaitForSeconds(0.5f); // Giả lập thời gian load UI

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false; // Ngăn scene mới tự động kích hoạt

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingController.Instance.UpdateProgress(progress); // 🔥 Cập nhật progress bar

            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f); // Giả lập thời gian chờ
                operation.allowSceneActivation = true; // 🔥 Kích hoạt scene mới
            }

            yield return null;
        }
    }
    public void SetLevel(int level)
    {
        currentLevelIndex = level;
    }
    public int GetLevel()
    {
        return  currentLevelIndex;
    }
    
}
