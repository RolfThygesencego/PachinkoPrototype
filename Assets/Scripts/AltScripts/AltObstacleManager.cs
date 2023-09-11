using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class AltObstacleManager : MonoBehaviour
{
    public List<GameObject> obstacles = new List<GameObject>();
    public List<GameObject> inactiveObstacles = new List<GameObject>();
    public List<GameObject> bottomObs = new List<GameObject>();
    public List<GameObject> goals = new List<GameObject>();
    public List<GameObject> pointPegs = new List<GameObject>();
    public List<GameObject> readyObstacles = new List<GameObject>();
    public List<GameObject> availableUpgrades = new List<GameObject>();
    public List<GameObject> tempUpgrades = new List<GameObject>();
    public List<GameObject> rightObs = new List<GameObject>();
    public bool Generated = false;


    public float obSpeed;


    public int ExtraBallPowerups = 10;
    public int AddToScorePowerups = 5;
    public int BounceHighPowerups = 5;
    public float obstacleScale = 1.5f;
    public float heightVariation = 0.01f;
    public float LengthDistance = 2;
    public float oldLengthDistance;
    public int ObstacleRows = 5;
    public GameObject obstacleHolder;
    public GameObject goalHolder;
    public GameObject pointPegHolder;

    public bool morePegPoints = false;
    public int additionalPegsPerRow = 0;
    private int maxObsPerRow = 10;
    public int setMaxObsPerRow;
    private bool lastOffset = false;
    private float finalOffset;
    private bool alternateLeft = false;
    private bool addedToAdditionalPegs = false;
    public void SetupObstacles()
    {
        int amountOfObsPerRow = 1;
        int rowsCounted = 0;
        float offset = 0f;
        for (int i = 1; i < ObstacleRows + 1; i++)
        {

            int sideDistance = 1;

            if (amountOfObsPerRow < maxObsPerRow)
                offset = ((LengthDistance / 2) * (i + additionalPegsPerRow));
            if (amountOfObsPerRow == maxObsPerRow && !lastOffset)
            {
                offset = ((LengthDistance / 2) * (i + additionalPegsPerRow));
                lastOffset = true;
                finalOffset = offset;
            }
            if (lastOffset)
            {
                if (!alternateLeft)
                {
                    offset = finalOffset;
                    amountOfObsPerRow = maxObsPerRow;

                }
                else
                {
                    offset = finalOffset - LengthDistance / 2;
                    amountOfObsPerRow--;
                }

            }
            for (int j = 1; j < amountOfObsPerRow + additionalPegsPerRow; j += 1)
            {

                GameObject ObstacleLeft = Object.Instantiate(PrefabHolder.Instance.ObstacleNoRand, new Vector2(0, 0), Quaternion.identity);
                ObstacleLeft.transform.position = new Vector2(sideDistance * LengthDistance - offset, -i * LengthDistance);


                ObstacleLeft.transform.SetParent(obstacleHolder.transform, false);

                //ObstacleLeft.transform.position = new Vector2(ObstacleLeft.transform.position.x + LengthDistance / 2, ObstacleLeft.transform.position.y);


                obstacles.Add(ObstacleLeft);


                sideDistance += 1;

                if (j + 1 == amountOfObsPerRow + additionalPegsPerRow)
                {
                    rightObs.Add(ObstacleLeft);
                }
                if (i == ObstacleRows)
                {
                    bottomObs.Add(ObstacleLeft);
                }

            }
            if (amountOfObsPerRow < maxObsPerRow)
                amountOfObsPerRow += 1;
            else
                rowsCounted++;
            if (lastOffset)
                alternateLeft = !alternateLeft;



        }
        AddPegPoints();
        SetupGoals();

    }
    public void Update()
    {
        if (!Generated)
        {
            maxObsPerRow = setMaxObsPerRow - additionalPegsPerRow;
            if(!addedToAdditionalPegs)
            additionalPegsPerRow += 1;
            ClearObs();
            SetupObstacles();
            ScaleObstacles();
            ChangeObstacleSpeed();
            lastOffset = false;
            alternateLeft = false;
            addedToAdditionalPegs = true;
            Generated = true;
            
        }
    }
    public void ScaleObstacles()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.transform.localScale = new Vector2(obstacleScale, obstacleScale);
        }
        foreach (var pointPeg in pointPegs)
        {
            pointPeg.transform.localScale = new Vector2(obstacleScale / 2, obstacleScale / 2);
        }

    }
    public void Start()
    {
        if (Generated)
        {
            foreach(GameObject goal in goals)
            {
                GameManager.Instance.Goals.Add(goal.GetComponent<Goal>());
            }
        }
    }
    public void Awake()
    {
        
    }

    public void AddPegPoints()
    {
        int amountOfPegsPerRow = 0;


        for (int i = 0; i < obstacles.Count; i++)
        {
            if (!rightObs.Contains(obstacles[i]))
            {
                GameObject pointPeg = Object.Instantiate(PrefabHolder.Instance.pegPoint, new Vector2(0, 0), Quaternion.identity);
                Vector2 pegPos = new Vector2(obstacles[i].transform.position.x + LengthDistance / 2, obstacles[i].transform.position.y);
                pointPeg.transform.position = pegPos;
                pointPeg.gameObject.transform.SetParent(pointPegHolder.transform);
                pointPegs.Add(pointPeg);
            }
                

            
            if (morePegPoints)
            {
                GameObject pointPeg2 = Object.Instantiate(PrefabHolder.Instance.pegPoint, new Vector2(0, 0), Quaternion.identity);
                Vector2 pegPos2 = new Vector2(obstacles[i].transform.position.x + LengthDistance / 2, obstacles[i].transform.position.y - (LengthDistance / 2));
                pointPeg2.transform.position = pegPos2;
                pointPeg2.gameObject.transform.SetParent(pointPegHolder.transform);
                pointPegs.Add(pointPeg2);
                GameObject pointPeg3 = Object.Instantiate(PrefabHolder.Instance.pegPoint, new Vector2(0, 0), Quaternion.identity);
                Vector2 pegPos3 = new Vector2(obstacles[i].transform.position.x, obstacles[i].transform.position.y - (LengthDistance / 2));
                pointPeg3.transform.position = pegPos3;
                pointPeg3.gameObject.transform.SetParent(pointPegHolder.transform);
                pointPegs.Add(pointPeg3);
                if (i + 1 == amountOfPegsPerRow - 1)
                {
                    GameObject pointPeg4 = Object.Instantiate(PrefabHolder.Instance.pegPoint, new Vector2(0, 0), Quaternion.identity);
                    Vector2 pegPos4 = new Vector2(obstacles[i].transform.position.x + LengthDistance, obstacles[i].transform.position.y - (LengthDistance / 2));
                    pointPeg4.transform.position = pegPos4;
                    pointPeg4.gameObject.transform.SetParent(pointPegHolder.transform);
                    pointPegs.Add(pointPeg4);
                }
                if (amountOfPegsPerRow == 2)
                {
                    GameObject pointPeg5 = Object.Instantiate(PrefabHolder.Instance.pegPoint, new Vector2(0, 0), Quaternion.identity);
                    Vector2 pegPos5 = new Vector2(obstacles[0].transform.position.x, obstacles[0].transform.position.y - (LengthDistance / 2));
                    pointPeg5.transform.position = pegPos5;
                    pointPeg5.gameObject.transform.SetParent(pointPegHolder.transform);
                    pointPegs.Add(pointPeg5);
                }

            }

        }

    }
    public void ClearObs()
    {



        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
        obstacles.Clear();
        bottomObs.Clear();
        rightObs.Clear();
        foreach (GameObject goal in goals)
        {
            Destroy(goal);
        }
        goals.Clear();
        GameManager.Instance.Goals.Clear();
        foreach (GameObject peg in pointPegs)
        {
            Destroy(peg);
        }
        pointPegs.Clear();
       
    }
    public void SetupGoals()
    {
        for (int i = 0; i < bottomObs.Count; i++)
        {
            GameObject Goal = Object.Instantiate(PrefabHolder.Instance.goal, new Vector2(0, 0), Quaternion.identity);
            Goal.gameObject.transform.position = new Vector2(bottomObs[i].transform.position.x - (LengthDistance / 2), bottomObs[i].transform.position.y - 2);
            Goal.gameObject.transform.SetParent(goalHolder.transform);
            goals.Add(Goal);
            if (i == bottomObs.Count - 1)
            {
                GameObject lastGoal = Object.Instantiate(PrefabHolder.Instance.goal, new Vector2(0, 0), Quaternion.identity);
                lastGoal.gameObject.transform.position = new Vector2(bottomObs[i].transform.position.x + (LengthDistance / 2), bottomObs[i].transform.position.y - 2);
                lastGoal.gameObject.transform.SetParent(goalHolder.transform);
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

