using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Spinning : State
{
    UnityEvent ballfinish = new UnityEvent();
    public int finishedBalls = 0;
    public int finishedReels = 0;
    public override void End()
    {
        
    }

    public override void Execute()
    {
        
    }

    public override void Initialize()
    {
        StartSpinning();
    }

    public void StartSpinning()
    {
        {
            foreach (SpinningReel reel in GameManager.Instance.Reels)
            {
                reel.spinning = true;
                reel.GetComponentInChildren<ObstacleManager>().RefreshUpgrades();
            }
            GameManager.Instance.balls.Clear();
            for (int i = 0; i < 1; i++)
            {
                GameManager.Instance.Ballsdropping.ballsToBeAdded++;
            }
        }
    }
    public void reelFinishedSpinning()
    {
        finishedReels++;
        if (finishedReels >= GameManager.Instance.Reels.Count)
        {
            finishedReels = 0;
            GameManager.Instance.ChangeState(GameManager.Instance.Ballsdropping);

        }
    }
}

