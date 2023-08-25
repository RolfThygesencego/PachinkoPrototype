using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CircleObstacleNoRand : MonoBehaviour
{
    bool directionSet = false;
    int Left;
    public CircleObstacleNoRand()
    {
    }
    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (!directionSet)
                Left = Random.Range(0, 2);

            directionSet = true;
            collision.gameObject.GetComponent<BallNoRand>().Falling = false;

        }


    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 0.01f);
            collision.gameObject.GetComponent<BallNoRand>().Falling = true;
        }

    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (Left > 0)
            {
                collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x - 0.015f, collision.gameObject.transform.position.y - 0.005f);
            }
            else
            {
                collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x + 0.015f, collision.gameObject.transform.position.y - 0.005f);
            }
        }

    }

}
