using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = -1f;
    protected Rigidbody2D myRigidbody;
    protected Animator myAnimator;
    protected bool isAttacking = false;

    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
    }

    protected virtual void Update()
    {
        if (!isAttacking)
        {
            Move();
        }
    }

    protected virtual void Move()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    protected virtual void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), 1f);
    }

    protected virtual void StopMoving()
    {
        myRigidbody.velocity = Vector2.zero;
    }
}
