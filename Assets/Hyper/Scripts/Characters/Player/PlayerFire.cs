using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] float SpantSpeed = 1f;
    public Transform bulletPool;
    private Character playerCharacter;
    Animator myAnimator;
    
    PlayerMovement playerMovement;

    void Awake()
    {
        StatsRefresh.OnRefresh += SpantSpeedRefresh; // Đăng ký sự kiện
    }


    void Start()
    {
        myAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCharacter = GetComponent<Character>();
        // Nếu bulletPool chưa được gán, tạo mới một GameObject "BulletPool"
        if (bulletPool == null)
        {
            GameObject poolObject = new GameObject("BulletPool");
            bulletPool = poolObject.transform;
        }
        
        InvokeRepeating("AutoFire", 1f, SpantSpeed);
    }

    void OnDestroy()
    {
        StatsRefresh.OnRefresh -= SpantSpeedRefresh; // Đăng ký sự kiện
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
        int damage = playerCharacter.GetDamage();
        GameObject newBullet = Instantiate(bullet, gun.position, bulletRotation);
        newBullet.transform.SetParent(bulletPool);
        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(damage); // ✅ Gán damage đúng cách
        }
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

    void SpantSpeedRefresh(Attr totalStats)
    {
        SpantSpeed = 1/totalStats.attackSpeed;
        Debug.Log($"Check update SpantSpeed: {SpantSpeed}");
        UpdateFireRate();
    }
    void UpdateFireRate()
    {
        CancelInvoke("AutoFire"); // 🔥 Hủy bắn tự động cũ
        InvokeRepeating("AutoFire", 0f, SpantSpeed); // 🔥 Gọi lại với tốc độ mới
    }


}