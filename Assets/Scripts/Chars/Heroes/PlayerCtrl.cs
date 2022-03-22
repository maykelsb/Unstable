using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : DamageableEntity
{
    private const int LAYER_AURA = 6;

    private Rigidbody2D rBody;
    private Animator animator;

    private enum Status { Normal, Hitted, Dead }
    private enum Condition { Normal, Poisoned/*, Cursed*/ }

    private Windrose.Direction facing;
    private float attackCooldown;
    private float invincibilityTime = 0f;
    private Status status = Status.Normal;

    [SerializeField]
    private GameObject attack;

    protected new void Start()
    {
        base.Start();

        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (status == Status.Dead)
            return;

        UpdateStatus();
        Attack();
        UpdateGUI();
    }

    private void FixedUpdate()
    {
        if (status == Status.Dead)
            return;

        Move();
    }

    void OnCollisionEnter2D(Collision2D other) // review this
    {
        GameObject collidedWith = other.gameObject;

        if (Status.Hitted != status)
            CheckEnemyCollision(collidedWith);

        CheckDoorCollision(collidedWith);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (LAYER_AURA == other.gameObject.layer)
            other.gameObject
                .GetComponent<AuraCollision>()
                .DoAttack(gameObject);

        if (Status.Hitted == status)
            return;

        CheckTraps(other.gameObject);
    }

    private void Move()
    {
        float moveX = 0f, moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = 0.001f;
            facing = Windrose.Direction.Up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -0.001f;
            facing = Windrose.Direction.Down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -0.001f;
            facing = Windrose.Direction.Left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 0.001f;
            facing = Windrose.Direction.Right;
        }

        Vector3 moveDir = new Vector3(moveX, moveY).normalized;
        rBody.velocity = moveDir * stats.GetSpeed();
    }

    private void Attack()
    {
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
            return;
        }

        if (Input.GetKey(KeyCode.L))
        {
            float incX = 0f;
            float incY = 0f;
            float incDegree = 0f;

            attackCooldown = stats.GetAttackCooldown();
            switch (facing)
            {
                case Windrose.Direction.Up:
                    incY = .75f;
                    incDegree = 90f;
                    break;
                case Windrose.Direction.Down:
                    incY = -.75f;
                    incDegree = -90f;
                    break;
                case Windrose.Direction.Left:
                    incX = -.75f;
                    break;
                case Windrose.Direction.Right:
                    incX = .75f;
                    break;
            }

            GameObject atk = Instantiate(
                attack,
                (transform.position + new Vector3(incX, incY, 0f)),
                Quaternion.Euler(0f, 0f, incDegree)
            );

            Attack scpAttack = atk.GetComponent<Attack>();
            scpAttack.Agressor = gameObject;
            scpAttack.AttackDirection = facing;

            if (0.0f > incX)
                scpAttack.FlipX();
            atk.transform.SetParent(transform);

        }
    }

    private void UpdateGUI()
    {
    }

    private void CheckDoorCollision(GameObject collidedWith) //@todo refactor - remove repetitions
    {
        GameObject destinedRoom = null;

        float moveX = 0f;
        float MoveY = 0f;

        if (collidedWith.CompareTag("DoorUp")
            && collidedWith.GetComponent<Door>().IsOpen())
        {
            destinedRoom = collidedWith.GetComponent<Door>().leadsTo;
            MoveY = 3f;
        }

        if (collidedWith.CompareTag("DoorDown")
            && collidedWith.GetComponent<Door>().IsOpen())
        {
            destinedRoom = collidedWith.GetComponent<Door>().leadsTo;
            MoveY = -3f;
        }
        if (collidedWith.CompareTag("DoorLeft")
            && collidedWith.GetComponent<Door>().IsOpen())
        {
            moveX = -3f;
            destinedRoom = collidedWith.GetComponent<Door>().leadsTo;
        }
        if (collidedWith.CompareTag("DoorRight")
            && collidedWith.GetComponent<Door>().IsOpen())
        {
            moveX = 3f;
            destinedRoom = collidedWith.GetComponent<Door>().leadsTo;
        }

        if (!destinedRoom)
            return;

        // -- Activate room
        destinedRoom.SetActive(true);
        destinedRoom.GetComponent<Room>().CheckDoors();
        // -- Change camera position to there
        GameObject.FindWithTag("MainCamera")
            .transform
            .Translate(moveX * 3, MoveY * 3, 0);
        // -- Move hero to there
        transform.Translate(moveX, MoveY, 0);
        // -- Inactivate last room
        transform.parent.gameObject.SetActive(false);
        // -- Move hero to new room
        transform.SetParent(destinedRoom.transform);
    }

    private void CheckEnemyCollision(GameObject collidedWith) // move to enemy
    {
        if (!collidedWith.CompareTag("Enemy"))
            return;

        TakeDamage(collidedWith);
        if (stats.IsAlive())
        {
            Hitted();
            return;
        }

        Die();
    }

    private void CheckTraps(GameObject collidedWith)
    {
        if (!collidedWith.CompareTag("SpikeTrap"))
            return;

        TakeDamageFromTrap(collidedWith.GetComponent<TrapStats>());
        if (stats.IsAlive())
        {
            Hitted();
            return;
        }

        Die();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SpikeTrap"))
        {
            other.gameObject.GetComponent<TrapStats>().Set();
            return;
        }

        //@todo this code bellow is horrible

        //Debug.Log("Tag: " + other.gameObject.transform.parent.gameObject.CompareTag("Enemy"));


        if (other.gameObject.transform.parent.gameObject.CompareTag("Enemy")
            && other.gameObject.transform.parent.gameObject.GetComponent<Monster>().IsChasing())
        {
            //Debug.Log("Auuu-au-a! (Aff, esse pedaço de carne não vale tanto trabalho");
            other.gameObject.transform.parent.gameObject.GetComponent<Monster>().Guard();
        }
    }

    private void Hitted()
    {
        status = Status.Hitted;
        invincibilityTime = 2f;
    }

    private void Die()
    {
        status = Status.Dead;
        animator.SetTrigger("IsDead");
        rBody.velocity = new Vector3(0, 0, 0);
    }

    protected void UpdateStatus()
    {
        if (Status.Hitted != status)
            return;

        if (invincibilityTime > 0f)
        {
            if (gameObject.GetComponent<SpriteRenderer>().color == new Color(1, 1, 1, 1))
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .25f);
            else
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            invincibilityTime -= Time.deltaTime;
            return;
        }

        status = Status.Normal;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
