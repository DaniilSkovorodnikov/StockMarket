using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Company : MonoBehaviour
{
    [SerializeField] private double Price;
    public int Amount { get; private set; }
    public double TotalCost { get; private set; }
    private double dynamics;
    private double dynamicsPercent;
    private double prevDynamicsPercent;
    [SerializeField] private CompanyNames companyName;
    public CompanyNames Name => companyName;
    [SerializeField] private Currency currency;
    public Currency Currency => currency;
    [SerializeField] private Text amountT;
    [SerializeField] private Text priceT;
    [SerializeField] private Text priceInBuyWindow;
    [SerializeField] private Text priceInCompanyCard;
    [SerializeField] private Text totalCostT;
    [SerializeField] private Text dynamicsT;
    public bool isHaveDivs;
    public int stepsForPayDivs;
    public double divPercents;
    [SerializeField] public double Prices { get; set; }

    public void BuyStock(int count)
    {
        Amount += count;
        TotalCost += Price * count;
        amountT.text = Amount.ToString();
        totalCostT.text = string.Format("{0} {1}", Math.Round(TotalCost, 2), ConvertCurrencyToString(currency));
        if(currency == Currency.Rubles)
            GameController.Rubles -= Math.Round(Price * count * 1.01, 2);
        else
            GameController.Dollars -= Math.Round(Price * count * 1.01, 2);
    }

    public void SellStock(int count)
    {
        Amount -= count;
        TotalCost -= Price * count;
        amountT.text = Amount.ToString();
        totalCostT.text = string.Format("{0} {1}", Math.Round(TotalCost, 2), ConvertCurrencyToString(currency));
        if(currency == Currency.Rubles)
            GameController.Rubles += Math.Round(Price * count * 0.99, 2);
        else
            GameController.Dollars += Math.Round(Price * count * 0.99, 2);
    }

    public void RecalculateCompanyStatistics(double influence)
    {
        var influencePercent = influence / 100;
        var currencyStr = ConvertCurrencyToString(currency);
        Price += Price * influencePercent;
        dynamics += TotalCost * influencePercent;
        dynamicsPercent = (100 + prevDynamicsPercent)*influencePercent + prevDynamicsPercent;
        prevDynamicsPercent = dynamicsPercent;
        TotalCost += TotalCost * influencePercent;
        priceT.text = string.Format("{0} {1}", Math.Round(Price, 2), currencyStr);
        priceInBuyWindow.text = string.Format("{0} {1}", Math.Round(Price, 2), currencyStr);
        priceInCompanyCard.text = string.Format("{0} {1}", Math.Round(Price, 2), currencyStr);
        totalCostT.text= string.Format("{0} {1}", Math.Round(TotalCost, 2), currencyStr);
        dynamicsT.text = string.Format("{0} {1} ({2}%)", Math.Round(dynamics, 2), currencyStr, Math.Round(dynamicsPercent, 2));
        if (dynamics > 0)
            dynamicsT.GetComponent<Text>().color = new Color(0, 255, 0);
        if (dynamics < 0)
            dynamicsT.GetComponent<Text>().color = new Color(255, 0, 0);
    }

    public string ConvertCurrencyToString(Currency currency)
    {
        if (currency == Currency.Rubles) return "₽";
        return "$";
    }

    public bool IsPossibleToBuy(int count)
    {
        if(currency == Currency.Rubles && Price * count <= GameController.Rubles) 
            return true;
        if (currency == Currency.Dollars && Price * count <= GameController.Dollars)
            return true;
        return false;
    }

    public bool IsPossibleToSell(int count)
    {
        return count <= Amount;
    }
}

public enum CompanyNames
{
    Gazprom,
    Ozon,
    Lenta,
    Polus,
    ExxonMobile,
    FirstSolar
}

public enum Currency
{
    Rubles,
    Dollars
}
