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
    public float maxObstacleTimer = 0.3f;
    public float minObDistance = 1f;
    public float maxObDistance = 2;
    public bool controlsObstacles = true;
    public bool rightDirection = false;
    public float maxSpinTime = 30f;
    public float currentSpinTime = 0;
    public bool spinFinished = false;

    public void StartSpinning()
    {
        if (!spinning)
        {
            spinFinished = false;
            spinning = true;
        }
            
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
                GameManager.Instance.reelFinished.Invoke();
                  
            }
        }
    }

    private void Update()
    {
       // MaxMoreThanMinDistance();
        SpinningTime();
    }

    void MaxMoreThanMinDistance()
    {
        if (maxObDistance - minObDistance < 0.5f)
        {
            maxObDistance = minObDistance;
            maxObDistance += 0.5f;
        }
    }
}
