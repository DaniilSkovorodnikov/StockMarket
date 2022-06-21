using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject newspaper;
    [SerializeField] private GameObject pauseMenu;
    private bool IsOpenedPauseMenu = false;
    [SerializeField] private List<GameObject> buyStocks;
    public static int currentStock { get; private set; }
    public GameObject completeMovePanel;
    [SerializeField] private GameObject notEnoughMoneyPanel;
    [SerializeField] private GameObject operationErrorPanel;
    [SerializeField] private GameObject operationSuccessPanel;
    [SerializeField] private GameObject depositPanel;
    [SerializeField] private GameObject withdrawPanel;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenClosePauseMenuEsc();
    }

    public void OpenDepositPanel()
    {
        depositPanel.SetActive(true);
    }

    public void OpenWithdrawPanel()
    {
        withdrawPanel.SetActive(true);
    }

    public void CloseDepositPanel()
    {
        depositPanel.SetActive(false);
    }

    public void CloseWithdrawPanel()
    {
        withdrawPanel.SetActive(false);
    }

    public void OpenCompleteMovePanel()
    {
        completeMovePanel.SetActive(true);
    }

    public void CloseCompleteMovePanel()
    {
        completeMovePanel.SetActive(false);
    }

    public void CloseStockWindow()
    {
        buyStocks[currentStock].SetActive(false);
    }

    public void BuyGazprom()
    {
        buyStocks[0].SetActive(true);
        currentStock = 0;
    }

    public void BuyOzon()
    {
        buyStocks[1].SetActive(true);
        currentStock = 1;
    }

    public void BuyLenta()
    {
        buyStocks[2].SetActive(true);
        currentStock = 2;
    }

    public void BuyPolus()
    {
        buyStocks[3].SetActive(true);
        currentStock = 3;
    }

    public void BuyExxon()
    {
        buyStocks[4].SetActive(true);
        currentStock = 4;
    }

    public void BuyFirstSolar()
    {
        buyStocks[5].SetActive(true);
        currentStock = 5;
    }

    public void OpenNewspaper()
    {
        newspaper.SetActive(true);
    }

    public void OpenClosePauseMenuEsc()
    {
        if (IsOpenedPauseMenu)
        {
            pauseMenu.SetActive(false);
            IsOpenedPauseMenu = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            IsOpenedPauseMenu = true;
        }

    }

    public void OpenPauseMenuButton()
    {
        pauseMenu.SetActive(true);
        IsOpenedPauseMenu = true;
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        IsOpenedPauseMenu = false;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseOperationSuccessPanel()
    {
        operationSuccessPanel.SetActive(false);
    }

    public void CloseOperationErrorPanel()
    {
        operationErrorPanel.SetActive(false);
    }

    public void CloseNotEnoughMoneyPanel()
    {
        notEnoughMoneyPanel.SetActive(false);
    }
}
