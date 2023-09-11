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
    public float maxTimer = 2;
    public bool ballTimer = true;
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
        
    }

    public void SelectBall()
    {
        if (timer < 0 && !FinishedBalls && BallList.Count > 0)
        {
            MoveBall(BallList[BallIndex]);


        }
        timer -= 0.1f;

    }

    public void MoveBall(GameObject ball)
    {
        if (!ball.GetComponent<BallNoRand>().Loaded)
        {
            if (FacingRight)
            {
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector3(10f, 0, 0));
                if (Vector2.Distance(ball.transform.position, FireCube.transform.position) < .2f)
                {
                    FireBall(ball);
                }
            }
            else
            {
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector3(-10f, 0, 0));
                if (Vector2.Distance(ball.transform.position, FireCube.transform.position) < .2f)
                {
                    FireBall(ball);
                }
            }
        }


    }
    public void FireBall(GameObject ball)
    {
        int shootforce = Random.Range(1000, 1300);
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, shootforce));
        ball.GetComponent<BallNoRand>().Loaded = true;
        if (BallIndex + 1 < BallList.Count)
        {
            timer = maxTimer;
            BallIndex = BallIndex + 1;
        }
        else
            FinishedBalls = true;
    }







}
