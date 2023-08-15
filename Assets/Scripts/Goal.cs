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
            }
//work please
//be nice
        }
    }
}
