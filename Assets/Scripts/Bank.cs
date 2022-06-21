using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    [SerializeField] private InputField moneyToDeposit;
    [SerializeField] private InputField moneyToWithdraw;
    [SerializeField] private List<GameObject> depositPanels = new List<GameObject>();
    private static int currentDepositIndex;
    public static readonly List<Deposit> deposits = new List<Deposit>();
    [SerializeField] private GameObject operationErrorPanel;
    [SerializeField] private GameObject operationSuccessPanel;
    [SerializeField] private GameObject notEnoughMoneyPanel;
    [SerializeField] private Text totalRubs;
    [SerializeField] private Text totalDollars;

    public void Start()
    {
        InitializeDeposits();
    }

    private void InitializeDeposits()
    {
        deposits.Add(depositPanels[0].GetComponent<Deposit>().InitializeDeposit(9, false, Currency.Rubles,4,12));
        deposits.Add(depositPanels[1].GetComponent<Deposit>().InitializeDeposit(6, true, Currency.Rubles,2,3));
        deposits.Add(depositPanels[2].GetComponent<Deposit>().InitializeDeposit(3, true, Currency.Dollars,2,3));
    }

    public void OnClickSavingAccButtons()
    {
        currentDepositIndex = 0;
    }

    public void OnClickDepositInRub()
    {
        currentDepositIndex = 1;
    }

    public void OnClickDepositInDollar()
    {
        currentDepositIndex = 2;
    }

    public void ToDeposit()
    {
        var currentDeposit = deposits[currentDepositIndex];
        if(double.TryParse(moneyToDeposit.text, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out double result))
        {
            DoDeposit(currentDeposit, result);
            operationSuccessPanel.SetActive(true);
            if(!currentDeposit.IsActivated) 
                currentDeposit.IsActivated = true;
        }
        else
            operationErrorPanel.SetActive(true);
    }

    public void ToWithdraw()
    {
        var currentDeposit = deposits[currentDepositIndex];
        if (double.TryParse(moneyToWithdraw.text, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out double result))
        {
            DoWithdraw(currentDeposit, result);
            operationSuccessPanel.SetActive(true);
        }
        else
            operationErrorPanel.SetActive(true);
    }

    private void DoDeposit(Deposit deposit, double money)
    {
        if (deposit.Currency == Currency.Rubles && money <= GameController.Rubles)
        {
            deposit.Money += money;
            GameController.Rubles -= money;
            totalRubs.text = string.Format("{0} ₽", Math.Round(GameController.Rubles, 2).ToString());
        }
        else if (deposit.Currency == Currency.Dollars && money <= GameController.Dollars)
        {
            deposit.Money += money;
            GameController.Dollars -= money;
            totalDollars.text = string.Format("{0} $", Math.Round(GameController.Dollars, 2).ToString());
        }
        else
        {
            notEnoughMoneyPanel.SetActive(true);
            return;
        }
        deposit.OnChangeMoney();
    }

    private void DoWithdraw(Deposit deposit, double money)
    {
        if (deposit.Currency == Currency.Rubles && money <= deposit.Money)
        {
            deposit.Money -= money;
            GameController.Rubles += money;
            totalRubs.text = string.Format("{0} ₽", Math.Round(GameController.Rubles, 2).ToString());
        }
        else if (deposit.Currency == Currency.Dollars && money <= deposit.Money)
        {
            deposit.Money -= money;
            GameController.Dollars += money;
            totalDollars.text = string.Format("{0} $", Math.Round(GameController.Dollars, 2).ToString());
        }
        else
        {
            notEnoughMoneyPanel.SetActive(true);
            return;
        }
        deposit.OnChangeMoney();
    }

    public void OnEndStep()
    {
        foreach(var deposit in deposits)
        {
            if(deposit.IsActivated)
            {
                deposit.currentStepWhenActivated++;
                if(deposit.currentStepWhenActivated * 3 % deposit.InterestPeriod == 0)
                {
                    deposit.Money = Math.Round(deposit.Money * (deposit.Percent/100 + 1), 2);
                    deposit.OnChangeMoney();
                }
                if(deposit.currentStepWhenActivated == deposit.DepositTerm)
                    DoWithdraw(deposit,deposit.Money);
            }
        }
    }
}
