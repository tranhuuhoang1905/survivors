using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    private GameObject player;
    private EnemyMovementBase enemyMovement;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovementBase>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            EnemyAttackController.Instance.RegisterEnemy(this);
            enemyMovement.IsMoving(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyAttackController.Instance.UnregisterEnemy(this);
            enemyMovement.IsMoving(true);
        }
    }

    public void Attack()
    {
        enemyMovement.Attack();
    }
    public int GetDamage()
    {
        return damage;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
