using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class News : MonoBehaviour
{
    public TypeNews Type { get; set; }
    public double Influence { get; set; }
    public int Index { get; set; }

    public static CompanyNames ConvertTypeToCompany(TypeNews type)
    {
        switch (type)
        {
            case TypeNews.Oil: return CompanyNames.ExxonMobile;
            case TypeNews.Gas: return CompanyNames.Gazprom;
            case TypeNews.OnlineStore: return CompanyNames.Ozon;
            case TypeNews.ChainStores: return CompanyNames.Lenta;
            case TypeNews.SolarEnergy: return CompanyNames.FirstSolar;
            case TypeNews.Gold: return CompanyNames.Polus;
            default: return CompanyNames.ExxonMobile;
        }
    }
}
public enum TypeNews
{
    Other,
    Oil,
    Gas,
    OnlineStore,
    ChainStores,
    SolarEnergy,
    Gold,
    NanoTech,
    Metal,
    Messenger,
    Tesla
}
