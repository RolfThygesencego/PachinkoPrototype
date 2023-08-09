using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class ObstacleManager : MonoBehaviour
{
    private SpinningReel spinningReel;
    public Camera Camera;
    [SerializeField]
    private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField]
    private List<GameObject> inactiveObstacles = new List<GameObject>();
    [SerializeField]
    private List<GameObject> readyObstacles = new List<GameObject>();
    [SerializeField]
    Color obstacleColor = Color.black;
    [SerializeField]
    private bool Spinning = false;
    [SerializeField]
    private float SpinningSpeed = 0.1f;
    [SerializeField]
    private int MaxObstacles = 8;
    [SerializeField]
    private float obstacleTimer = 0.2f;
    private float currentObstacleTime = 0;
    [SerializeField]
    private float minObDistance = 1f;


    void Start()
    {
        spinningReel = GetComponentInParent<SpinningReel>();
        int totalObstacles = gameObject.transform.childCount;
        for (int i = 0; i < totalObstacles; i++)
        {
            var currentObstacle = transform.GetChild(i);
            obstacles.Add(currentObstacle.gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (spinningReel.controlsObstacles)
        {
            Spinning = spinningReel.spinning;
            SpinningSpeed = spinningReel.SpinningSpeed;
            MaxObstacles = spinningReel.MaxObstacles;
            obstacleTimer = spinningReel.ObstacleTimer;
            minObDistance = spinningReel.minObDistance;
        }
        ChangeColor();
        Spin();
        ReorderInactiveObstacles();
        ReactivateObstacles();
    }

    void ChangeColor()
    {
        foreach (GameObject obstacle in obstacles)
        {
            SpriteRenderer sr = obstacle.GetComponent<SpriteRenderer>();
            sr.color = obstacleColor;
        }
    }

    void Spin()
    {
        if (Spinning)
        {
            foreach (GameObject obstacle in obstacles)
            {
                if (obstacle.activeSelf)
                    obstacle.transform.position = new Vector2(obstacle.transform.position.x - SpinningSpeed / 10, obstacle.transform.position.y);

                if (obstacle.transform.position.x < -10 && obstacle.activeSelf)
                {
                    if (inactiveObstacles.Contains(obstacle))
                        break;

                    obstacle.SetActive(false);
                    inactiveObstacles.Add(obstacle);
                }
            }
        }
    }
    void ReorderInactiveObstacles()
    {
        if (getActiveObstacles() < MaxObstacles && getInactiveObstacles() > 0)
        {
            int obindex = Random.Range(0, getInactiveObstacles());
            GameObject obs = inactiveObstacles[obindex];
            obs.transform.position = new Vector2(Random.Range(12.34f + (float)getReadyObstacles() / 3, 14.34f + (float)getReadyObstacles() / 3),
                                                 Random.Range(-1.2f, 1.2f));

            inactiveObstacles.Remove(obs);

            if (!readyObstacles.Contains(obs))
                readyObstacles.Add(obs);

        }
    }
    void ReactivateObstacles()
    {
        currentObstacleTime -= 0.01f;
        if (currentObstacleTime < 0 && getReadyObstacles() > 0)
        {
            if (readyObstacles[0] == null)
            {
                return;
            }
            var obs = readyObstacles[0];
            

            if (DetermineDistance(obs))
            {
                obs.SetActive(true);
                readyObstacles.Remove(obs);
                currentObstacleTime = obstacleTimer;
            }
            else if (!inactiveObstacles.Contains(obs))
                inactiveObstacles.Add(obs);
                
        }
    }
    private int getInactiveObstacles()
    {
        int currentObstacles = 0;
        foreach (var item in inactiveObstacles)
        {
            currentObstacles += 1;
        }
        return currentObstacles;
    }
    private int getActiveObstacles()
    {
        int currentObstacles = 0;
        foreach (var item in obstacles)
        {
            if (item.activeSelf)
                currentObstacles++;
        }
        return currentObstacles;
    }
    private int getReadyObstacles()
    {
        int currentObstacles = 0;
        foreach (var item in readyObstacles)
        {
            currentObstacles += 1;
        }
        return currentObstacles;
    }
    private int getNextObstacle()
    {
        int i = 0;
        while (true)
        {
            if (readyObstacles[i] == null)
                i++;
            else
                return i;
        }
    }

    private bool DetermineDistance(GameObject obs)
    {
        if (getReadyObstacles() >= 1)
            if (readyObstacles[getReadyObstacles() - 1] != null)
            {
                if (Vector2.Distance(obs.transform.position, readyObstacles[getReadyObstacles() - 1].transform.position) < minObDistance)
                    return false;
            }
        if (getReadyObstacles() >= 2)
            if (readyObstacles[getReadyObstacles() - 2] != null)
            {
                if (Vector2.Distance(obs.transform.position, readyObstacles[getReadyObstacles() - 2].transform.position) < minObDistance)
                    return false;
            }
        return true;
    }
}
