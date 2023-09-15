using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    public bool FacingRight;
    public GameObject Hatch;
    public GameObject FireCube;
    private bool HatchOpen;
    public GameObject spawnPos;
    public bool ReadyToFire;
    public List<GameObject> BallList;
    public int BallIndex;
    public bool FinishedBalls = false;
    public float timer;
    public float maxTimer = 5;
    public bool ballTimer = true;
    public Barrel barrel;
    public float minPower;
    public float maxPower;

    public void OpenHatch()
    {
        Hatch.SetActive(false);
        HatchOpen = true;
    }

    public void CloseHatch()
    {
        Hatch.SetActive(true);
        HatchOpen = false;
    }
    private void Update()
    {
        FireBall();
    }

    public void SelectBall()
    {
        if (timer < 0 && !FinishedBalls && BallList.Count > 0)
        {


        }
        timer -= 0.1f;

    }
    public void AddBall()
    {
        GameObject ball = GameManager.Instantiate(PrefabHolder.Instance.ballNoRand, spawnPos.transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().simulated = true;
    }
    public void FireBall()
    {
 
            Vector2 shootdirection = barrel.target.transform.position - barrel.transform.position;
            shootdirection.Normalize();
            GameObject ball = GameManager.Instantiate(PrefabHolder.Instance.ballNoRand, barrel.spawnCube.transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().simulated = true;
            float power = Random.Range(minPower, maxPower);
            ball.GetComponent<Rigidbody2D>().velocity = shootdirection * power;
    }







}
