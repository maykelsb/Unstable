using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string job;
    public string charName;
    public int age;
    public string background;
    public string preferredFood;
    public string weaponName;

    public float skill = 1;
    public float vigor = 1;
    public float speed = 1;
    public int range = 1; // max 3

    public float fullLife;
    public float life;

    public float defense;

    private void Start()
    {
        fullLife = life = (vigor * 5);
        defense = vigor;
    }

    public float GetAttackCooldown()
    {
        return (.3f);
    }

    public float GetDamage()
    {
        return (skill * 2);
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
}
