using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CircleObstacleNoRand : MonoBehaviour
{
    bool directionSet = false;
    public float speed;
    int Left;
    public GameObject lastBall;
    public CircleObstacleNoRand()
    {
    }
    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (lastBall == null || lastBall != collision.gameObject)
            {
                Left = Random.Range(0, 2);
                lastBall = collision.gameObject;
            }
            if (!directionSet)
            {
                
            }
                
            collision.gameObject.GetComponent<BallNoRand>().Falling = false;

        }


    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - speed / 10);
            collision.gameObject.GetComponent<BallNoRand>().Falling = true;
        }

    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (Left > 0)
            {
                collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x - speed / 8, collision.gameObject.transform.position.y - speed / 20);
            }
            else
            {
                collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x + speed / 8, collision.gameObject.transform.position.y - speed / 20);
            }
        }

    }

}
