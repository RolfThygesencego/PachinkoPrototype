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

    public int timesScored;
    private void Awake()
    {
        BaseScore = Score;
    }
    private void Update()
    {
        scoreText.text = $"{Score}";
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("triggered");
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (!ball.ballScoreAdded)
            {
                GameManager.Instance.Ballsdropping.ballsFinished += 1;
                GameManager.Instance.AddToScore(Score);
                Score += (Score / 10);
                ball.ballScoreAdded = true;
                timesScored++;

                GameManager.Instance.Ballsdropping.ReadyForNextBall = true;
                if(GameManager.Instance.gmode == GMode.TEN_BALL)
                {
                    SpinningReel reel = GameManager.Instance.Reels[Random.Range(0, GameManager.Instance.Reels.Count)];
                    reel.obstacleManager.ChangeObstacleToSpecial();
                }
            }

//work please
//be nice
        }
    }

}
