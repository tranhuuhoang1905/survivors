using System.Collections;
using UnityEngine;

public class BossCombatAI : MonoBehaviour, IAttackable
{
    private Animator myAnimator;
    private Transform playerTransform;
    private bool isAttacking = false;

    public bool IsAttacking => isAttacking; // Trả về trạng thái hiện tại

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {
        isAttacking = true;
        StartCoroutine(AttackCoroutine());
    }

    public void StopAttack()
    {
        isAttacking = false;
        StopCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (isAttacking && playerTransform != null)
        {
            myAnimator.SetTrigger("Attack2");
            yield return new WaitForSeconds(1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            Attack();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopAttack();
            playerTransform = null;
        }
    }
}
