using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] float SpantSpeed = 1f;
    [SerializeField] bool IsSpawn = true;
    [SerializeField] GameObject enemy;
    public Transform enemyPool;
    
    bool isAlive = true;

    void Start()
    {
        InvokeRepeating("AutoSpant", 1f, SpantSpeed);
        if (enemyPool == null)
        {
            GameObject poolObject = new GameObject("EnemyPool");
            enemyPool = poolObject.transform;
        }
    }
    void AutoSpant()
    {
        if (!isAlive) { return; }
        if (enemy == null) { return; }
        if (IsSpawn){
            GameObject newEnemy = Instantiate(enemy, transform.position, transform.rotation);
            newEnemy.transform.SetParent(enemyPool);
        }
    }
}
