using System;
using UnityEngine;

public enum GameStatus
{
    GameRunning,
    GamePaused,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameStatus status;
    public int overAllCoins;
    public int currentCoins;
    public int totalCoins;
    public int multiplier;
    public float score;
    public float meters;
    public float timer;

    public bool IsJumping;
    public bool IsSliding;

    [SerializeField] public float speedWall;
    public static event Action<int> OnCoinCollected;

    public int wallMovimentDirection;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        multiplier = 1;
        wallMovimentDirection = 0;
        currentCoins = 0;
        totalCoins = 0;
        status = GameStatus.GameRunning;
        IsJumping = false;
        IsSliding = false;
        meters = 1;
    }


    private void Update()
    {
        //gestione punteggio
        meters += Time.deltaTime * speedWall * 100;
        score += (meters * multiplier) * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= 3)
        {
            speedWall += 0.001f;
            timer = 0;
        }

        if (status == GameStatus.GameRunning) { Time.timeScale = 1.0f; return; }

        else if (status == GameStatus.GamePaused) { Time.timeScale = 0f; return; }

    }

    public void UpdateCoinCount()
    {
        currentCoins++;
        totalCoins++;
        score += 100 * multiplier;
        OnCoinCollected?.Invoke(currentCoins);

        if(currentCoins>=100)
        {
            overAllCoins += totalCoins;
            totalCoins += currentCoins;
            currentCoins = 0;
            multiplier *= 2;
        }
    }
}
