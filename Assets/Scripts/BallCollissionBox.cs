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
    // Update is called once per frame

}
