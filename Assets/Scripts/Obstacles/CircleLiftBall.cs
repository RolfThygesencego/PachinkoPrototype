using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CircleLiftBall : CircleObstacle 
{ 
    public CircleLiftBall()
    {
        locScale = 1f;
    }
    public override void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ball") && UpgradeSpent == false)
        {
            Debug.Log("triggered");
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            float xBurst;
            if (collision.transform.position.x < transform.position.x)
                xBurst = -300;
            else
                xBurst = 300;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xBurst, 500));
            
            UpgradeSpent =true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;

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
