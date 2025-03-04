using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1f;
    protected Animator myAnimator;
    private SpriteRenderer spriteRenderer;
    protected bool isMovement = true;
    protected Transform player;

    protected virtual void Start()
    {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected virtual void Update()
    {
        if (isMovement)
        {
            MoveToPlayer();
        }
    }

    protected virtual void MoveToPlayer()
    {
        Vector2 direction = (player.position - transform.position ).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        FlipEnemyFacing(direction.x);
    }

    protected virtual void FlipEnemyFacing(float directionX)
    {
        if (directionX != 0 )
        {
            spriteRenderer.flipX = directionX < 0; // Flip chỉ sprite
        }
    }

    public virtual void IsMoving(bool action)
    {
        isMovement = action;
    }
}
