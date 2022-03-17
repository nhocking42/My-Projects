using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float enemySpeed;
    float defaultSpeed;
    float timerToMoveContinous;
    public float checkRadius;

    public bool shouldRotate;

    public LayerMask isPlayer;

    private Transform target;
    private Rigidbody2D rb;
    public Vector2 movement;
    private Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;

    public float attackRange;
    public int attackDamage;
    public float attackDelay;
    float lastAttack;

    public Transform enemyAttackPoint;

    Vector2 VTCP;
    float corner;
    public Animator ani;

    private void Start()
    {
        defaultSpeed = enemySpeed;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        timerToMoveContinous = 0;
    }

    private void Update()
    {
        if (timerToMoveContinous > 0)
        {
            timerToMoveContinous -= Time.deltaTime;
        }
        else
        {
            enemySpeed = defaultSpeed;
        }
        checkCornerForSprites();

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, isPlayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if (isInChaseRange && !isInAttackRange && distanceToPlayer > attackRange/2)
        {
            MoveCharacter(movement);
        }

        if (isInChaseRange && distanceToPlayer > attackRange)
        {
            ani.SetBool("isMoving", true);
        }
        else
        {
            ani.SetBool("isMoving", false);
        }
        if (distanceToPlayer < attackRange)
        {
            if (Time.time > lastAttack + attackDelay)
            {
                EnemyAttack();
                lastAttack = Time.time;
            }
        }

    }
    private void EnemyAttack()
    {
        enemySpeed = 0;
        timerToMoveContinous = attackDelay;
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemyAttackPoint.position, attackRange, isPlayer);
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<CharacterMovement>().takeDamage(attackDamage);
        }
        ani.SetTrigger("Attack");

        switch (this.gameObject.name)
        {
            case "Bad Flower(Clone)":
                SoundManager.PlaySound("bad_flower");
                break;
            case "Red Bird(Clone)":
                SoundManager.PlaySound("red_bird");
                break;
            case "Big Salamander(Clone)":
                SoundManager.PlaySound("big_salamander");
                break;
            case "Cruel Insect(Clone)":
                SoundManager.PlaySound("cruel_insect");
                break;
            case "Old Golem(Clone)":
                SoundManager.PlaySound("old_golem");
                break;
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        float dis = Vector3.Distance(dir, transform.position);
        if(dis > 0.5f)
        {
            rb.MovePosition((Vector2)transform.position + (dir * enemySpeed * Time.deltaTime));
        }
    }

    void checkCornerForSprites()
    {
        VTCP = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        corner = Vector2.Angle(new Vector2(0, 1), VTCP);

        if (corner <= 45 && corner != 0)
        {
            ani.SetInteger("currentDirection", 0);
        }
        if (corner >= 135)
        {
            ani.SetInteger("currentDirection", 2);
        }
        if (corner < 135 && corner > 45)
        {
            if (target.position.x > transform.position.x)
            {
                ani.SetInteger("currentDirection", 1);
            }
            else
            {
                ani.SetInteger("currentDirection", 3);
            }

        }

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(enemyAttackPoint.position, attackRange);

    }
}
