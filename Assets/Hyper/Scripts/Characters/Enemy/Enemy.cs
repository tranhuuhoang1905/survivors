using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    private GameObject player;
    private EnemyMovementBase enemyMovement;
    [SerializeField] GameObject itemBonus;
    [SerializeField] ScoreEntry scoreEntry;

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

    protected override void Die()
    {
        ScoreEvent.RaiseScore(scoreEntry); // 🔥 Gửi sự kiện khi enemy chết
        // DisablePhysics(); // 🔥 Gọi phương thức vô hiệu hóa vật lý
        

        enemyMovement.Die();
        
        if (itemBonus)
        {
            Instantiate(itemBonus, transform.position, transform.rotation);
        }
        StartCoroutine(DestroyAfterDelay(1f));
    }

    // 🕒 Coroutine để delay việc xóa enemy
    private IEnumerator DestroyAfterDelay(float delay)
    {
        Debug.Log("check Delay ------------------------------------");
        yield return new WaitForSeconds(delay); // Chờ 1 giây
        Destroy(gameObject); // Xóa enemy khỏi scene
    }
    
}
