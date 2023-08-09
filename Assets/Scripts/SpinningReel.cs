using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpinningReel : MonoBehaviour
{
    public bool spinning = false;
    public float SpinningSpeed = 3;
    public int MaxObstacles = 12;
    public float ObstacleTimer = 0.2f;
    public float minObDistance = 1f;
    public bool controlsObstacles = true;

    void StartSpinning()
    {
        spinning = true;
    }
}
