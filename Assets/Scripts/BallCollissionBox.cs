using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BallCollissionBox : MonoBehaviour
{
    
    void Start()
    {
        
    }
    private void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (collision.GetComponent<Ball>().ballScoreAdded)
            {
                collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            else if (collision.GetComponent<Rigidbody2D>().velocity.x == 0)
            {
                int leftRight = Random.Range(0, 1);
                if (leftRight == 1)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0));
                }
                if (leftRight == 0)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0));
                }
            }
            
        }
    }
    // Update is called once per frame

}
