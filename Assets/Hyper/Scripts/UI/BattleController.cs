using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] private AudioClip BattleScenebackgroundMusic; // Nhạc nền mặc định
    public SceneSignal sceneSignal;
    void Start()
    {

        if (BattleScenebackgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(BattleScenebackgroundMusic);
        }
        sceneSignal.SceneLoaded();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
