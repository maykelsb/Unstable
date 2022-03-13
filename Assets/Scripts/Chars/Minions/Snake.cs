using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Monster, IAttack
{
    // Behaviors
    //private Lungable lungable; //@todo Is this worth? oO

    private Vector3 targetLastSeenPosition;

    private void FixedUpdate()
    {
        if (CheckDeath())
            return;

        if (IsStalled())
        {
            stallable.Stall(this);
            return;
        }

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            if (IsPreparing())
            {
                EnterAttack();
                return;
            }

            if (IsBumped())
            {
                EnterStall();
                return;
            }
        }
    }

    private void EnterAttack()
    {
        rBody.MovePosition(targetLastSeenPosition);
        CancelInvoke("Flip");
        state = State.Stalled;
    }

    private void EnterPrepare()
    {
        state = State.Preparing;
        remainingTime = 1.5f;
        InvokeRepeating("Flip", 0.0f, 0.25f);
    }

    public void Attack(GameObject target)
    {
        targetLastSeenPosition = target.transform.position;
        EnterPrepare();
    }

    protected override void Die()
    {
        Destroy(transform.GetChild(0).gameObject);
        CancelInvoke("Flip");
        base.Die();
    }
}
