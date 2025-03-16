using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    
    [System.Serializable]
    public class Wave
    {
        public float time;  // Thời gian xuất hiện wave
    }

    public List<Wave> listWaves;  // Danh sách các wave
    // public WaveSpawner waveSpawner;  // Tham chiếu đến WaveSpawner
    [SerializeField] private float normalSpawnInterval = 5f;
    private int wareId = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        // StartCoroutine(SpawnWaves());
        StartCoroutine(NormalWaveSpawn());
    }

    // private IEnumerator SpawnWaves()
    // {
    //     foreach (var wave in listWaves)
    //     {
    //         yield return new WaitForSeconds(wave.time);  // Chờ đến thời gian của wave
    //     }
    // }
    private IEnumerator NormalWaveSpawn()
    {
        while (true) // 🔄 Chạy vô hạn, spawn normal wave mỗi 10 giây
        {
            yield return new WaitForSeconds(normalSpawnInterval); // ⏳ Chờ 10 giây
            GameEvents.NomalWareSpawn(wareId);
            wareId ++;
        }
    }
}