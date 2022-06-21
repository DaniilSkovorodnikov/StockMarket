using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStatistics : MonoBehaviour
{
    [SerializeField] private GameObject statisticsPanel;
    [SerializeField] private Text rubles;
    [SerializeField] private Text dollars;
    [SerializeField] private Text stocksCount;
    [SerializeField] private Text depositCount;
    [SerializeField] private Text rubGain;
    [SerializeField] private Text dollarGain;

    public void ShowStatistics()
    {
        if (!GameController.IsGameOver)
            return;
        var allRubles = GameController.Rubles;
        var allDollars = GameController.Dollars;
        foreach (var company in GameController.companies.Values)
        {
            if(company.Currency == Currency.Rubles)
                allRubles += company.TotalCost;
            else
                allDollars += company.TotalCost;
        }
        foreach (var deposit in Bank.deposits)
        {
            if (deposit.Currency == Currency.Rubles)
                allRubles += deposit.Money;
            else 
                allDollars += deposit.Money;
        }
        allRubles = Math.Round(allRubles, 2);
        allDollars = Math.Round(allDollars, 2);
        rubles.text = allRubles.ToString();
        dollars.text = allDollars.ToString();
        stocksCount.text = GameController.stocksPurchased.ToString();
        depositCount.text = "0";
        rubGain.text = (allRubles - 100000).ToString();
        dollarGain.text = (allDollars - 1000).ToString();
        statisticsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
