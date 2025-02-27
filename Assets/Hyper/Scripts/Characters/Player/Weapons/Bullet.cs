using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletBase
{
    // [SerializeField] private float burnDamageOverTime = 0.5f;

    protected override void Initialize()
    {
        // Khởi tạo các thuộc tính riêng của FireBullet
        damage = 2; // Ví dụ: Đạn lửa gây sát thương cao hơn
    }

    protected override void Move()
    {
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    protected override void HandleCollision(Collision2D other)
    {
        base.HandleCollision(other);
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // enemy.ApplyBurnEffect(burnDamageOverTime);
            }
        }
    }
}
