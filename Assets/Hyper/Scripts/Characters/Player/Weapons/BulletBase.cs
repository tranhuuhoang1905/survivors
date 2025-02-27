using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed = 10f;
    protected Rigidbody2D myRigidbody;
    protected PlayerMovement player;
    protected float xSpeed;
    protected int damage = 1;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
        transform.localScale = new Vector2((Mathf.Sign(xSpeed)), 1f);
        Initialize();
    }

    protected virtual void Initialize()
    {
        // Phương thức này có thể được ghi đè bởi các lớp con để khởi tạo các thuộc tính riêng
    }

    void Update()
    {
        Move();
    }

    protected abstract void Move();

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            HandleCollision(other);
            Destroy(gameObject);
        }
    }

    protected virtual void HandleCollision(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>(); 
            if (enemy != null)
            {
                enemy.TakeDamage(damage); 
            }
        }
    }
}