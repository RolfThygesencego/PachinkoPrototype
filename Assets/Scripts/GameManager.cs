using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Ball ball;
    public List<Ball> balls = new List<Ball>();

    public List<SpinningReel> Reels = new List<SpinningReel>();
    public List<Goal> Goals = new List<Goal>();
    public UnityEvent reelFinished = new UnityEvent();
    public Ballsdropping Ballsdropping = new Ballsdropping();
    public Spinning Spinning = new Spinning();
    public ReadyForSpin ReadyForSpin = new ReadyForSpin();
    public TextMeshProUGUI scoreTally;
    public TextMeshProUGUI BallTally;
    private int Score;
    
    public State CurrentState;
    public CSVWriter CSVWriter = new CSVWriter();

    public void Awake()
    {
        Score = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        reelFinished.AddListener(Spinning.reelFinishedSpinning);
        CurrentState = ReadyForSpin;
        CSVWriter.CreateCVSGoals();
    }

    public void ChangeState(State nextState)
    {
        if (CurrentState != null)
        {
            CurrentState.End();
        }
        nextState.Initialize();
        CurrentState = nextState;
    }
    public void Update()
    {
        //Debug.Log(CurrentState.ToString());
        CurrentState.Execute();
        scoreTally.text = $"{Score}";
        DisplayBalls();

    }

    public void EnterSpinState()
    {
        if (CurrentState == ReadyForSpin)
        {
            ChangeState(Spinning);
            Score -= 100 * Spinning.ballWager;
        }
    }

    public void AddToScore(int scoreAddition)
    {
        Score += scoreAddition;
        if (scoreAddition == 0)
        {
            foreach (Goal goal in Goals)
            {
                goal.Score = goal.BaseScore;
            }
        }
    }

    public void UpgradeAddToScore()
    {
        foreach (Goal goal in Goals)
        {
            goal.Score += goal.Score / 10;
        }
    }

    public void DisplayBalls()
    {
        BallTally.text = $"Balls {Spinning.ballWager}";
    }

    public void IncreaseWager()
    {
        if (Spinning.ballWager < 100 && CurrentState == ReadyForSpin)
            Spinning.ballWager += 5;
    }
    public void DecreaseWager()
    {
        if (Spinning.ballWager > 1 && CurrentState == ReadyForSpin)
            Spinning.ballWager -= 5;
    }
}
