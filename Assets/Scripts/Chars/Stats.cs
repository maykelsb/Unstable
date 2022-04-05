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

    protected int level = 1;

    [SerializeField] protected int attrSkill = 1;
    [SerializeField] protected int attrSpeed = 1;
    [SerializeField] protected int attrRange = 1; 
    [SerializeField] protected int attrDefense = 1;
    [SerializeField] protected int attrLife = 1;

    protected float fullLife = 0f;
    protected float currentLife;

    //protected void Start()
    protected void Awake() //@todo Understand why Start is not called to set values
    {
        if (0f == this.GetFullLife())
            this.fullLife = this.currentLife = (this.GetLife() * 3);
    }

    protected int GetSkill()
    {
        return (this.level + this.attrSkill);
    }

    protected int GetLife()
    {
        return (this.level + this.attrLife);
    }

    protected float GetRange()
    {
        return (1 + ((this.attrRange - 1) * .5f));
    }

    public int GetDefense()
    {
        return (this.level + this.attrDefense);
    }

    public int GetSpeed()
    {
        return (this.level + this.attrSpeed);
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
        return (this.GetCurrentLife() > 0);
    }

    public float CalculateDamage(float defense)
    {
        return Mathf.Max(
            (GetDamage() - defense),
            attrSkill
        );
    }

    public Stats TakeDamage(float damage)
    {
        this.currentLife -= damage;
        Debug.Log(gameObject + "Life: " + this.GetCurrentLife() + "/" + this.GetFullLife());
        return this;
    }
}
