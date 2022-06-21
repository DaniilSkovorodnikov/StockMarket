using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Converter : MonoBehaviour
{
    [SerializeField] private Image inputCurrency;
    [SerializeField] private Image outputCurrency;
    [SerializeField] private Text inputCurrencyT;
    [SerializeField] private Text outputCurrencyT;
    [SerializeField] private Text outputValue;
    [SerializeField] private InputField valueToConvert;
    [SerializeField] private Text rublesT;
    [SerializeField] private Text dollarsT;
    [SerializeField] private GameObject operationErrorPanel;
    [SerializeField] private GameObject operationSuccessPanel;
    private bool currentCurrencyIsRubles = true;
    private double rate = 65.78;
    private double inputValue;

    public void Convert()
    {
        if (currentCurrencyIsRubles)
            ConvertRublesToDollars();
        else
            ConvertDollarsToRubles();
        rublesT.text = Math.Round(GameController.Rubles, 2).ToString();
        dollarsT.text = Math.Round(GameController.Dollars, 2).ToString();
    }

    private void ConvertRublesToDollars()
    {
        if (inputValue > GameController.Rubles)
        {
            operationErrorPanel.SetActive(true);
            return;
        }
        var rubles = Math.Round(inputValue, 2);
        var dollars = Math.Round(rubles / rate, 2);
        GameController.Rubles -= rubles;
        GameController.Dollars += dollars;
        operationSuccessPanel.SetActive(true);
    }

    private void ConvertDollarsToRubles()
    {
        if (inputValue > GameController.Dollars)
        {
            operationErrorPanel.SetActive(true);
            return ;
        }
        var dollars = inputValue;
        var rubles = dollars * rate;
        GameController.Rubles += Math.Round(rubles, 2);
        GameController.Dollars -= Math.Round(dollars, 2);
        operationSuccessPanel.SetActive(true);
    }

    public void OnChangeInputValue()
    {  
        if(double.TryParse(valueToConvert.text, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out double result))
        {
            inputValue = result;
            if(currentCurrencyIsRubles)
                outputValue.text = Math.Round(result / rate, 2).ToString();
            else
                outputValue.text = Math.Round(result * rate, 2).ToString();
        }
    }

    public void Swap()
    {
        var temp = inputCurrency.sprite;
        var tempT = inputCurrencyT.text;
        inputCurrency.sprite = outputCurrency.sprite;
        inputCurrencyT.text = outputCurrencyT.text;
        outputCurrency.sprite = temp;
        outputCurrencyT.text = tempT;
        if(currentCurrencyIsRubles)
            currentCurrencyIsRubles = false;
        else
            currentCurrencyIsRubles = true;
    }
}
