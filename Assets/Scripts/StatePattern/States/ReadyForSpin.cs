using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ReadyForSpin : State
{
    public override void End()
    {
    }

    public override void Execute()
    {
    
    }

    public override void Initialize()
    {
        GameManager.Instance.Spinning.ballWager = GameManager.Instance.ballsStandardAmount;
        GameManager.Instance.Ballsdropping.ballsToBeAdded = 0;
        RemoveTempUpgrades();
        GameManager.Instance.pegsHit = 0;
        GameManager.Instance.Streaks.Clear();
        GameManager.Instance.maxScoreStreak = 0;
<<<<<<< Updated upstream
=======
        GameManager.Instance.leftBallHolder.FinishedBalls = false;

        GameManager.Instance.leftBallHolder.BallIndex = 0;

>>>>>>> Stashed changes
    }
    void RemoveTempUpgrades()
    {
        foreach (SpinningReel reel in GameManager.Instance.Reels)
        {
            ObstacleManager obman = reel.GetComponentInChildren<ObstacleManager>();
            int obManIndex = obman.tempUpgrades.Count;
            for (int i = 0; i < obManIndex; i++)
            {
                GameObject obs = obman.obstacles.Find(t => t.GetComponent<CircleObstacle>().Temporary == true);
                obman.tempUpgrades.Remove(obs);
                obman.obstacles.Remove(obs);
                if (obman.readyObstacles.Contains(obs))
                    obman.readyObstacles.Remove(obs);
                if (obman.inactiveObstacles.Contains(obs))
                    obman.inactiveObstacles.Remove(obs);

                GameObject.Destroy(obs);
                

            }
        }
    }
    public override void FixedExecute()
    {

    }

}

