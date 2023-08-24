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
    public GameObject tenBallObstacle;
    public GameObject standardBallObstacle;
    public GameObject originStart;
    public GameObject circleExtraBall;
    public GameObject circleAddToScore;
    public GameObject circleBounceHigh;
    public int ExtraBallPowerups = 10;
    public int AddToScorePowerups = 5;
    public int BounceHighPowerups = 5;
    private GameObject prevObstacle;
    private float lastObDistance = 0;
    public float obstacleScale = 1.5f;
    public float heightVariation = 0.01f;
   

    void Start()
    {
        AddAvailableUpgrades();
        spinningReel = GetComponentInParent<SpinningReel>();
        int totalObstacles = gameObject.transform.childCount;
        if (GameManager.Instance.gmode == GMode.STANDARD)
            obstacle = standardBallObstacle;
        if (GameManager.Instance.gmode == GMode.TEN_BALL)
            obstacle = tenBallObstacle;

        for (int i = 0; i < ExtraBallPowerups; i++)
        {
            GameObject ob = Instantiate(circleExtraBall);
            ob.transform.localScale = new Vector2(ob.GetComponent<CircleObstacle>().locScale, ob.GetComponent<CircleObstacle>().locScale);
            ob.transform.SetParent(transform, true);
            ob.SetActive(false);
            Instantiate(ob);
            obstacles.Add(ob);
            inactiveObstacles.Add(ob);
            totalObstacles -= 1;
            ob.GetComponent<CircleObstacle>().SetObstacleManager();
        }
        for (int j = 0; j < AddToScorePowerups; j++)
        {
            GameObject ob = Instantiate(circleAddToScore);
            ob.transform.localScale = new Vector2(ob.GetComponent<CircleObstacle>().locScale, ob.GetComponent<CircleObstacle>().locScale);
            ob.transform.SetParent(transform, true);
            ob.SetActive(false);
            Instantiate(ob);
            obstacles.Add(ob);
            inactiveObstacles.Add(ob);
            totalObstacles -= 1;
            ob.GetComponent<CircleObstacle>().SetObstacleManager();
        }
        for (int j = 0; j < BounceHighPowerups; j++)
        {
            GameObject ob = Instantiate(circleBounceHigh);
            ob.transform.localScale = new Vector2(ob.GetComponent<CircleObstacle>().locScale, ob.GetComponent<CircleObstacle>().locScale);
            ob.transform.SetParent(transform, true);
            ob.SetActive(false);
            Instantiate(ob);
            obstacles.Add(ob);
            inactiveObstacles.Add(ob);
            totalObstacles -= 1;
            ob.GetComponent<CircleObstacle>().SetObstacleManager();
        }
        for (int i = 0; i < totalObstacles; i++)
        {
            var currentObstacle = transform.GetChild(i);
            obstacles.Add(currentObstacle.gameObject);
            currentObstacle.GetComponent<CircleObstacle>().SetObstacleManager();
            currentObstacle.transform.localScale = new Vector2(currentObstacle.GetComponent<CircleObstacle>().locScale, currentObstacle.GetComponent<CircleObstacle>().locScale);
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
                ob.transform.SetParent(transform, true);
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
                    vect = new Vector2(originStart.transform.position.x + randX, Random.Range(-heightVariation, heightVariation - 0.01f));
                    lastObDistance = randX;
                }
                else
                {
                    float randX = Random.Range(minObDistance, maxObDistance);
                    vect = new Vector2(originStart.transform.position.x - randX, Random.Range(-heightVariation, heightVariation - 0.01f));
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
            if (Vector2.Distance(obs.transform.position, originStart.transform.position) < lastX) 
                return false;

                //if (Mathf.Abs(obs.transform.position.x - originStart.transform.position.x) < lastX)
                //    return false;
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
            ob.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void TurnInvisible()
    {
        foreach (GameObject ob in obstacles)
        {

            if (ob.GetComponent<CircleObstacle>().UpgradeSpent == true)
            {
                ob.GetComponent<Rigidbody2D>().simulated = false;
                ob.GetComponent<SpriteRenderer>().enabled = false;
            }

        }
    }

    public void ChangeObstacleToSpecial()
    {


        GameObject oldObst = obstacles.Find(ob => ob.GetComponent<CircleObstacle>() && ob.activeSelf);

      
        Vector2 obstpos = oldObst.gameObject.transform.position;
        obstacles.Remove(oldObst);
        ObstacleManager obMan = oldObst.GetComponent<CircleObstacle>().pObstacleManager;
        
        

        GameObject newObs = Instantiate(availableUpgrades[Random.Range(0, availableUpgrades.Count)]);
        
        newObs.transform.position = obstpos;
        newObs.GetComponent<CircleObstacle>().Temporary = true;
        newObs.transform.localScale = new Vector2(newObs.GetComponent<CircleObstacle>().locScale, newObs.GetComponent<CircleObstacle>().locScale);
        newObs.transform.transform.SetParent(obMan.gameObject.transform);
        obstacles.Add(newObs);
        tempUpgrades.Add(newObs);
        Destroy(oldObst);
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
    public void RemoveBalls(Ball ball)
    {
        GameManager.Instance.balls.Remove(ball);
        Destroy(ball);
    }

}
