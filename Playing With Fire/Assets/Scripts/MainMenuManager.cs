using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager _instance;
    public static MainMenuManager instance => _instance;
    private int _numberOfPlayers;

    public int numberOfPlayers => _numberOfPlayers;

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
        DontDestroyOnLoad(gameObject);
    }
    
    public void NewGamePanel()
    {
        MainMenuUI.instance.newGamePanel.SetActive(true);
        MainMenuUI.instance.mainMenuPanel.SetActive(false);
    }

    public void StartNewGame()
    {
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
                _numberOfPlayers = 2;
                break;
            case 2:
                _numberOfPlayers = 3;
                break;
            case 3:
                _numberOfPlayers = 4;
                break;
        }

    }
}
