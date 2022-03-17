using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject mainCharacter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(mainCharacter.transform.position.x, mainCharacter.transform.position.y, -10);


    }

}
