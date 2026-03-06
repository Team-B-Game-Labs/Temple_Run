using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject deathWater;
    [SerializeField] GameObject deathTree;
    [SerializeField] GameObject deathRoot;
    
  
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

    public void DeathByWater()
    {
        deathWater.SetActive(true);
    }

    public void DeathByTree()
    {
        deathTree.SetActive(true);
    }

    public void DeathByRoot()
    {
        deathRoot.SetActive(true);
    }

}

