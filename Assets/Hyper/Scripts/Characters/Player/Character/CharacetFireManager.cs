using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CharacetFireManager : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] float SpantSpeed = 1f;
    [SerializeField] bool isFire = false;
    public Transform bulletPool;
    private Character playerCharacter;
    Animator myAnimator;
    
    CharacterMovement characterMovement;

    void Awake()
    {
        StatsRefresh.OnRefresh += SpantSpeedRefresh; // Đăng ký sự kiện
    }


    void Start()
    {
        myAnimator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
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

    /// 🔥 Hàm bắn 3 lượt, mỗi lượt 20 viên đạn
    public void FireSkill()
    {
        if (!characterMovement.IsAlive) return;
        if (GameManager.Instance.GetScore() < 20) return;
        ScoreEntry scoreEntry = new ScoreEntry(1, -20);
        GameManager.Instance.AddToScore(scoreEntry);
        StartCoroutine(FireMultipleRounds());
    }

    IEnumerator FireMultipleRounds()
    {
        for (int i = 0; i < 3; i++) // Bắn 3 lượt
        {
            FireBurst();
            yield return new WaitForSeconds(0.2f); // Chờ 0.2s trước khi bắn lượt tiếp theo
        }
    }

    /// 🔥 Hàm bắn 20 viên đạn theo 20 hướng khác nhau
    void FireBurst()
    {
        float angleStep = 360f / 20; // Chia đều 360 độ cho 20 viên đạn
        float startAngle = 0f; // Bắt đầu từ góc 0 độ

        for (int i = 0; i < 20; i++)
        {
            float angle = startAngle + (angleStep * i);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);
            
            SpawnBullet(bulletRotation);
        }
    }

    /// 🏹 Hàm tạo viên đạn
    void SpawnBullet(Quaternion bulletRotation)
    {
        int damage = playerCharacter.GetDamage();
        GameObject newBullet = Instantiate(bullet, gun.position, bulletRotation);
        newBullet.transform.SetParent(bulletPool);
        
        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(damage); // Gán damage cho viên đạn
        }
    }

    public void AutoFire()
    {
        if (!characterMovement.IsAlive) return;
        if (!isFire) return;
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

        SpawnBullet(bulletRotation);

        // // Instantiate viên đạn và đặt vào bulletPool
        // int damage = playerCharacter.GetDamage();
        // GameObject newBullet = Instantiate(bullet, gun.position, bulletRotation);
        // newBullet.transform.SetParent(bulletPool);
        // Bullet bulletScript = newBullet.GetComponent<Bullet>();
        // if (bulletScript != null)
        // {
        //     bulletScript.SetDamage(damage); // ✅ Gán damage đúng cách
        // }
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
    public void SetIsFire(bool flag)
    {
        isFire= flag;
    }


}