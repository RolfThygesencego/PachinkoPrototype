using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int Score;
    public int BaseScore;
    public int ballCount;
    public int maxBallCount;
    public int timesScored;
    public GameObject bottomCube;
    public float ballFallDownTime = 15f;
    private void Awake()
    {
        BaseScore = Score;
        ballCount = 0;
        ballFallDownTime = 10f;
    }
    private void Update()
    {
        scoreText.text = $"{Score}";
        if (ballCount == maxBallCount)
        {
            bottomCube.GetComponentInChildren<Rigidbody2D>().simulated = false;
            bottomCube.GetComponentInChildren<MeshRenderer>().enabled = false;
            ballFallDownTime -= 0.1f;
            if (ballFallDownTime < 0)
            {
                bottomCube.GetComponentInChildren<Rigidbody2D>().simulated = true;
                bottomCube.GetComponentInChildren<MeshRenderer>().enabled = true;
                ballCount = 0;
                ballFallDownTime = 15f;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("triggered");
            BallNoRand ball = collision.gameObject.GetComponent<BallNoRand>();
            if (!ball.ballScoreAdded)
            {
                GameManager.Instance.Ballsdropping.ballsFinished += 1;
                GameManager.Instance.AddToScore(Score);
                Score += (Score / 10);
                if (Score / 10 < 1 && Score != 0)
                    Score += 1;

                ball.ballScoreAdded = true;
                timesScored++;

                GameManager.Instance.Ballsdropping.ReadyForNextBall = true;
                if(GameManager.Instance.gmode == GMode.TEN_BALL)
                {
                    SpinningReel reel = GameManager.Instance.Reels[Random.Range(0, GameManager.Instance.Reels.Count)];
                    reel.obstacleManager.ChangeObstacleToSpecial();
                }
                ballCount++;
            }

//work please
//be nice
        }
    }

}
