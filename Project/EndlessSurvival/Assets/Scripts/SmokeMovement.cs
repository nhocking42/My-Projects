using UnityEngine;

public class SmokeMovement : MonoBehaviour
{
    public float speed;
    public bool vertical;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (vertical)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y+speed*Time.deltaTime ,0);
            if (transform.position.y > 112)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, 0);
            if (transform.position.x > 112)
            {
                Destroy(this.gameObject);
            }
        }


    }
}
