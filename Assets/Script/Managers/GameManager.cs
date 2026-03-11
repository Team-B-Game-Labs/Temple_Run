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
    public int totalCoins;
    public int currentCoins;
    public float timer;

    public bool IsJumping;
    public bool IsSliding;

    [SerializeField] public float speedWall;



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
        currentCoins = 0;
        status = GameStatus.GameRunning;
        IsJumping = false;
        IsSliding = false;
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3)
        {
            speedWall += 0.001f;
            timer = 0;
        }

        if (status == GameStatus.GameRunning) { Time.timeScale = 1.0f; return; }

        else if (status == GameStatus.GamePaused) { Time.timeScale = 0f; return; }

    }
}
