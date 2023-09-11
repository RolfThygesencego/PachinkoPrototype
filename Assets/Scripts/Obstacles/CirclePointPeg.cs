using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePointPeg : CircleObstacle
{
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && UpgradeSpent == false)
        {
            Debug.Log("triggered");
            
            UpgradeSpent = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;

            GameManager.Instance.pegsHit += 1;
            if (!GameManager.Instance.PegAddToScore)
                return;
            GameManager.Instance.UpgradeAddToScore();
        }

    }
}
