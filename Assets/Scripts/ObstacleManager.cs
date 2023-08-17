using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.XR;

public class ObstacleManager : MonoBehaviour
{
    private SpinningReel spinningReel;
    public Camera Camera;
    public List<GameObject> obstacles = new List<GameObject>();
    public List<GameObject> inactiveObstacles = new List<GameObject>();
    public List<GameObject> readyObstacles = new List<GameObject>();
    public List<GameObject> availableUpgrades = new List<GameObject>();
    public List<GameObject> tempUpgrades = new List<GameObject>();
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
    [SerializeField]
    private float maxObstacleTimer = 0.3f;
    private float currentObstacleTime = 0;
    [SerializeField]
    private float minObDistance = 1f;
    [SerializeField]
    private float maxObDistance = 2f;
    private int totalObstacles;

    public GameObject obstacle;
    public GameObject originStart;
    public GameObject circleExtraBall;
    public GameObject circleAddToScore;
    public GameObject circleBounceHigh;
    public int ExtraBallPowerups = 10;
    public int AddToScorePowerups = 5;
    public int BounceHighPowerups = 5;
    private GameObject prevObstacle;
    private float lastObDistance = 0;
   

    void Start()
    {
        AddAvailableUpgrades();
        spinningReel = GetComponentInParent<SpinningReel>();
        int totalObstacles = gameObject.transform.childCount;
        for (int i = 0; i < ExtraBallPowerups; i++)
        {
            GameObject ob = Instantiate(circleExtraBall);
            ob.transform.SetParent(transform, false);
            ob.SetActive(false);
            Instantiate(ob);
            obstacles.Add(ob);
            inactiveObstacles.Add(ob);
            totalObstacles -= 1;
            ob.GetComponent<CircleObstacle>().pObstacleManager = this;
        }
        for (int j = 0; j < AddToScorePowerups; j++)
        {
            GameObject ob = Instantiate(circleAddToScore);
            ob.transform.SetParent(transform, false);
            ob.SetActive(false);
            Instantiate(ob);
            obstacles.Add(ob);
            inactiveObstacles.Add(ob);
            totalObstacles -= 1;
            ob.GetComponent<CircleObstacle>().pObstacleManager = this;
        }
        for (int j = 0; j < BounceHighPowerups; j++)
        {
            GameObject ob = Instantiate(circleBounceHigh);
            ob.transform.SetParent(transform, false);
            ob.SetActive(false);
            Instantiate(ob);
            obstacles.Add(ob);
            inactiveObstacles.Add(ob);
            totalObstacles -= 1;
            ob.GetComponent<CircleObstacle>().pObstacleManager = this;
        }
        for (int i = 0; i < totalObstacles; i++)
        {
            var currentObstacle = transform.GetChild(i);
            obstacles.Add(currentObstacle.gameObject);
            currentObstacle.GetComponent<CircleObstacle>().pObstacleManager = this;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (spinningReel.controlsObstacles)
        {
            Spinning = spinningReel.spinning;
            if (!spinningReel.rightDirection)
                SpinningSpeed = spinningReel.SpinningSpeed;
            else if (spinningReel.rightDirection)
                SpinningSpeed = 0 - spinningReel.SpinningSpeed;
            MaxObstacles = spinningReel.MaxObstacles;
            obstacleTimer = spinningReel.ObstacleTimer;
            maxObstacleTimer = spinningReel.maxObstacleTimer;
            minObDistance = spinningReel.minObDistance;
            maxObDistance = spinningReel.maxObDistance;
            totalObstacles = spinningReel.TotalObstacles;
            if (obstacles.Count < totalObstacles)
            {
                GameObject ob = Instantiate(obstacle);
                ob.transform.SetParent(transform, false);
                ob.SetActive(false);
                Instantiate(ob);
                obstacles.Add(ob);
                inactiveObstacles.Add(ob);
                //bruh
            }

        }
        //ChangeColor();
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
                if (!spinningReel.rightDirection)
                    if (obstacle.transform.position.x < -7.9 && obstacle.activeSelf)
                    {
                        if (inactiveObstacles.Contains(obstacle))
                            break;

                        if (obstacles.Count > totalObstacles)
                        {
                            obstacles.Remove(obstacle);
                            Destroy(obstacle.gameObject);
                            return;
                        }
                        obstacle.SetActive(false);
                        inactiveObstacles.Add(obstacle);
                    }
                if (spinningReel.rightDirection)
                {
                    if (obstacle.transform.position.x > 7.9 && obstacle.activeSelf)
                    {
                        if (inactiveObstacles.Contains(obstacle))
                            break;

                        if (obstacles.Count > totalObstacles)
                        {
                            obstacles.Remove(obstacle);
                            Destroy(obstacle);
                            return;
                        }
                        obstacle.SetActive(false);
                        inactiveObstacles.Add(obstacle);
                    }
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

            inactiveObstacles.Remove(obs);
            if (!readyObstacles.Contains(obs))
                readyObstacles.Add(obs);
        }
    }
    void ReactivateObstacles()
    {
        if (Spinning)
            currentObstacleTime -= 0.1f;
        if (currentObstacleTime < 0 && getReadyObstacles() > 0)
        {
            if (readyObstacles[0] == null)
            {
                return;
            }
            var obs = readyObstacles[0];

            if (DetermineDistance(prevObstacle, lastObDistance) || prevObstacle == null)
            {
                prevObstacle = obs;
                Vector2 vect = new Vector2();
                if (!spinningReel.rightDirection)
                {
                    float randX = Random.Range(minObDistance, maxObDistance);
                    vect = new Vector2(originStart.transform.position.x + randX, Random.Range(0.6f, -0.6f));
                    lastObDistance = randX;
                }
                else
                {
                    float randX = Random.Range(minObDistance, maxObDistance);
                    vect = new Vector2(originStart.transform.position.x - randX, Random.Range(0.6f, -0.6f));
                    lastObDistance = randX;
                }
                obs.transform.localPosition = vect;
                obs.SetActive(true);
                readyObstacles.Remove(obs);
                float newTimer = Random.Range(obstacleTimer, maxObstacleTimer);
                currentObstacleTime = obstacleTimer;
            }
            else if (!inactiveObstacles.Contains(obs))
            {
                inactiveObstacles.Add(obs);
                readyObstacles.Remove(obs);
            }
                

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

    private bool DetermineDistance(GameObject obs, float lastX)
    {
        if (prevObstacle != null)
        {

                if (Mathf.Abs(obs.transform.position.x - originStart.transform.position.x) < lastX)
                    return false;
        }
        return true;
    }
    public void RefreshUpgrades()
    {
        foreach (GameObject ob in obstacles)
        {
            ob.GetComponent<Rigidbody2D>().simulated = true;
            CircleObstacle co = ob.GetComponent<CircleObstacle>();
            co.UpgradeSpent = false;
            ob.GetComponent<SpriteRenderer>().color = co.originalColor;
        }
    }

    public void ChangeObstacleToSpecial()
    {


        GameObject oldObst = obstacles.Find(ob => ob.GetComponent<CircleObstacle>() && ob.activeSelf);

      
        Vector2 obstpos = oldObst.gameObject.transform.position;
        obstacles.Remove(oldObst);
        Destroy(oldObst);

        GameObject newObs = Instantiate(availableUpgrades[Random.Range(0, availableUpgrades.Count)]);
        newObs.transform.position = obstpos;
        obstacles.Add(newObs);
        tempUpgrades.Add(newObs);
    }
    public void AddAvailableUpgrades()
    {
        availableUpgrades.Add(circleAddToScore);
        availableUpgrades.Add(circleAddToScore);
        availableUpgrades.Add(circleAddToScore);
        availableUpgrades.Add(circleAddToScore);
        availableUpgrades.Add(circleBounceHigh);
        availableUpgrades.Add(circleBounceHigh);
        availableUpgrades.Add(circleExtraBall);
    }
}
