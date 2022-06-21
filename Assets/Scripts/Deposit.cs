using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deposit : MonoBehaviour
{
    public double Percent { get; private set; }
    public double Money { get; set; }
    public bool IsPossibleWithdraw { get; private set; }
    public Currency Currency { get; private set; }
    public int currentStepWhenActivated { get; set; }
    public bool IsActivated { get; set; } = false;
    public int DepositTerm { get; set; }
    public int InterestPeriod { get; set; }
    [SerializeField] private Text moneyT;


    public Deposit InitializeDeposit(int percent, bool isPossibleWithdraw, Currency currency, int depositTerm, int interestPeriod)
    {
        Percent = (double)percent / 4;
        IsPossibleWithdraw = isPossibleWithdraw;
        Currency = currency;
        DepositTerm = depositTerm;
        InterestPeriod = interestPeriod;
        return this;
    }

    public void OnChangeMoney()
    {
        if(Currency == Currency.Rubles)
            moneyT.text = string.Format("{0} ₽", Money);
        else
            moneyT.text = string.Format("{0} $", Money);
    }
}
