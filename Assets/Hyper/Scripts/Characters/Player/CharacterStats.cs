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
        baseStats = new Attr(1, 5, 1f, 5f, 5, 100);
        StatsUpLevel = new Attr(10, 1, 0.1f, 0.5f, 1, 10);
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
