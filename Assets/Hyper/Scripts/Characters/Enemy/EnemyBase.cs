using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float health, maxHealth = 3f;
    [SerializeField] protected int damage = 10;
    protected SliderBar healthBar;

    protected virtual void Awake()
    {
        healthBar = GetComponentInChildren<SliderBar>();
    }
    protected virtual void Initialize()
    {
        // Phương thức này có thể được ghi đè bởi các lớp con để khởi tạo các thuộc tính riêng
    }

    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateSliderBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }
    public virtual void ApplyBurnEffect(float burnDamageOverTime)
    {
        // Xử lý hiệu ứng cháy
    }

    public virtual void ApplySlowEffect(float slowEffectDuration)
    {
        // Xử lý hiệu ứng làm chậm
    }
    protected abstract void Die();
}
