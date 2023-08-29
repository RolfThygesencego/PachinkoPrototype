using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[SerializeField]
public class AltObstacleManager : MonoBehaviour
{
    public List<GameObject> obstacles = new List<GameObject>();
    public List<GameObject> inactiveObstacles = new List<GameObject>();
    public List<GameObject> bottomObs = new List<GameObject>();
    public List<GameObject> goals = new List<GameObject>();
    public List<GameObject> readyObstacles = new List<GameObject>();
    public List<GameObject> availableUpgrades = new List<GameObject>();
    public List<GameObject> tempUpgrades = new List<GameObject>();
    

    private bool Spinning = false;

    private float SpinningSpeed = 0.1f;
    public float obSpeed;
    [SerializeField]
    private float minObDistance = 1f;
    [SerializeField]
    private float maxObDistance = 2f;
    private int totalObstacles;

    public int ExtraBallPowerups = 10;
    public int AddToScorePowerups = 5;
    public int BounceHighPowerups = 5;
    public float obstacleScale = 1.5f;
    public float heightVariation = 0.01f;
    public float LengthDistance = 2;
    public float oldLengthDistance;
    public int ObstacleRows = 5;
    public GameObject obstacleHolder;
    public GameObject goal;


    public void SetupObstacles()
    {
        int amountOfObsPerRow = 1;
        for (int i = 1; i < ObstacleRows + 1; i++)
        {

            int sideDistance = 1;
            float offset = ((LengthDistance / 2) * i);

            for (int j = 1; j < amountOfObsPerRow; j += 1)
            {

                GameObject ObstacleLeft = Object.Instantiate(PrefabHolder.Instance.ObstacleNoRand, new Vector2(0, 0), Quaternion.identity);
                ObstacleLeft.transform.position = new Vector2(sideDistance * LengthDistance - offset, -i * LengthDistance);
                
                
                ObstacleLeft.transform.SetParent(obstacleHolder.transform, false);

                //ObstacleLeft.transform.position = new Vector2(ObstacleLeft.transform.position.x + LengthDistance / 2, ObstacleLeft.transform.position.y);
                if (i % 2 == 0)
                {
                   
                   
                }
                obstacles.Add(ObstacleLeft);
 

                sideDistance += 1;
                if (ObstacleRows == amountOfObsPerRow)
                {
                    bottomObs.Add(ObstacleLeft);
                }

            }

            amountOfObsPerRow += 1;
        }

        SetupGoals();
    }
    public void Update()
    {
        ScaleObstacles();
        ChangeObPositions();
        ChangeObstacleSpeed();
    }
    public void ScaleObstacles()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.transform.localScale = new Vector2(obstacleScale, obstacleScale);
        }

    }
    public void ChangeObPositions()
    {
        if (LengthDistance == oldLengthDistance) return;
        else
        {
            foreach (GameObject obstacle in obstacles)
            {
                Destroy(obstacle);
            }
            obstacles.Clear();
            foreach (GameObject goal in goals)
            {
                Destroy(goal);
            }
            obstacles.Clear();
            SetupObstacles();
            oldLengthDistance = LengthDistance;
        }
    }
    public void SetupGoals()
    {
        for (int i = 0; i < bottomObs.Count; i++)
        {
            GameObject Goal = Object.Instantiate(PrefabHolder.Instance.goal, new Vector2(0, 0), Quaternion.identity);
            Goal.gameObject.transform.position = new Vector2(bottomObs[i].transform.position.x - (LengthDistance/2), bottomObs[i].transform.position.y - 4);
            Goal.gameObject.transform.SetParent(goal.transform);
            goals.Add(Goal);
            if (i == bottomObs.Count - 1)
            {
                GameObject lastGoal = Object.Instantiate(PrefabHolder.Instance.goal, new Vector2(0, 0), Quaternion.identity);
                lastGoal.gameObject.transform.position = new Vector2(bottomObs[i].transform.position.x + (LengthDistance / 2), bottomObs[i].transform.position.y - 4);
                lastGoal.gameObject.transform.SetParent(goal.transform);
                goals.Add(lastGoal);
            }

        }
       
    }
    public void ChangeObstacleSpeed()
    {
        foreach (GameObject go in obstacles)
        {
            go.GetComponent<CircleObstacleNoRand>().speed = obSpeed;
        }
    }

}

