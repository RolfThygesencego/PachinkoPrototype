using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CircleObstacle : MonoBehaviour
{
    public bool UpgradeSpent = false;
    public float BallContact = 3f;
    public Color originalColor;
    public ObstacleManager pObstacleManager;

    public void Awake()
    {
        originalColor = GetComponent<SpriteRenderer>().color;

    }
    public virtual void OnCollisionStay2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BallContact -= 0.1f;
            if (BallContact < 0)
            {
                ObstacleManager PobstacleManager = transform.GetComponentInParent<ObstacleManager>();
                PobstacleManager.obstacles.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
    public virtual void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {

        
    }
    public virtual void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BallContact = 3f;
        }

    }
}
