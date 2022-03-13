using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hound : Monster, IAttack
{
    // Behaviors
    private Wanderable wanderable;
    private Chasable chasable;

    private GameObject target;

    private new void Start()
    {
        base.Start();

        wanderable = new Wanderable();
        chasable = new Chasable();
    }

    private void FixedUpdate()
    {
        if (CheckDeath())
            return;

        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0f)
        {
            if (IsWandering() || IsBumped())
            {
                EnterStall();
                return;
            }

            if (IsStalled())
            {
                EnterWander();
                return;
            }
        }

        if (IsWandering())
            wanderable.Wander(this);

        if (IsStalled())
            stallable.Stall(this);

        if (IsChasing())
            chasable.Chase(this, target);
    }

    public void Attack(GameObject target)
    {
        this.target = target;
        state = State.Chasing;
    }

    public void EnterWander() //@TODO Move to Wanderable
    {
        state = State.Wandering;
        remainingTime = Random.Range(0.5f, 1.5f); //@TODO Criar um ResetRemainingTime ou SetRemainingTime
        wanderable.WanderingDirection = Windrose.getRandomDirection();
        wanderable.Wander(this);
    }

    protected override void Die()
    {
        Destroy(transform.GetChild(0).gameObject);
        base.Die();
    }
}
