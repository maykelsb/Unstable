using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraCollision : MonoBehaviour
{
    private IAttack monster;

    public void Start()
    {
        monster = transform.parent.GetComponent<IAttack>();
    }

    public void DoAttack(GameObject target)
    {
        monster.Attack(target);
    }
}
