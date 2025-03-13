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
        baseStats    = new Attr(1,  5, 3, 1f  ,2f,   5f,  5, 100);
        StatsUpLevel = new Attr(10, 5, 2, 0.3f,0.2f, 0.5f, 1, 25);
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
