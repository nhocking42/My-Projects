using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireManager : MonoBehaviour
{
    public BoxCollider2D bc;
    Vector3 defaultPosition;
    public float timing;
    private void Start()
    {
        defaultPosition = bc.transform.position;
        timing = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timing <= 0) {
            if (bc.transform.position == defaultPosition)
            {
                bc.transform.position = new Vector3(1000,1000,0);
            }
            else
            {
                bc.transform.position = defaultPosition;
            }
        }     
        else
        {
            timing -= Time.deltaTime;
        }
    }
}
