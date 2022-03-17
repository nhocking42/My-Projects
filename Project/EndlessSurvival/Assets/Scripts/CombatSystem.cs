using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;
    public LayerMask animalLayer;
    public int attackDamage;
    public float attackRate;
    float nextAttackTime = 0f;

    public int weapon;
    // 1: sword
    // 2: axe
    // 3: pickaxe
    public GameObject MainCharacter;

    private void Start()
    {
        weapon = 0;
    }


    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            
            if (Input.GetKeyDown(KeyCode.Space) && MainCharacter.GetComponent<CharacterMovement>().deathState == false)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        checkWeaponInHand();

    }

    void checkWeaponInHand()
    {
        if (weapon == 0)
        {
            attackDamage = 2;
            attackRate = 2f;
            attackRange = 0.5f;
            animator.SetInteger("itemInHand", 0);
        }
        if (weapon == 1)
        {
            attackDamage = 12;
            attackRate = 3f;
            attackRange = 1f;
            animator.SetInteger("itemInHand", 1);
        }
        if (weapon == 2)
        {
            attackDamage = 15;
            attackRate = 1.5f;
            attackRange = 1f;
            animator.SetInteger("itemInHand", 2);
        }
        if (weapon == 3)
        {
            attackDamage = 8;
            attackRate = 1.5f;
            attackRange = 1f;
            animator.SetInteger("itemInHand", 3);
        }
    }
    void Attack()
    {
        MainCharacter.GetComponent<CharacterMovement>().stopMoving();
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDamage(attackDamage);
        }

        Collider2D[] hitAnimals = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, animalLayer);
        foreach (Collider2D animal in hitAnimals)
        {
            animal.GetComponent<Animal>().takeDamage(attackDamage);
        }
        if (weapon == 0)
        {
            SoundManager.PlaySound("attack_hand");
        }
        else
        {
            SoundManager.PlaySound("attack_weapon");
        }

    }
    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
