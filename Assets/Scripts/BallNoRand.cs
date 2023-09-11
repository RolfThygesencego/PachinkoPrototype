using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNoRand : MonoBehaviour
{
    public bool ballScoreAdded = false;
    public bool toBeRemoved = false;
    public bool Falling = true;
    public float speed = 0.08f;
    public bool Loaded = false;
    public void Awake()
    {
        
    }
    public void Update()
    {
        //FallDown();
    }
    void FallDown()
    {
        if (Falling)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y -0.04f);
        }
    }

}
