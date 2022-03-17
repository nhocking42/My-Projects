using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSpawner : MonoBehaviour
{
    public GameObject smoke;
    public float defaultTimer;
    float timer;
    void Start()
    {
        timer = default;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Instantiate(smoke, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            timer = defaultTimer;
        }
    }
}
