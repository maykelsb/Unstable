using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    /**
     * Garret/Warrior           |Norton/Cruzader |Daermun Drunkfoot/Barbarian |
     * Skill: 2                 |Skill: 2        |Skill: 2                    |
     * Speed: 2                 |Speed: 1        |Speed: 1                    |
     * Range: 1 (1+((R-1) *.5)) |Range: 2        |Range: 1                    |
     * Defense: 2               |Defense: 2      |Defense: 2                  |
     * Life: 2 (x3)             |Life: 2         |Life: 3                     |
     * -------------------------|----------------|----------------------------|
     * Venna Valphyra/Archer
     * Skill: 1
     * Speed: 3
     * Range: 3
     * Defense: 0
     * Life: 2
     * -------------------------|----------------|----------------------------|
     * */

    public string job;
    public string charName;
    public int age;
    public string background;
    public string preferredFood;
    public string weaponName;

    private int level = 1;

    [SerializeField]
    private int skill = 1;
    [SerializeField]
    private int speed = 1;
    [SerializeField]
    private int range = 1; 
    [SerializeField]
    private int defense;
    [SerializeField]
    private int life;

    private float fullLife;
    private float currentLife;

    private void Start()
    {
        fullLife = currentLife = (this.GetLife() * 3);
    }

    private int GetSkill()
    {
        return (this.level + this.skill);
    }

    public int GetDefense()
    {
        return (this.level + this.defense);
    }

    private int GetLife()
    {
        return (this.level + this.life);
    }

    public int GetSpeed()
    {
        return (this.speed);
    }

    private float GetRange()
    {
        return (1 + ((this.range - 1) * .5f));
    }

    public float GetCurrentLife()
    {
        return this.currentLife;
    }

    public float GetFullLife()
    {
        return this.fullLife;
    }

    public float GetAttackCooldown()
    {
        return (.3f);
    }

    public float GetDamage()
    {
        return this.GetSkill();
    }

    public bool IsAlive()
    {
        return (life > 0);
    }

    public float CalculateDamage(float defense)
    {
        return Mathf.Max(
            (GetDamage() - defense),
            skill
        );
    }

    public Stats TakeDamage(float damage)
    {
        this.currentLife -= damage;
        return this;
    }
}
