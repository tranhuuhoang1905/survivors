using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public int level;
    public Attr baseStats;
    public Attr StatsUpLevel;
    public Attr itemBonus;

    public CharacterStats(int startLevel)
    {
        level = startLevel;
        baseStats = new Attr(
            damage: 1,
            attack: 5,
            swordAttack: 3,
            attackSpeed: 1f,
            swordSpeed: 5f,
            moveSpeed: 5f,
            armor: 5,
            health: 100
        );
        StatsUpLevel =new Attr(
            damage: 10,
            attack: 5,
            swordAttack: 2,
            attackSpeed: 0.3f,
            swordSpeed: 0.2f,
            moveSpeed: 0.5f,
            armor: 1,
            health: 25
        );
        itemBonus = new Attr();
    }

    public Attr TotalStats => baseStats + itemBonus;

    public void SetLevel(int newLevel)
    {

        int levelUp = Mathf.Max(0, newLevel - level);
        level = newLevel;
        baseStats += StatsUpLevel * levelUp;
    }
}
