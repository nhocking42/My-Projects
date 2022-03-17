using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemy;
    int randomSpawnPoint, randomEnemy;
    public float delaySecond;

    public float activeTime;
    float defaultActiveTime;

    public float lifeTime;
    void Start()
    {
        defaultActiveTime = activeTime;
    }
    void Update()
    {
        if (activeTime > 0)
        {
            activeTime -= Time.deltaTime;
        }
        else
        {
            CancelInvoke();
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            activeTime = defaultActiveTime;
            InvokeRepeating("SpawnEnemy", 4f, delaySecond);
        }
        //if (collider.gameObject.tag == null)
        //{
        //    Destroy(gameObject);
        //    gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //}

    }
    

    void SpawnEnemy()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length - 1);
        randomEnemy = Random.Range(0, enemy.Length - 1);
        GameObject enemyClone = Instantiate(enemy[randomEnemy],new Vector3(spawnPoints[randomSpawnPoint].position.x, spawnPoints[randomSpawnPoint].position.y, -1), Quaternion.identity);
        Destroy(enemyClone, lifeTime);
    }

}
