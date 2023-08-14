using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CircleExtraBall : CircleObstacle 
{ 
    // Start is called before the first frame update
    public override void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ball") && UpgradeSpent == false)
        {
            Debug.Log("triggered");
            GameManager.Instance.Ballsdropping.ballsToBeAdded += 1;
            
            UpgradeSpent =true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
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

    public override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
    }
}
