using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// CR: fix exit buttons
// CR: fix leaderboards bug

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager _instance;
    public static MainMenuManager instance => _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void NewGamePanel()
    {
        MainMenuUI.instance.newGamePanel.SetActive(true);
        MainMenuUI.instance.mainMenuPanel.SetActive(false);
    }

    public void StartNewGame()
    {
        // CR: [style] never have if/else/for/... without braces. 
        if(MainMenuUI.instance.dropdown.value == 0)
            Debug.Log("Please select number of players");
        else
            SceneManager.LoadScene("Game");
    }

    public void GetValueFromDropdown()
    {   
        switch (MainMenuUI.instance.dropdown.value)
        {
            case 1:
                GameSettings.instance.SetNumOfPlayers(2);
                break;
            case 2:
                GameSettings.instance.SetNumOfPlayers(3);
                break;
            case 3:
                GameSettings.instance.SetNumOfPlayers(4);
                break;
        }

    }
}
