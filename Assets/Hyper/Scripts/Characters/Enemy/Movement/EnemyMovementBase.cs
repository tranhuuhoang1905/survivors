using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.FantasyMonsters.Common.Scripts;

public class EnemyMovementBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1f;
    protected Animator myAnimator;
    private SpriteRenderer spriteRenderer;
    protected bool isMovement = true;
    protected Transform player;
    protected Monster monster;
    [SerializeField] protected GameObject bodyMonster;

    protected virtual void Start()
    {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        monster = GetComponent<Monster>();

        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        bodyMonster = transform.Find("Body")?.gameObject;

        
    }

    protected virtual void Update()
    {
        if (isMovement)
        {
            Move();
        }
    }

    protected virtual void Move()
    {
        // monster.
        if ( monster){
        }
        Vector2 direction = (player.position - transform.position ).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        FlipEnemyFacing(direction.x);
    }

    protected virtual void FlipEnemyFacing(float directionX)
    {
        
        if (directionX != 0 )
        {
            if (bodyMonster != null)
            {
                if (directionX < 0)
                {
                    bodyMonster.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    bodyMonster.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                
                spriteRenderer.flipX = directionX < 0; // Flip chỉ sprite
            }
        }
    }

    public virtual void IsMoving(bool action)
    {
        isMovement = action;
    }
    public virtual void Attack()
    {
        monster.Attack();
    }
}
