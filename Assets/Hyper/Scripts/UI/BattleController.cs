using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] private AudioClip BattleScenebackgroundMusic; // Nhạc nền mặc định
    void Start()
    {

        if (BattleScenebackgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(BattleScenebackgroundMusic);
        }
        SceneSignal.SceneLoaded();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
