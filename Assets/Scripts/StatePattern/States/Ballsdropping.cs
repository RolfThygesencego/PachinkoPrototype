using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;



public class Ballsdropping : State
{
    public int ballsFinished = 0;
    public int ballsToBeAdded = 0;
    public float ballTimer = 1f;
    public float ballTimerMax = 5f;
    public List<Ball> readyBalls = new List<Ball>();
    public int[] goalScores = new int[9];
    public bool ReadyForNextBall = false;
    public float blinkTimer = 0.2f;
    public float secondBlinkTimer = 0.2f;
    public float blinkTimerMax = 0.2f;
    public bool setSecondblinkTimerEnabled = false;

    public override void End()
    {
        ballsToBeAdded = 0;

        saveData();

    }

    public override void Execute()
    {
        

        if (ballsFinished >= GameManager.Instance.balls.Count && ballsFinished > 0)
        {
            GameManager.Instance.ChangeState(GameManager.Instance.ReadyForSpin);
        }

    }

    public override void Initialize()
    {
        ReadyForNextBall = true;
        ballsFinished = 0;
        ballTimer = 0.5f;
       // DropBalls();
    }


    public void AddBallsStandard()
    {
        if (ballsToBeAdded > 0)
        {

            Ball go = GameManager.Instantiate(GameManager.Instance.ball);
            GameManager.Instance.balls.Add(go);
            readyBalls.Add(go);
            go.transform.position = new Vector2(Random.Range(-3f, 3f), 8.37f);
            ballsToBeAdded -= 1;
        }
        if (readyBalls.Count > 0)
            ballTimer -= 0.1f;
        if (ballTimer < 0 && readyBalls.Count > 0)
        {
            readyBalls[0].GetComponent<Rigidbody2D>().simulated = true;
            readyBalls[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, 200), 0));

            ballTimer = ballTimerMax;
            readyBalls[0].GetComponent<SpriteRenderer>().color = Color.white;
            readyBalls.RemoveAt(0);


        }

    }
    public void AddBalls10_Ball()
    {
        if (ballsToBeAdded > 0)
        {

            Ball go = GameManager.Instantiate(GameManager.Instance.ball);
            GameManager.Instance.balls.Add(go);
            readyBalls.Add(go);
            go.transform.position = new Vector2(Random.Range(-4.3f, 4.3f), 7.8f);
            ballsToBeAdded -= 1;
        }
        if (ReadyForNextBall)
        {
            foreach (SpinningReel reel in GameManager.Instance.Reels)
            {
                reel.obstacleManager.TurnInvisible();
            }
            blinkTimer -= 0.1f;
            
            if (blinkTimer < 0)
            {
                if(!setSecondblinkTimerEnabled)
                secondBlinkTimer = blinkTimerMax;
                setSecondblinkTimerEnabled = true;
                secondBlinkTimer -= 0.1f;
                readyBalls[0].GetComponent<SpriteRenderer>().color = Color.white;


            }
            if (secondBlinkTimer < 0)
            {
                
                readyBalls[0].GetComponent<SpriteRenderer>().color = Color.gray;
                blinkTimer = blinkTimerMax;
                setSecondblinkTimerEnabled = false;
                
            }
            readyBalls[0].GetComponent<SpriteRenderer>().color = Color.white;
            ballTimer -= 0.1f;
  
        }
        
        if (readyBalls.Count > 0 && ballTimer < 0)
        {
            readyBalls[0].GetComponent<Rigidbody2D>().simulated = true;
            readyBalls[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-320, 320), Random.Range(200, 300)));
            ballTimer = ballTimerMax;
            readyBalls[0].GetComponent<SpriteRenderer>().color = Color.white;
            readyBalls.RemoveAt(0);
            ReadyForNextBall = false;
        }
    } 

    void saveData()
    {
        int prevSpins = GameManager.Instance.CSVWriter.LoadCVSTimesSPun();
        int prevBalls = GameManager.Instance.CSVWriter.LoadCVSBallsPerSpin();
        if (prevSpins < 0)
        {
            prevSpins = 0;
        }
        int[] goalIndex = GameManager.Instance.CSVWriter.loadCVSGoalsScored();
        for (int i = 0; i < 9; i++)
        {
            goalScores[i] = (goalIndex[i] + GameManager.Instance.Goals[i].timesScored);
        }

        GameManager.Instance.CSVWriter.WriteCSVSaveGoals(goalScores[0], goalScores[1], goalScores[2], goalScores[3], goalScores[4], goalScores[5], goalScores[6], goalScores[7], goalScores[8], prevSpins + 1, prevBalls + ballsFinished);

        if (GameManager.Instance.gmode == GMode.TEN_BALL)
        {
            int streakTotal = 0;
            foreach (int i in GameManager.Instance.Streaks)
            {
                streakTotal += i;
            }
            float averageStreak = 0;
            if (GameManager.Instance.Streaks.Count > 0)
             averageStreak= streakTotal / GameManager.Instance.Streaks.Count;

            GameManager.Instance.CSVWriter.WriteTenBallCSV(GameManager.Instance.pegsHit, GameManager.Instance.maxScoreStreak, averageStreak, prevSpins + 1);
        }
    }

    public override void FixedExecute()
    {
        switch (GameManager.Instance.gmode)
        {
            case GMode.STANDARD:
                {
                    AddBallsStandard();
                    break;
                }
            case GMode.TEN_BALL:
                {
                    AddBalls10_Ball();
                    break;
                }
        }
    }
    //public void DropBalls()
    //{
    //    foreach (Ball obj in GameManager.Instance.balls)
    //    {
    //        obj.GetComponent<Rigidbody2D>().simulated = true;

    //        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300, 300), 99999990));

    //    }
    //}

}

