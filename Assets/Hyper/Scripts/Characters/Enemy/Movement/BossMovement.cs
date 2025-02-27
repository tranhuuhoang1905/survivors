using UnityEngine;

public class BossMovement : EnemyMovementBase
{
    private IAttackable attackSystem;

    protected override void Start()
    {
        base.Start();
        attackSystem = GetComponent<IAttackable>(); // Lấy tham chiếu đến hệ thống tấn công
    }

    protected override void Update()
    {
        if (attackSystem == null){
            MoveToPlayer();
            return;
        }
        if (!attackSystem.IsAttacking)
        {
            MoveToPlayer();
            myAnimator.SetInteger("AnimState", 1);
            myAnimator.SetBool("Grounded", true);
        }
        else
        {
            StopMoving();
        }
    }

    protected override void StopMoving()
    {
        // myRigidbody.velocity = Vector2.zero;
        // myAnimator.SetInteger("AnimState", 0);
        // myAnimator.SetBool("Grounded", false);
    }
}
