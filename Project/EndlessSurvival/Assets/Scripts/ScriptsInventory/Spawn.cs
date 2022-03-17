using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    private Transform playerPos;
    public GameObject item;
    public int inSlot;

    private void Start()
    {
        inSlot = 0;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    [System.Obsolete]
    public void SpawnItem() {
        Vector2 spawnArea = Vector2.zero;
        spawnArea = new Vector2(playerPos.position.x + Random.RandomRange(-1, 1), playerPos.position.y + 1);
        GameObject cloneItem = Instantiate(item, spawnArea, Quaternion.identity);

    }

    public void SpawnItemAndPickAgain()
    {
        GameObject cloneItem = Instantiate(item, new Vector2(playerPos.position.x, playerPos.position.y), Quaternion.identity);
    }
}
