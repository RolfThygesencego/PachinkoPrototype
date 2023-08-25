using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public class PrefabHolder : MonoBehaviour
{
    public static PrefabHolder Instance { get; private set; }
    public GameObject obstacle;
    public GameObject tenBallObstacle;
    public GameObject standardBallObstacle;
    public GameObject ObstacleNoRand;
    public GameObject circleExtraBall;
    public GameObject circleAddToScore;
    public GameObject circleBounceHigh;
    public GameObject goal;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }
}


