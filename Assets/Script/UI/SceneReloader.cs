using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneReloader : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] AudioClip buttonSFX;
    public void BackToMainMenu()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonSFX, transform, 1f);
        SceneManager.LoadScene(0);
        DontDestroyOnLoad(gameManager);
    }

    public void Quit()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonSFX, transform, 1f);
        Application.Quit();
    }

    public void StartGame()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonSFX, transform, 1f);
        SceneManager.LoadScene(1);
        DontDestroyOnLoad(gameManager);
    }
}
