using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpinningReel : MonoBehaviour
{
    public bool spinning = false;
    public float SpinningSpeed = 3;
    public int TotalObstacles = 15;
    public int MaxObstacles = 12;
    public float ObstacleTimer = 0.2f;
    public float minObDistance = 1f;
    public float maxObDistance = 2;
    public bool controlsObstacles = true;
    public bool rightDirection = false;
    public GameObject ball;
    public float maxSpinTime = 30f;
    public float currentSpinTime = 0;
    public bool ballSpawner = false;
    public List<GameObject> balls = new List<GameObject>();
    public void StartSpinning()
    {
        if (ballSpawner)
        {
            foreach (var ball in balls)
            {
                Destroy(ball);
            }
            balls.Clear();
            for (int i = 0; i < 3; i++)
            {
                GameObject go = Instantiate(ball);
                balls.Add(go);
                go.transform.position = new Vector2(-3 + (i * 3), 8.37f);
            }
        }   
        if (!spinning)
            spinning = true;
    }

    public void SpinningTime()
    {
        if (spinning)
        {
            
            currentSpinTime += 0.1f;
            if (currentSpinTime > maxSpinTime)
            {
                spinning = false;
                currentSpinTime = 0;
                if (ballSpawner)
                {
                    foreach (GameObject obj in balls)
                    {
                        obj.GetComponent<Rigidbody2D>().simulated = true;
                    }
                }    
            }
        }
    }

    private void Update()
    {
        if (maxObDistance - minObDistance < 0.5f)
        {
            maxObDistance = minObDistance;
            maxObDistance += 0.5f;
        }
        SpinningTime();
    }
}
