using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : DamageableEntity
{
    protected enum State { Stalled, Walking, Preparing, Chasing, Wandering, Dead, Bumped }
    protected State state;

    protected Bumpable bumpable;
    protected Stallable stallable;

    public Rigidbody2D rBody;
    protected Animator animator;

    public float remainingTime;

    [SerializeField] private int threatLevel;

    public global::System.Int32 ThreatLevel { get => threatLevel; set => threatLevel = value; }

    public int GetExperiencePoints()
    {
        return (this.ThreatLevel * 10);
    }

    protected new void Start()
    {
        base.Start();

        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        state = State.Stalled;
        stallable = new Stallable();
        bumpable = new Bumpable();

        ConfigEffect(EffectType.Monster);
    }

    public bool IsStalled()
    {
        return (State.Stalled == state);
    }

    public bool IsPreparing()
    {
        return (State.Preparing == state);
    }

    public bool IsChasing()
    {
        return (State.Chasing == state);
    }

    public bool IsWandering()
    {
        return (State.Wandering == state);
    }

    public bool IsDead()
    {
        return (State.Dead == state);
    }

    public bool IsBumped()
    {
        return (State.Bumped == state);
    }

    protected bool IsAlive()
    {
        return stats.IsAlive();
    }

    public void Guard()
    {
        state = State.Stalled;
        remainingTime = 0.5f; //@TODO Criar um ResetRemainingTime ou SetRemainingTime
    }

    protected virtual void Die() //@todo Has to interact with Stats TakeDamage
    {
        state = State.Dead;
        animator.SetTrigger("IsDead");
        StopDamageEffect();
        Destroy(rBody);
        Destroy(GetComponent<BoxCollider2D>());
    }

    public void Flip() //@TODO Move this to wanderable??
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
    }

    public void Flip(bool flip)
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = flip;
    }

    public Monster SetVelocity(Vector3 newVelocity)
    {
        rBody.velocity = newVelocity * stats.GetSpeed();

        return this;
    }

    public Monster Impulse(Vector2 impulse)
    {
        rBody.AddForce(
            impulse,
            ForceMode2D.Impulse
        );

        return this;
    }

    public void MoveTowards(Vector3 from, Vector3 to) //@TODO Move to Chasable
    {
        rBody.position = Vector3.MoveTowards(
            from,
            to,
            (stats.GetSpeed() * Time.deltaTime)
        );
    }

    public bool CheckDeath()
    {
        if (IsDead())
            return true;

        if (!stats.IsAlive())
        {
            Die();
            return true;
        }

        return false;
    }

    public void TakeDamage(GameObject agressor, Windrose.Direction attackDirection)
    {
        CancelInvoke(); //@TODO this is used only to Snake, keep?
        base.TakeDamage(agressor);
        this.state = State.Bumped;
        bumpable.Bump(this, attackDirection);
    }

    protected void EnterStall() //@TODO Move to Stallable
    {
        state = State.Stalled;
        remainingTime = 0.5f;
        stallable.Stall(this);
    }
}
