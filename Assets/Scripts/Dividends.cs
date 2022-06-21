using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dividends : MonoBehaviour
{
    public static double gazpromSum { get; private set; }
    public static int gazpromPayouts { get; private set; }
    public static double gazpromPercents { get; private set; }
    public static double exxonSum { get; private set; }
    public static int exxonPayouts { get; private set; }
    public static double exxonPercents { get; private set; }
    public static double poluysSum { get; private set; }
    public static int poluysPayouts { get; private set; }
    public static double polyusPercents { get; private set; }

    public static void PayoutDividends()
    {
        foreach (Company company in GameController.companies.Values)
        {
            if(company.isHaveDivs && GameController.countSteps % company.stepsForPayDivs == 0 && company.Amount > 0)
            {
                if (company.Currency == Currency.Rubles)
                {
                    var divSum = company.TotalCost * (company.divPercents / 100d);
                    switch (company.Name)
                    {
                        case CompanyNames.Gazprom:
                            gazpromSum += divSum;
                            gazpromPayouts++;
                            gazpromPercents += company.divPercents;
                            break;
                        case CompanyNames.Polus:
                            poluysSum += divSum;
                            poluysPayouts++;
                            polyusPercents += company.divPercents;
                            break;
                    }
                    GameController.Rubles += divSum;
                }
                else
                {
                    var divSum = company.TotalCost * (company.divPercents / 100d);
                    exxonSum += divSum;
                    exxonPayouts++;
                    exxonPercents += company.divPercents;
                    GameController.Dollars += divSum;
                }
            }
        }
    }
}
