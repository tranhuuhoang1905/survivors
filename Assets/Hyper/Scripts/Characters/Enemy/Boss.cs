using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{

    [SerializeField] private int armor = 1;
    
    protected override void Initialize()
    {
        // Khởi tạo các thuộc tính riêng của FireBullet
        score = 2; // Ví dụ: Đạn lửa gây sát thương cao hơn
    }

    public override void TakeDamage (int damageAmount)
    {
        int actualDamage =Mathf.Max(0, damageAmount - armor); // Apply armor
        base.TakeDamage(actualDamage);
    }
   

    
}
