using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    [SerializeField] AudioClip buttonSFX;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject endPanel;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject deathWater;
    [SerializeField] GameObject deathTree;
    [SerializeField] GameObject deathRoot;

    [SerializeField] Image moneyBar;
    [SerializeField] Image moneyBall;

    int fillCoinBall;
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
        moneyBall.fillAmount = 0;
        moneyBar.fillAmount = 0;
    }

    private void OnEnable()
    {
        GameManager.onCoinCollected += CoinFillManager;
    }

    private void OnDisable()
    {
        GameManager.onCoinCollected -= CoinFillManager;
    }
    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.status == GameStatus.GameRunning)
        {



            OpenPauseMenu();

            return;
        }


        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.status == GameStatus.GamePaused)
        {
            if (optionsMenu == true)
            {
                CloseOptionsMenu(); return;
            }
            else if (PauseMenu == true)
            {
                ClosePauseMenu();
                return;
            }
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

    public void OpenOptionsMenu()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonSFX, transform, 1f);
        optionsMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void CloseOptionsMenu()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonSFX, transform, 1f);
        optionsMenu.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void DeathByWater()
    {
        endPanel.SetActive(true);
        deathWater.SetActive(true);
    }

    public void DeathByTree()
    {
        endPanel.SetActive(true);
        deathTree.SetActive(true);
    }

    public void DeathByRoot()
    {
        endPanel.SetActive(true);
        deathRoot.SetActive(true);
    }


    public void CoinFillManager(int totalCoins)
    {
        moneyBar.fillAmount = Mathf.Clamp01((float)totalCoins / 80f);
        if (totalCoins > 80)
        {
            moneyBall.fillAmount = Mathf.Clamp01((float)(totalCoins - 80) / 20f);
            moneyBar.fillAmount = 1f;
        }
        else moneyBall.fillAmount = 0f;






       // if(GameManager.instance.currentCoins <= 80)
       // {
       //     moneyBall.fillAmount = 0;
       // }
       // else if(GameManager.instance.currentCoins >= 80)
       // {
       //     moneyBall.fillAmount = (float)GameManager.instance.currentCoins / 800f;
       // }

            // moneyBar.fillAmount = (float)GameManager.instance.currentCoins / 80f;
            //;

            // if (GameManager.instance.currentCoins >= 80) { moneyBar.fillAmount = 1f; }
    }
}

