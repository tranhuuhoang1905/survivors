using UnityEngine;

[System.Serializable]
public class Attr
{
    public int damage;
    public int attack;
    public float attackSpeed;
    public float moveSpeed;
    public int armor;
    public int health;

    // Constructor mặc định
    public Attr()
    {
        damage = 0;
        attack = 0;
        attackSpeed = 0;
        moveSpeed = 0;
        armor = 0;
        health = 0;
    }

    // Constructor có tham số
    public Attr(int dmg, int atk, float atkSpd, float moveSpd, int arm, int hp)
    {
        damage = dmg;
        attack = atk;
        attackSpeed = atkSpd;
        moveSpeed = moveSpd;
        armor = arm;
        health = hp;
    }

    // Cộng chỉ số từ item hoặc buff
    public static Attr operator +(Attr a, Attr b)
    {
        return new Attr(
            a.damage + b.damage,
            a.attack + b.attack,
            a.attackSpeed + b.attackSpeed,
            a.moveSpeed + b.moveSpeed,
            a.armor + b.armor,
            a.health + b.health
        );
    }
    // Nhân Attr với một số (levelUp)
    public static Attr operator *(Attr a, int multiplier)
    {
        return new Attr(
            a.damage * multiplier,
            a.attack * multiplier,
            a.attackSpeed * multiplier,
            a.moveSpeed * multiplier,
            a.armor * multiplier,
            a.health * multiplier
        );
    }
}