using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : MonoBehaviour
{
    [SerializeField] private GameObject prefabEffect; // Hiệu ứng skill
    [SerializeField] private GameObject prefabHitEffect; // Hiệu ứng skill
    [SerializeField] private float skillRadius = 5f;  // Phạm vi ảnh hưởng
    [SerializeField] private int damagePerSecond = 50;
    
    [SerializeField] private float skillDuration = 3.1f;
    

    public void ActionSkill_1()
    {
        // Tạo hiệu ứng skill tại vị trí nhân vật
        GameObject effectInstance = Instantiate(prefabEffect, transform.position, Quaternion.identity);
        effectInstance.transform.SetParent(transform,true);
        Destroy(effectInstance, skillDuration); // Hủy hiệu ứng sau thời gian tồn tại

        // Bắt đầu Coroutine gây sát thương
        StartCoroutine(ApplyDamageOverTime());
    }

    private IEnumerator ApplyDamageOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < skillDuration)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, skillRadius);

            foreach (Collider2D hit in hitEnemies)
            {
                if (hit.CompareTag("Enemy") ) // Chỉ gây sát thương cho Enemy
                {
                    EnemyBase enemy = hit.GetComponent<EnemyBase>();
                    if (enemy != null)
                    {
                        if (prefabHitEffect!= null)
                        {
                            GameObject effectInstance = Instantiate(prefabHitEffect, enemy.transform.position, Quaternion.identity);
                        }
                        enemy.TakeDamage(damagePerSecond);
                    }
                }
            }

            // Đợi 1 giây rồi tiếp tục gây sát thương
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
    }

    // Debug phạm vi skill trong Scene
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, skillRadius);
    }
}