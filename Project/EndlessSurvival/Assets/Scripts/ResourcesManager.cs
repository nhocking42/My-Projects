using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesManager : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public GameObject[] droppedItem;

    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            broken();
        }

    }

    public void takeDamage(int number)
    {
        health -= number;
    }

    public void broken()
    {
        for (int i=0; i<droppedItem.Length; i++)
        {
            Instantiate(droppedItem[i], new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f),
                                                    transform.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity);

        }
        Destroy(this.gameObject);
    }
}
