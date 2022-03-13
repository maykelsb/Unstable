using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int range;
    private float attackDurantion;

    private Windrose.Direction attackDirection;
    public Windrose.Direction AttackDirection {
        get => attackDirection;
        set => attackDirection = value;
    }

    private GameObject agressor;
    public GameObject Agressor { get => agressor; set => agressor = value; }


    private void Start()
    {
        attackDurantion = .2f;
    }

    private void Update()
    {
        if (attackDurantion > 0.0f)
        {
            attackDurantion -= Time.deltaTime;
            return;
        }
        GameObject.Destroy(gameObject, 0f);
    }

    public void FlipX()
    {
        transform.localScale += new Vector3(-2, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;

        DamageableEntity de = other.GetComponent<DamageableEntity>();
        if (de.AmIEnemy())
        {
            ((Monster)de).TakeDamage(Agressor, AttackDirection);
            return;
        }
    }

}
