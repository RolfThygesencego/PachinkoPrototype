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
    public float ballTimer = 3f;
    public float ballTimerMax = 5f;
    public List<Ball> readyBalls = new List<Ball>();
    public int[] goalScores = new int[9];
    public bool ReadyForNextBall = false;
    
    public override void End()
    {
        ballsToBeAdded = 0;
        
        saveData();
    }

    public override void Execute()
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
        DropBalls();
    }
    public void DropBalls()
    {
        foreach (Ball obj in GameManager.Instance.balls)
        {
            obj.GetComponent<Rigidbody2D>().simulated = true;

            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300, 300), 0));
        }
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
            if (readyBalls.Count > 0 && ReadyForNextBall)
            {
                readyBalls[0].GetComponent<Rigidbody2D>().simulated = true;
                readyBalls[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-450, 450), 0));
                ballTimer = ballTimerMax;
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



    }
  
}

