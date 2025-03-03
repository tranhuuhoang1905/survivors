using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public static Character Instance { get; private set; } // Singleton
    public int level = 1;
    public int exp = 0;
    private int health = 100;
    private int maxHealth = 1;
    private int[] expToLevelUp = { 0, 10, 20, 35, 50, 70, 95, 120, 150, 185, 225, 375,500,500,500,500,500,500,500,500,500 };
    private CharacterStats stats;
    protected SliderBar healthBar;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        healthBar = GetComponentInChildren<SliderBar>();
    }

    void Start()
    {
        stats = new CharacterStats(level); 
        StatsRefresh.Refresh(stats.TotalStats);
        RefreashHealth();
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

    public int GetMaxExp()
    {
        return expToLevelUp[level];
    }

    private void LevelUp()
    {
        level++;
        stats.SetLevel(level);
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
        return stats.TotalStats.damage;
    }

}
