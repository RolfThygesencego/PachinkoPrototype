using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CircleExtraBall : CircleObstacle 
{ 
    public CircleExtraBall()
    {
        locScale = 1f;
    }
    public override void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ball") && UpgradeSpent == false)
        {
            Debug.Log("triggered");
            GameManager.Instance.Ballsdropping.ballsToBeAdded++;

            UpgradeSpent = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }
        
    }
    //bruh
    public override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
    }
}
