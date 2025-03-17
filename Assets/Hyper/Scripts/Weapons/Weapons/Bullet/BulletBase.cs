using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed = 10f;
    [SerializeField] protected int damage = 1;

    private BulletSoundManager soundManager;
    private BulletMovement movement;

    public void Initialize(BulletSoundManager soundManager, BulletMovement movement)
    {
        this.soundManager = soundManager;
        this.movement = movement;

        if (soundManager != null)
        {
            soundManager.PlayShootSound();
        }
        movement.Initialize(bulletSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            HandleCollision(other);
            Destroy(gameObject);
        }
    }

    protected virtual void HandleCollision(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                soundManager?.PlayHitSound();
                enemy.TakeDamage(damage);
            }
        }
    }

    public virtual void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}