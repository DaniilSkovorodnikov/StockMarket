using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject news1;
    [SerializeField] private GameObject news2;
    [SerializeField] private GameObject news3;
    [SerializeField] private GameObject news4;
    [SerializeField] private GameObject news5;
    [SerializeField] private GameObject exxonMobile;
    [SerializeField] private GameObject gazprom;
    [SerializeField] private GameObject ozon;
    [SerializeField] private GameObject lenta;
    [SerializeField] private GameObject polus;
    [SerializeField] private GameObject firstsolar;
    [SerializeField] private GameObject notEnoughMoneyPanel;
    [SerializeField] private GameObject operationErrorPanel;
    [SerializeField] private GameObject operationSuccessPanel;
    [SerializeField] private Text rublesT;
    [SerializeField] private Text dollarsT;
    [SerializeField] private Text dateT;
    private DateTime date;
    private readonly List<GameObject> _news = new List<GameObject>();
    private readonly List<TypeNews> _types = new List<TypeNews>();
    private readonly Dictionary<TypeNews, List<int>> _usedNews = new Dictionary<TypeNews, List<int>>();
    public static readonly Dictionary<CompanyNames, Company> companies = new Dictionary<CompanyNames, Company>();
    public static double Rubles { get; set; } = 100000;
    public static double Dollars { get; set; } = 1000;
    public static int stocksPurchased { get; private set; }
    [SerializeField] private List<InputField> counts = new List<InputField>();
    public static int countSteps { get; set; }
    public static bool IsGameOver { get; private set; }
    [SerializeField] private List<Text> gazpromT;
    [SerializeField] private List<Text> exxonT;
    [SerializeField] private List<Text> poluysT;

    public void Start()
    {
        date = new DateTime(2022, 1, 1);
        if (_news.Count == 0)
        {
            SetNewsArray();
            SetUsedNewsDict();
            GetNewspaper();
        }
        if(companies.Count == 0)
            SetCompaniesDict();
    }

    public void GetNewspaper()
    {
        var rnd = new System.Random();
        foreach (var item in _news) // Инициализируем тип, "влияние" и содержимое новости
        {
            // Инициализируем тип новости
            var type = GetNewsTypes(rnd);
            item.GetComponent<News>().Type = type;
            // Инициализируем влияние новости на динамику акции
            var index = GetNewsIndex(type, rnd);
            item.GetComponent<News>().Index = index;
            SetNewsInfluence(item, index);
            // Подгружаем содержимое новости
            var path = string.Format("{0}News/{1}", type.ToString(), index);
            item.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        }
    }

    private int GetNewsIndex(TypeNews type, System.Random rnd)
    {
        var index = rnd.Next(1, 13);
        while(_usedNews[type].Contains(index))
            index = rnd.Next(1, 13);
        _usedNews[type].Add(index);
        return index;
    }

    private void SetNewsInfluence(GameObject news, int index)
    {
        var newsComponent = news.GetComponent<News>();
        if (newsComponent.Type == TypeNews.Other)
        {
            newsComponent.Influence = 0;
            return;
        }
        switch (index)
        {
            case 1:
            case 2:
                newsComponent.Influence = 4;
                break;
            case 3:
            case 4:
                newsComponent.Influence = 9;
                break;
            case 5:
            case 6:
                newsComponent.Influence = 13;
                break;
            case 7:
            case 8:
                newsComponent.Influence = 0;
                break;
            case 9:
            case 10:
                newsComponent.Influence = -4;
                break;
            case 11:
                newsComponent.Influence = -9;
                break;
            case 12:
                newsComponent.Influence = -13;
                break;
        }
    }

    private TypeNews GetNewsTypes(System.Random rnd)
    {
        var type = (TypeNews)rnd.Next(7);
        while (_types.Contains(type))
            type = (TypeNews)rnd.Next(7);
        _types.Add(type);
        return type;
    }

    public void SetNewsArray()
    {
        if (_news.Count > 0)
            return;
        _news.Add(news1);
        _news.Add(news2);
        _news.Add(news3);
        _news.Add(news4);
        _news.Add(news5);
    }

    public void SetUsedNewsDict()
    {
        _usedNews.Add(TypeNews.Other, new List<int>());
        _usedNews.Add(TypeNews.Oil, new List<int>());
        _usedNews.Add(TypeNews.Gas, new List<int>());
        _usedNews.Add(TypeNews.OnlineStore, new List<int>());
        _usedNews.Add(TypeNews.ChainStores, new List<int>());
        _usedNews.Add(TypeNews.SolarEnergy, new List<int>());
        _usedNews.Add(TypeNews.Gold, new List<int>());
    }

    private void SetCompaniesDict()
    {
        companies[CompanyNames.ExxonMobile] = exxonMobile.GetComponent<Company>();
        companies[CompanyNames.Gazprom] = gazprom.GetComponent<Company>();
        companies[CompanyNames.Lenta] = lenta.GetComponent<Company>();
        companies[CompanyNames.Ozon] = ozon.GetComponent<Company>();
        companies[CompanyNames.Polus] = polus.GetComponent<Company>();
        companies[CompanyNames.FirstSolar] = firstsolar.GetComponent<Company>();
    }

    public void BuyStock()
    {
        var companyIndex = GameSceneUI.currentStock;
        var company = companies[(CompanyNames)companyIndex];
        var count = int.Parse(counts[companyIndex].text);
        if (company.IsPossibleToBuy(count))
        {
            company.BuyStock(count);
            stocksPurchased += count;
            operationSuccessPanel.SetActive(true);
            if (company.Currency == Currency.Rubles)
                rublesT.text = Rubles.ToString();
            if (company.Currency == Currency.Dollars)
                dollarsT.text = Dollars.ToString();
        }
        else
            notEnoughMoneyPanel.SetActive(true);
    }

    public void SellStock()
    {
        var companyIndex = GameSceneUI.currentStock;
        var company = companies[(CompanyNames)companyIndex];
        var count = int.Parse(counts[companyIndex].text);
        if (company.IsPossibleToSell(count))
        {
            company.SellStock(count);
            operationSuccessPanel.SetActive(true);
            if (company.Currency == Currency.Rubles)
                rublesT.text = Rubles.ToString();
            if (company.Currency == Currency.Dollars)
                dollarsT.text = Dollars.ToString();
        }
        else
            operationErrorPanel.SetActive(true);
    }

    public void CompleteMove()
    {
        // Завершаем игру если игрок сделал 12 ходов
        if (countSteps == 11)
        {
            IsGameOver = true;
            return;
        }
        date = date.AddMonths(3);
        dateT.text = date.ToString().Substring(0, 10);
        countSteps++;
        // Собираем "процент влияния" всех новостей в данном ходе
        var companyInfluences = _news.Select(news => news.GetComponent<News>().Influence).ToList();
        // Превращаем типы новостей в данном ходе в названия компаний
        var usedCompanies = _types.Select(types => News.ConvertTypeToCompany(types)).Select(name => companies[name]).ToList();
        // Пересчитываем статистику каждой компании
        for (int i = 0; i < usedCompanies.Count; i++)
        {
            if (companyInfluences[i] == 0) continue;
            usedCompanies[i].RecalculateCompanyStatistics(companyInfluences[i]);
        }
        // Обновляем газету новостей, производим выплату дивидендов и пересчитываем деньги
        _types.Clear();
        GetNewspaper();
        Dividends.PayoutDividends();
        RecalcMoney();
    }

    public void RecalcMoney()
    {
        rublesT.text = Math.Round(Rubles,2).ToString();
        dollarsT.text = Math.Round(Dollars,2).ToString();
        gazpromT[0].text = Dividends.gazpromPercents.ToString();
        gazpromT[1].text = string.Format("{0} ₽", Math.Round(Dividends.gazpromSum, 2).ToString());
        gazpromT[2].text = Dividends.gazpromPayouts.ToString();

        exxonT[0].text = Dividends.exxonPercents.ToString();
        exxonT[1].text = string.Format("{0} $", Math.Round(Dividends.exxonSum, 2).ToString());
        exxonT[2].text = Dividends.exxonPayouts.ToString();

        poluysT[0].text = Dividends.polyusPercents.ToString();
        poluysT[1].text = string.Format("{0} ₽", Math.Round(Dividends.poluysSum, 2).ToString());
        poluysT[2].text = Dividends.poluysPayouts.ToString();
    }
}
