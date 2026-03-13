using System;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    [Header("------Menus--------")]
    [SerializeField] AudioClip buttonSFX;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject endPanel;
    [SerializeField] GameObject inGameUI;
    [Header("-------EndPanelStuff--------")]
    [SerializeField] TMP_Text scoreENDText;
    [SerializeField] TMP_Text coinsEndText;
    [SerializeField] TMP_Text multiplierEndText;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject deathWater;
    [SerializeField] GameObject deathTree;
    [SerializeField] GameObject deathRoot;
    [Header("----------PauseMenuStuff--------")]
    [SerializeField] TMP_Text scorePauseText;
    [SerializeField] TMP_Text coinsPauseText;
    [SerializeField] TMP_Text metersPauseText;
    [SerializeField] TMP_Text multiplierPauseText;
    [Header("----HUDStuff----")]
    [SerializeField] Image moneyBar;
    [SerializeField] Image moneyBall;
    [SerializeField] TMP_Text scoreTXT;
    [SerializeField] TMP_Text coinsText;


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
        GameManager.OnCoinCollected += CoinFillManager;
        GameManager.OnCoinCollected += UpdateCoinsTXT;
    }

    private void OnDisable()
    {
        GameManager.OnCoinCollected -= CoinFillManager;
        GameManager.OnCoinCollected -= UpdateCoinsTXT;
    }
    private void Update()
    {
        scoreTXT.text = GameManager.instance.score.ToString("0");

        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.status == GameStatus.GameRunning)
        {



            OpenPauseMenu();

            return;
        }


        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.status == GameStatus.GamePaused)
        {
            
            if (PauseMenu.activeInHierarchy && !optionsMenu.activeInHierarchy)
            {
                
                ClosePauseMenu();
                return;

            }

            if (optionsMenu.activeInHierarchy)
            {
                
                CloseOptionsMenu();
                return;

            }

        }

    }

    public void OpenPauseMenu()
    {
        inGameUI.SetActive(false);
        PauseMenu.SetActive(true);
        scorePauseText.text = GameManager.instance.score.ToString("0");
        coinsPauseText.text = GameManager.instance.totalCoins.ToString();
        metersPauseText.text = GameManager.instance.meters.ToString("0"+ "m");
        multiplierPauseText.text = GameManager.instance.multiplier.ToString("0" + "x");
        GameManager.instance.status = GameStatus.GamePaused;

    }

    public void ClosePauseMenu()
    {
        inGameUI.SetActive(true);
        PauseMenu.SetActive(false);
        GameManager.instance.status = GameStatus.GameRunning;

    }

    public void OpenOptionsMenu()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonSFX, transform, 1f);
        PauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonSFX, transform, 1f);
        PauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void DeathByWater()
    {
        GameFinished();
        endPanel.SetActive(true);
        deathWater.SetActive(true);
    }

    public void DeathByTree()
    {
        GameFinished();
        endPanel.SetActive(true);
        deathTree.SetActive(true);
    }

    public void DeathByRoot()
    {
        GameFinished();
        endPanel.SetActive(true);
        deathRoot.SetActive(true);
    }

    public void GameFinished()
    {

        inGameUI.SetActive(false);
        scoreENDText.text = GameManager.instance.score.ToString("0" + "m");
        coinsEndText.text = GameManager.instance.totalCoins.ToString();
        multiplierEndText.text = GameManager.instance.multiplier.ToString("0" + "x");
        GameManager.instance.status = GameStatus.GamePaused;
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

    }

    public void UpdateCoinsTXT(int coin)
    {
        coin = GameManager.instance.totalCoins;
        coinsText.text = coin.ToString();

    }
}

