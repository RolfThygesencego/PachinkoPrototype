using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ShootingBalls : State
{
    float timer;
    float MaxTimer = 2;
    int ballsSpawned;
    bool leftTurn = true;
    public override void End()
    {
        GameManager.Instance.leftBallHolder.BallList.Clear();
        GameManager.Instance.rightBallHolder.BallList.Clear();
    }

    public override void Execute()
    {
        GameManager.Instance.leftBallHolder.SelectBall();
        GameManager.Instance.rightBallHolder.SelectBall();
        if (GameManager.Instance.leftBallHolder.FinishedBalls && GameManager.Instance.rightBallHolder.FinishedBalls)
        {
            GameManager.Instance.ChangeState(GameManager.Instance.ReadyForSpin);
        }
    }

    public override void FixedExecute()
    {

    }

    public override void Initialize()
    {
        
    }
}

