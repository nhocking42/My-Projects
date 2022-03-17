using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float speed;
    Vector2 randomMovePos;
    float timerToMove;

    Vector2 VTCP;
    public float corner;
    public Animator ani;

    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;

    public GameObject deathEffect;

    public Collider2D myCollider;
    public Collider2D otherCollider;

    public GameObject[] dropItem;
    void Start()
    {
        Physics2D.IgnoreCollision(myCollider, otherCollider, true);
        corner = 0;
        setRandomMovePositionNearly();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        timerToMove = 5;
    }

    // Update is called once per frame
    void Update()
    {
        checkCornerForSprites();

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, randomMovePos, step);

        timerToMove -= Time.deltaTime;
        if (timerToMove <= 0)
        {
            setRandomMovePositionNearly();
            timerToMove = 5;
        }

        healthBar.transform.position = new Vector2(transform.position.x, transform.position.y + 1);

    }

    void setRandomMovePositionNearly()
    {
        randomMovePos.x = Random.Range(transform.position.x - 7, transform.position.x + 7);
        randomMovePos.y = Random.Range(transform.position.y - 7, transform.position.y + 7);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "campfire")
        {
            takeDamage(1);
        }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            die();
        }
        else
        {
            switch (this.gameObject.name)
            {
                case "Cow(Clone)":
                    SoundManager.PlaySound("cow");
                    break;
                case "Chicken(Clone)":
                    SoundManager.PlaySound("chicken");
                    break;
                case "Pig(Clone)":
                    SoundManager.PlaySound("pig");
                    break;
                case "Sheep(Clone)":
                    SoundManager.PlaySound("sheep");
                    break;

            }
        }
    }

    public void die()
    {
        for (int i = 0; i < dropItem.Length; i++)
        {
            Instantiate(dropItem[i], transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject, 0.2f);
        GameObject dEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(dEffect, 0.9f);
        SoundManager.PlaySound("kill_mob");
    }

    void checkCornerForSprites()
    {
        VTCP = new Vector2(randomMovePos.x - transform.position.x, randomMovePos.y - transform.position.y);
        corner = Vector2.Angle(new Vector2(0, 1), VTCP);

        if (corner <= 45 && corner != 0)
        {
            ani.SetInteger("Direction", 0);
        }
        if (corner >= 135)
        {
            ani.SetInteger("Direction", 2);
        }
        if (corner < 135 && corner > 45)
        {
            if (randomMovePos.x > transform.position.x)
            {
                ani.SetInteger("Direction", 1);
            }
            else
            {
                ani.SetInteger("Direction", 3);
            }

        }
    }
}
