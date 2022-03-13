using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStats : MonoBehaviour
{

    public float GetDamage()
    {
        return 5;
    }

    public void Spring()
    {
        gameObject.GetComponent<Animator>().SetTrigger("HasSprung");
    }

    public void Set()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Setted");
    }
}
