using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text totalCoins;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject mainUI;
    [SerializeField] AudioClip buttonSFX;
    public float rotationSpeed;
    public float maxAngle;
    float time;

    private void Start()
    {
        time = 0;
    }
    private void Update()
    {
        totalCoins.text = "Total Coins: "; // + GameManager.instance.totalCoins  

        time += Time.deltaTime * rotationSpeed;

        float rotation = Mathf.PingPong(time, maxAngle * 2) - maxAngle;
        startText.transform.rotation = Quaternion.Euler(0,0,rotation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void OpenOptionsMenu()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonSFX, transform, 1f);
        optionsMenu.SetActive(true);
        mainUI.SetActive(false);
    }

    public void CloseOptionsMenu()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonSFX, transform, 1f);
        optionsMenu.SetActive(false);
        mainUI.SetActive(true);
    }
}
