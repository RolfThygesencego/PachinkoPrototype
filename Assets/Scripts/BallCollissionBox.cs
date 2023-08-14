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
            
            
            


            

        }
    }
    // Update is called once per frame

}
