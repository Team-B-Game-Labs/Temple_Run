using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.status == GameStatus.GameRunning)
        {
            OpenPauseMenu();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.status == GameStatus.GamePaused)
        {
            ClosePauseMenu();
            return;
        }

    }

    public void OpenPauseMenu()
    {
        PauseMenu.SetActive(true);
        GameManager.instance.status = GameStatus.GamePaused;

    }

    public void ClosePauseMenu()
    {
        PauseMenu.SetActive(false);
        GameManager.instance.status = GameStatus.GameRunning;

    }
}

