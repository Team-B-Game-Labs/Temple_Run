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
    private void Awake()
    {
        if(instance != null)
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
    }


    private void Update()
    {
        if(status == GameStatus.GameRunning) { Time.timeScale = 1.0f; return; }
        
        else if(status == GameStatus.GamePaused) { Time.timeScale = 0f; return; }
    }
}
