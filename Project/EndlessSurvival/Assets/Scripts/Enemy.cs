using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public int enemyHealth;
    public HealthBar enemyHealthBar;
    public int enemyMaxHealth;

    public GameObject deathEffect;

    public Collider2D myCollider;
    public Collider2D[] otherCollider;

    public GameObject[] dropItem;

    void Start()
    {
        enemyHealthBar.SetMaxHealth(enemyMaxHealth);
        enemyHealth = enemyMaxHealth;

        for (int i = 0; i < otherCollider.Length; i++)
        {
            Physics2D.IgnoreCollision(myCollider, otherCollider[i], true);
        }

    }

    private void Update()
    {
        
        enemyHealthBar.transform.position = new Vector2(transform.position.x, transform.position.y + 1);

    }
    
    public void takeDamage(int dmg)
    {
        enemyHealth -= dmg;
        enemyHealthBar.SetHealth(enemyHealth);

        if(enemyHealth <= 0)
        {
            Die();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "campfire")
        {
            takeDamage(1);
        }
    }

    void Die()
    {
        for (int i = 0; i < dropItem.Length; i++)
        {
            Instantiate(dropItem[i], transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject,0.2f);
        GameObject dEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(dEffect,0.9f);
        SoundManager.PlaySound("kill_mob");

    }

    
}
