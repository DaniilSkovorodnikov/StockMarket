using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject rulesPanel;
    [SerializeField] private GameObject exitPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RulesPanel()
    {
        rulesPanel.SetActive(true);   
    }

    public void CloseRulesPanel()
    {
        rulesPanel.SetActive(false);
    }

    public void ExitPanel()
    {
        exitPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseExitPanel()
    {
        exitPanel.SetActive(false);
    }
}
