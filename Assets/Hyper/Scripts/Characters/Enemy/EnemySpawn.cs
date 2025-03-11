using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] float SpantSpeed = 10f;
    [SerializeField] float spawnSpeedIncrease = 0.5f;
    [SerializeField] private float spawnRadius = 100f; // 🔥 Bán kính spawn ngẫu nhiên
     
    [SerializeField] bool IsSpawn = true;
    [SerializeField] GameObject enemy;
    public Transform enemyPool;
    
    bool isAlive = true;

    void Start()
    {
        if (enemyPool == null)
        {
            GameObject poolObject = new GameObject("EnemyPool");
            enemyPool = poolObject.transform;
        }

        StartCoroutine(SpawnEnemyCoroutine()); // 🔥 Bắt đầu spawn quái
        StartCoroutine(IncreaseSpawnSpeedCoroutine()); // 🔥 Tăng tốc độ spawn mỗi 10s
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        while (isAlive)
        {
            if (IsSpawn && enemy != null)
            {
                float randomSpawnTime = Random.Range(SpantSpeed*0.5f, SpantSpeed* 1.5f); // 🔥 Random thời gian spawn
                yield return new WaitForSeconds(randomSpawnTime);
                
                GameObject newEnemy = Instantiate(enemy, transform.position, transform.rotation);
                newEnemy.transform.SetParent(enemyPool,true);
                
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator IncreaseSpawnSpeedCoroutine()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(10f); // 🔥 Cứ mỗi 10s
            SpantSpeed = Mathf.Max(0.2f,SpantSpeed - spawnSpeedIncrease); // 🔥 Tăng tốc độ spawn (Giảm SpantSpeed)
            Debug.Log($"🔥 SpantSpeed mới: {SpantSpeed}");
        }
    }

}
