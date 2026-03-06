using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneReloader : MonoBehaviour
{
    GameManager gameManager;
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        DontDestroyOnLoad(gameManager);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        DontDestroyOnLoad(gameManager);
    }
}
