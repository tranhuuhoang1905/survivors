using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] float SpantSpeed = 1f;
    public Transform bulletPool;
    Animator myAnimator;
    
    PlayerMovement playerMovement;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        // Nếu bulletPool chưa được gán, tạo mới một GameObject "BulletPool"
        if (bulletPool == null)
        {
            GameObject poolObject = new GameObject("BulletPool");
            bulletPool = poolObject.transform;
        }
        
        InvokeRepeating("AutoFire", 1f, SpantSpeed);
    }

    // void OnFire(InputValue value)
    // {
    //     if (!playerMovement.IsAlive) return;

    //     float direction = Mathf.Sign(transform.localScale.x);
    //     Quaternion bulletRotation = direction > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

    //     // Instantiate viên đạn và đặt vào bulletPool
    //     GameObject newBullet = Instantiate(bullet, gun.position, bulletRotation);
    //     newBullet.transform.SetParent(bulletPool);
    // }

    public void AutoFire()
    {
        if (!playerMovement.IsAlive) return;
        
        myAnimator.SetTrigger("IsActack");
        Invoke("InstantiateBullet", 0.2f);
    }

    void InstantiateBullet()
{
    float direction = Mathf.Sign(transform.localScale.x);
    Quaternion bulletRotation = direction > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

    Transform nearestEnemy = FindNearestEnemy();
    if (nearestEnemy != null)
    {
        // Tính toán góc quay để viên đạn hướng đến enemy
        Vector2 directionToEnemy = (nearestEnemy.position - gun.position).normalized;
        float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
        
        // Tạo góc quay chỉ trên trục Z (đúng cho game 2D)
        bulletRotation = Quaternion.Euler(0, 0, angle);
    }

    // Instantiate viên đạn và đặt vào bulletPool
    GameObject newBullet = Instantiate(bullet, gun.position, bulletRotation);
    newBullet.transform.SetParent(bulletPool);
}

    private Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Lấy tất cả enemy trong scene
        Transform nearestEnemy = null;
        float nearestDistance = 999999; 

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
    }

}