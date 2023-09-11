
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
    public PreBallDrop PreBallDrop = new PreBallDrop();
    public ShootingBalls ShootingBalls = new ShootingBalls();
    public ReadyForSpin ReadyForSpin = new ReadyForSpin();
    public TextMeshProUGUI scoreTally;
    public TextMeshProUGUI BallTally;
    public TextMeshProUGUI ScoreStreakTally;
    public int ScoreMultiplier;
    public TextMeshProUGUI ScoreMultiplierTally;
    public int scoreStreak;
    public int maxScoreStreak = 0;
    public List<int> Streaks = new List<int>();
    private int Score;
    public int pegsHit = 0;

    public BallHolder leftBallHolder;
    public BallHolder rightBallHolder;

    public State CurrentState;
    public CSVWriter CSVWriter = new CSVWriter();
    public GameMode CurrentGameMode;
    public GMode gmode;
    public int ballWagerAmount;
    public int ballsStandardAmount;

    public bool changeReelDistance;
    public float obDistance;
    public bool GoalAddToScore = false;
    public bool PegAddToScore = false;
    public bool DeleteObOnHit;
    public AltObstacleManager AltObstacleManager;

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
        CSVWriter.CreateCSVGoals();
        CSVWriter.CreateTenBallGoals();
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
        Debug.Log(CurrentState.ToString());
        CurrentState.Execute();
        scoreTally.text = $"{Score}";
        ScoreStreakTally.text = $"{scoreStreak}X";
        ScoreMultiplierTally.text = $"{ScoreMultiplier}%";
        DisplayBalls();
        if (changeReelDistance)
        {
            foreach (SpinningReel reel in Reels)
            {
                reel.maxObDistance = obDistance;
                reel.minObDistance = obDistance;
            }
        }

    }
    public void FixedUpdate()
    {
        CurrentState.FixedExecute();
    }

    public void EnterSpinState()
    {
        if (CurrentState == ReadyForSpin)
        {
            ChangeState(PreBallDrop);
            Score -= 100 * ballWagerAmount;
        }
    }

    public void AddToScore(int scoreAddition)
    {
        

        Score += scoreAddition;
        scoreStreak+= 1;
        if (scoreStreak > maxScoreStreak)
            maxScoreStreak = scoreStreak;
        
        ScoreMultiplier += ScoreMultiplier / 10;
        if (scoreAddition == 0)
        {
            if (scoreStreak != 0)
                Streaks.Add(scoreStreak);
            scoreStreak = 0;
            ScoreMultiplier = 100;
            foreach (Goal goal in Goals)
            {
                goal.Score = goal.BaseScore;
                
            }

        }
    }

    public void UpgradeAddToScore()
    {
        

        scoreStreak += 1;
        ScoreMultiplier += ScoreMultiplier / 10;
        if (!PegAddToScore)
            return;
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
        ballWagerAmount++;
    }
    public void DecreaseWager()
    {
        if (ballWagerAmount > 1)
        ballWagerAmount--;
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
