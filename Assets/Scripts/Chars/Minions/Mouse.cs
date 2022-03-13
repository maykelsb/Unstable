using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : Monster
{
    private Wanderable wanderable;
    //private Stallable stallable;

    private new void Start()
    {
        base.Start();

        state = State.Stalled;
        wanderable = new Wanderable();
    }

    private void FixedUpdate()
    {
        if (CheckDeath())
            return;

        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0f)
        {
            if (IsWandering() || IsBumped()) //stop wander, start stall
            {
                EnterStall();
                return;
            }

            if (IsStalled()) //stop stall, start wander
            {
                EnterWander();
                return;
            }
        }

        if (IsWandering()) // keep wandering
            wanderable.Wander(this);

        if (IsStalled()) // keep stalled
            stallable.Stall(this);
    }

    private void EnterWander() //@TODO Move to Wanderable
    {
        state = State.Wandering;
        remainingTime = Random.Range(0.5f, 1.5f);
        wanderable.WanderingDirection = Windrose.getRandomDirection();
        wanderable.Wander(this);
    }
}
