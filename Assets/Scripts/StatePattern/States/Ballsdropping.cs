using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Ballsdropping : State
{
    public int ballsFinished = 0;
    public int ballsToBeAdded = 0;
    public float ballTimer = 3f;
    public float ballTimerMax = 20f;
    public List<Ball> readyBalls = new List<Ball>();
    public override void End()
    {

    }

    public override void Execute()
    {
        AddBalls();
        if (ballsFinished >= GameManager.Instance.balls.Count && ballsFinished > 0)
        {
            GameManager.Instance.ChangeState(GameManager.Instance.ReadyForSpin);
        }

    }

    public override void Initialize()
    {
        ballsToBeAdded = 1;
        ballsFinished = 0;
        ballTimer = 0.5f;
        DropBalls();
    }
    public void DropBalls()
    {
        foreach (Ball obj in GameManager.Instance.balls)
        {
            obj.GetComponent<Rigidbody2D>().simulated = true;

            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, 200), 0));
        }
    }

    public void AddBalls()
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
}

