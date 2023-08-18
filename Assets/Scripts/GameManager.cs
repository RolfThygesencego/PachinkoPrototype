using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum GMode { STANDARD, TEN_BALL }

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
    public TextMeshProUGUI ScoreStreakTally;
    public int ScoreMultiplier;
    public TextMeshProUGUI ScoreMultiplierTally;
    public int scoreStreak;
    private int Score;

    public State CurrentState;
    public CSVWriter CSVWriter = new CSVWriter();
    public GameMode CurrentGameMode;
    public GMode gmode;
    public int ballWagerAmount;
    public int ballsStandardAmount;

    public void Awake()
    {

        
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        Score = 100;
        scoreStreak = 0;
        ScoreMultiplier = 100;
        ChangeRules(CurrentGameMode);
        ChangeState(ReadyForSpin);
        reelFinished.AddListener(Spinning.reelFinishedSpinning);
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
        ScoreStreakTally.text = $"{scoreStreak}X";
        ScoreMultiplierTally.text = $"{ScoreMultiplier}%";
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
        scoreStreak+= 1;
        ScoreMultiplier += ScoreMultiplier / 10;
        if (scoreAddition == 0)
        {
            scoreStreak = 0;
            ScoreMultiplier = 100;
            foreach (Goal goal in Goals)
            {
                goal.Score = goal.BaseScore;
                foreach (SpinningReel reel in Reels)
                {
                    reel.obstacleManager.RefreshUpgrades();
                }
            }
        }
    }

    public void UpgradeAddToScore()
    {
        scoreStreak += 1;
        ScoreMultiplier += ScoreMultiplier / 10;
        foreach (Goal goal in Goals)
        {
            
            goal.Score += goal.Score / 20;
            if (goal.Score / 20 < 1 && goal.Score != 0)
                goal.Score += 1;
        }
    }

    public void DisplayBalls()
    {
        BallTally.text = $"Balls {Spinning.ballWager}";
    }

    public void IncreaseWager()
    {
        if (Spinning.ballWager < 100 && CurrentState == ReadyForSpin)
            Spinning.ballWager += ballWagerAmount;
    }
    public void DecreaseWager()
    {
        if (Spinning.ballWager > 1 && CurrentState == ReadyForSpin)
            Spinning.ballWager -= ballWagerAmount;
        if (Spinning.ballWager < 1)
            Spinning.ballWager = 1;
    }

    void ChangeRules(GameMode gameMode)
    {
        gmode = gameMode.gMode;
        ball.GetComponent<Rigidbody2D>().sharedMaterial.bounciness = gameMode.ballBounciness;
        ball.GetComponent<Rigidbody2D>().gravityScale = gameMode.ballGravityScale;
        ballWagerAmount = gameMode.ballWager;
        ballsStandardAmount = gameMode.standardBallsAmount;

    }
}
