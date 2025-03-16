using UnityEngine;
using UnityEngine.UI;
using System.Collections; // <--- Bổ sung dòng này


public class Character : MonoBehaviour
{
    public static Character Instance { get; private set; } // Singleton
    public int level = 1;
    public int exp = 0;
    private int health = 100;
    private int maxHealth = 100;
    private int[] expToLevelUp = { 0, 10, 20, 35, 50, 70, 95, 120, 150, 185, 225, 375,500,500,500,500,500,500,500,500,500 };
    private CharacterStats stats;
    protected SliderBar healthBar;
    private CharacterMovement characterMovement;
    private CharacterSwordHandler characterSwordHandler;
    private CharacterFireHandler characterFireHandler;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        healthBar = GetComponentInChildren<SliderBar>();
        characterMovement = GetComponent<CharacterMovement>();
        characterSwordHandler = GetComponent<CharacterSwordHandler>();
        characterFireHandler= GetComponent<CharacterFireHandler>();
    }

    void Start()
    {
        stats = new CharacterStats(level); 
        health = stats.TotalStats.health;
        StatsRefresh.Refresh(stats.TotalStats);
        RefreashHealth();
        StartCoroutine(WaitForSwordManagerAndLoadWeapon());
    }

    IEnumerator WaitForSwordManagerAndLoadWeapon()
    {
        yield return new WaitUntil(() => characterSwordHandler != null);
        yield return new WaitForSeconds(0.2f); // Thêm delay nếu cần
        LoadWeapon();
    }
    void LoadWeapon(){
        
        int playerType = GameManager.Instance.GetPlayerType();
        characterFireHandler.SetIsFire(true);
        AddSwordLevel(1);
        // switch (playerType)
        // {
        //     case 1:
        //         characterFireHandler.SetIsFire(true);
        //         break;
        //     case 2:
        //         AddSwordLevel(1);
        //         break;
            
        //     default:
        //         break;
        // }
    }

    public void AddExp(int amount)
    {
        exp += amount;
        while (level < expToLevelUp.Length - 1 && exp >= GetMaxExp())
        {
            exp -= GetMaxExp();
            LevelUp();
        }
        exp = Mathf.Min(exp, GetMaxExp());
        
        GameEvents.ShowFloatingText(transform.position, amount,FloatingType.AddExp);
        ScoreEvent.RaiseExpUpdated(exp, GetMaxExp(), level);
    }

    public void AddHealth(int amount)
    {
        health = Mathf.Min(health + amount,maxHealth);
        
        GameEvents.ShowFloatingText(transform.position, amount,FloatingType.AddBlood);
        RefreashHealth();
        
    }
    public void AddSwordLevel(int amount)
    {
        characterSwordHandler.LevelUp(amount);
        
    }

    public int GetMaxExp()
    {
        return expToLevelUp[level];
    }

    private void LevelUp()
    {
        level++;
        stats.SetLevel(level);
        health = stats.TotalStats.health;
        RefreashHealth();
        StatsRefresh.Refresh(stats.TotalStats);
        Debug.Log(JsonUtility.ToJson(stats.TotalStats, true));
    }

    private void RefreashHealth()
    {
        maxHealth = stats.TotalStats.health;
        healthBar.UpdateSliderBar(health, maxHealth);
    }

    public int GetDamage()
    {
        return stats.TotalStats.attack;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateSliderBar(health, maxHealth);
        GameEvents.ShowFloatingText(transform.position, damageAmount,FloatingType.ExceptBlood);
        if (health <= 0)
        {
            Die();
        }
    }
    public Attr GetCharacterStats(){
        
        return stats.TotalStats;
    }

    private void Die()
    {
        characterMovement.Die();
        GameEvents.GameOver();
    }

}
