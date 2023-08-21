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

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
            if (collision.GetComponent<Rigidbody2D>().velocity.x <= 1f)
            {
                int leftRight = Random.Range(0, 1);
                if (leftRight == 1)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 0));
                }
                if (leftRight == 0)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 0));
                }
            }
    }
}
