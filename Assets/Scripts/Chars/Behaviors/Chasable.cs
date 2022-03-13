using UnityEngine;

public class Chasable
{
    public void Chase(Monster monster, GameObject target)
    {
        monster.MoveTowards(
            monster.rBody.position,
            target.transform.position
        );
    }
}
