using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PreBallDrop : State
{
    float timer;
    float MaxTimer = 2;
    int ballsSpawned;
    bool leftTurn = true;
    public override void End()
    {
        if (GameManager.Instance.ballWagerAmount < 2)
        {
            if (!leftTurn)
                GameManager.Instance.rightBallHolder.FinishedBalls = true;
            else
                GameManager.Instance.leftBallHolder.FinishedBalls = true;
        }

    }

    public override void Execute()
    {
        if (ballsSpawned < GameManager.Instance.ballWagerAmount)
        {
            timer -= 0.1f;
            if (timer <= 0)
            {
                ballsSpawned++;
                timer = MaxTimer;
                if (leftTurn)
                {
                    GameManager.Instance.leftBallHolder.BallList.Add(GameObject.Instantiate(PrefabHolder.Instance.ballNoRand,
                                        GameManager.Instance.leftBallHolder.spawnPos.transform.position, Quaternion.identity));
                }
                if (!leftTurn)
                {
                    GameManager.Instance.rightBallHolder.BallList.Add(GameObject.Instantiate(PrefabHolder.Instance.ballNoRand,
                                        GameManager.Instance.rightBallHolder.spawnPos.transform.position, Quaternion.identity));

                }
                leftTurn = !leftTurn;
                timer = MaxTimer;
            }
        }
        else
            GameManager.Instance.ChangeState(GameManager.Instance.ShootingBalls);
    }

    public override void FixedExecute()
    {

    }

    public override void Initialize()
    {
        timer = 0;
        ballsSpawned = 0;
    }
}

