using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] float SpantSpeed = 1f;
    [SerializeField] bool IsSpawn = true;
    [SerializeField] GameObject bullet;
    
    bool isAlive = true;

    void Start()
    {
        InvokeRepeating("AutoSpant", 1f, SpantSpeed);
    }
    void AutoSpant()
    {
        if (!isAlive) { return; }
        if (IsSpawn){
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
