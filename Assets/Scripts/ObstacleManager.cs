using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class ObstacleManager : MonoBehaviour
{
    public Camera Camera;
    [SerializeField]
    private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField]
    private List<GameObject> inactiveObstacles = new List<GameObject>();
    [SerializeField]
    Color obstacleColor = Color.black;
    [SerializeField]
    private bool Spinning = false;
    [SerializeField]
    private float SpinningSpeed = 0.1f;
    [SerializeField]
    private int MaxObstacles = 8;

    
    void Start()
    {
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
        ChangeColor();
        Spin();
        StartCoroutine(ReactivateObstacles());
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
                obstacle.transform.position = new Vector2(obstacle.transform.position.x - SpinningSpeed, obstacle.transform.position.y);

                if (obstacle.transform.position.x < -10 && obstacle.activeSelf)
                {
                    inactiveObstacles.Add(obstacle);
                    obstacle.SetActive(false);
                }
            }
        }
    }
    IEnumerator ReactivateObstacles()
    {
        if (getActiveObstacles() < MaxObstacles)
        {
            int inactiveObstaclesCount = inactiveObstacles.Count;
            GameObject obs = inactiveObstacles[Random.Range(0, inactiveObstaclesCount)];
            obs.transform.position = new Vector2(Random.Range(12.34f, 15.34f),
                      Random.Range(obs.transform.position.y -1, obs.transform.position.y + 1));
            obs.SetActive(true);
            inactiveObstacles.Remove(obs);
            yield return new WaitForSeconds(0.2f);
        }
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
}
