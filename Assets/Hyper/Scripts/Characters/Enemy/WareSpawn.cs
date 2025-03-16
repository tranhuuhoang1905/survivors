using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareSpawn : MonoBehaviour
{
    public Transform enemyPool;
    void Start()
    {
        if (enemyPool == null)
        {
            GameObject poolObject = new GameObject("EnemyPool");
            enemyPool = poolObject.transform;
        }

    }

    protected virtual void SpawnAction(int wareId)
    {
        
    }

}
