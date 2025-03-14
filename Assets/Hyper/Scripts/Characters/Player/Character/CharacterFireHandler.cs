using UnityEngine;
using System.Collections;

public class CharacterFireHandler : MonoBehaviour
{
    [SerializeField] private bool isFire = false;
    private BulletSystem bulletSystem;
    private CharacterMovement characterMovement;
    private EnemyTracker enemyTracker;
    private Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        bulletSystem = GetComponent<BulletSystem>();
        enemyTracker = GetComponent<EnemyTracker>();

        InvokeRepeating("AutoFire", 1f, 1f);
    }

    public void FireSkill()
    {
        if (!characterMovement.IsAlive) return;
        if (GameManager.Instance.GetScore() < 20) return;

        ScoreEntry scoreEntry = new ScoreEntry(ScoreType.Score, -20);
        GameManager.Instance.AddToScore(scoreEntry);

        StartCoroutine(FireMultipleRounds());
    }

    IEnumerator FireMultipleRounds()
    {
        for (int i = 0; i < 3; i++)
        {
            FireBurst();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void FireBurst()
    {
        float angleStep = 360f / 20;
        float startAngle = 0f;

        for (int i = 0; i < 20; i++)
        {
            float angle = startAngle + (angleStep * i);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);
            bulletSystem.SpawnBullet(bulletRotation);
        }
    }

    public void AutoFire()
    {
        if (!characterMovement.IsAlive || !isFire) return;

        myAnimator.SetTrigger("IsActack");
        Invoke("InstantiateBullet", 0.2f);
    }

    void InstantiateBullet()
    {
        float direction = Mathf.Sign(transform.localScale.x);
        Quaternion bulletRotation = direction > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

        Transform nearestEnemy = enemyTracker.FindNearestEnemy();
        if (nearestEnemy != null)
        {
            Vector3 positionAttack = FindPositionAttack(nearestEnemy);

            Vector2 directionToEnemy = (positionAttack - bulletSystem.transform.position).normalized;
            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
            bulletRotation = Quaternion.Euler(0, 0, angle);
        }

        bulletSystem.SpawnBullet(bulletRotation);
    }
    private Vector3 FindPositionAttack(Transform enemy){
        Transform healthBar = enemy.transform.Find("Canvas");
        if (!healthBar) return enemy.transform.position;
        Vector3 positionAttack = (healthBar.transform.position + enemy.transform.position) / 2;        
        return positionAttack;
    }

    public void SetIsFire(bool flag)
    {
        isFire = flag;
    }

    void SpantSpeedRefresh(Attr totalStats)
    {
        float newFireRate = 1 / totalStats.attackSpeed;
        bulletSystem.UpdateFireRate(newFireRate);
    }
}
