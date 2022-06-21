using UnityEngine;
using System.Collections.Generic;

public class Browser : MonoBehaviour
{
    private GameObject currentPanel;
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private GameObject laptopPanel;

    public void OpenLaptopPanel()
    {
        laptopPanel.SetActive(true);
        currentPanel = panels[0];
        currentPanel.SetActive(true);
    }

    public void CloseLaptopPanel()
    {
        gameObject.SetActive(false);
        currentPanel.SetActive(false);
    }

    public void OpenBuyStocksWindow()
    {
        if(currentPanel != panels[1])
        {
            panels[1].SetActive(true);
            currentPanel.SetActive(false);
            currentPanel = panels[1];
        }
    }

    public void OpenPortfolioWindow()
    {
        if(currentPanel != panels[0])
        {
            panels[0].SetActive(true);
            currentPanel.SetActive(false);
            currentPanel = panels[0];
        }
    }

    public void OpenConverterWindow()
    {
        if(currentPanel != panels[2])
        {
            panels[2].SetActive(true);
            currentPanel.SetActive(false);
            currentPanel = panels[2];
        }
    }

    public void OpenDividendsWindow()
    {
        if (currentPanel != panels[3])
        {
            panels[3].SetActive(true);
            currentPanel.SetActive(false);
            currentPanel = panels[3];
        }
    }

    public void OpenBankWindow()
    {
        if (currentPanel != panels[4])
        {
            panels[4].SetActive(true);
            currentPanel.SetActive(false);
            currentPanel = panels[4];
        }
    }
}
