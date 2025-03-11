using UnityEngine;

[System.Serializable]
public class Attr
{
    public int damage;
    public int attack;
    public float attackSpeed;
    public int swordAttack;
    public float swordSpeed;
    public float moveSpeed;
    public int armor;
    public int health;

    // Constructor mặc định
    public Attr()
    {
        damage = 0;
        attack = 0;
        swordAttack = 0;
        attackSpeed = 0;
        swordSpeed = 0;
        moveSpeed = 0;
        armor = 0;
        health = 0;
    }

    // Constructor có tham số
    public Attr(int dmg, int atk, int swordAtk, float atkSpd, float swordSpd, float moveSpd, int arm, int hp)
    {
        damage = dmg;
        attack = atk;
        swordAttack = swordAtk;
        attackSpeed = atkSpd;
        swordSpeed = swordSpd;
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
            a.swordAttack + b.swordAttack,
            a.attackSpeed + b.attackSpeed,
            a.swordSpeed + b.swordSpeed,
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
            a.swordAttack * multiplier,
            a.attackSpeed * multiplier,
            a.swordSpeed * multiplier,
            a.moveSpeed * multiplier,
            a.armor * multiplier,
            a.health * multiplier
        );
    }
}