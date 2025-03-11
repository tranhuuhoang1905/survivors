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
    private CharacterSwordManager characterSwordManager;
    private CharacetFireManager characetFireManager;

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
        characterSwordManager = GetComponent<CharacterSwordManager>();
        characetFireManager= GetComponent<CharacetFireManager>();
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
        yield return new WaitUntil(() => characterSwordManager != null);
        yield return new WaitForSeconds(0.2f); // Thêm delay nếu cần
        LoadWeapon();
    }
    void LoadWeapon(){
        
        int playerType = GameManager.Instance.GetPlayerType();
        switch (playerType)
        {
            case 1:
                characetFireManager.SetIsFire(true);
                break;
            case 2:
                AddSwordLevel(1);
                break;
            
            default:
                break;
        }
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

        ScoreEvent.RaiseExpUpdated(exp, GetMaxExp(), level);
    }

    public void AddHealth(int amount)
    {
        health = Mathf.Min(health + amount,maxHealth);
        
        RefreashHealth();
        
    }
    public void AddSwordLevel(int amount)
    {
        characterSwordManager.LevelUp(amount);
        
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
        if (health <= 0)
        {
            Die();
        }
    }
    public Attr GetCharacterStats(){
        
        Debug.Log($"check Character GetCharacterStats TotalStats{stats.TotalStats}");
        return stats.TotalStats;
    }

    private void Die()
    {
        // Time.timeScale = 0;
        characterMovement.Die();
        GameEvents.GameOver();
    }

}
