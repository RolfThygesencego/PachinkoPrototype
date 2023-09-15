using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    float speed = 0.05f;
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
        if (transform.position.y > 8)
        {
            speed = -0.1f;
        }
        if (transform.position.y < 5.5)
        {
            speed = 0.1f;
        }
    }
}
