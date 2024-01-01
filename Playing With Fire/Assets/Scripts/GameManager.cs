using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance => _instance;
    private List<Player> _players;
    private Player _fourthPlace;
    private Player _thirdPlace;
    private Player _secondPlace;

    void Awake()
    {
        _players = new List<Player>();
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        SetLeaderBoardText();
        if (_players.Count <= 1)
        {
            foreach (var player in _players)
            {
                UiManager.instance.winnerNameText.text = player.playerName;
                player.GetComponent<PlayerMovement>().enabled = false;
            }

            UiManager.instance.winScreen.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }
    public void AddPlayer(Player player,int playerNumber)
    {
        _players.Add(player);
        player.SetPlayerNumber(playerNumber);
        Debug.Log("Player added " + player.playerNumber);
    }
    public void RemovePlayer(Player player)
    {
        if (_players.Count == 4)
        {
            _fourthPlace = player;
        }
        else if (_players.Count == 3)
        {
            _thirdPlace = player;
        }
        else if (_players.Count == 2)
        {
            _secondPlace = player;
        }

        _players.Remove(player);
    }

    private void SetLeaderBoardText()
    {
        if (_fourthPlace != null)
            UiManager.instance.fourthPlaceNameText.text = _fourthPlace.playerName;

        if (_thirdPlace != null)
            UiManager.instance.thirdPlaceNameText.text = _thirdPlace.playerName;

        if (_secondPlace != null)
            UiManager.instance.secondPlaceNameText.text = _secondPlace.playerName;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        _fourthPlace = null;
        _thirdPlace = null;
        _secondPlace = null;
        UiManager.instance.winScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        UiManager.instance.pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToPauseMenu()
    {
        UiManager.instance.controlsScreen.SetActive(false);
        UiManager.instance.pauseMenu.SetActive(true);
    }

    public void ControlsMenu()
    {
        UiManager.instance.controlsScreen.SetActive(true);
        UiManager.instance.pauseMenu.SetActive(false);
    }

    private void PauseMenu()
    {
        UiManager.instance.pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void UpdateInputMappingText(Player player,string up, string down, string right, string left, KeyCode bombKey)
    {
        if(player.playerNumber == 1)
        {
            UiManager.instance.p1NameText.text = player.playerName;
            UiManager.instance.p1UpText.text = up;
            UiManager.instance.p1DownText.text = down;
            UiManager.instance.p1RightText.text = right;
            UiManager.instance.p1LeftText.text = left;
            UiManager.instance.p1BombText.text = bombKey.ToString();
            UiManager.instance.p1NameText.color = UiManager.instance.p1Color;
            UiManager.instance.p1UpText.color = UiManager.instance.p1Color;
            UiManager.instance.p1DownText.color = UiManager.instance.p1Color;
            UiManager.instance.p1RightText.color = UiManager.instance.p1Color;
            UiManager.instance.p1LeftText.color = UiManager.instance.p1Color;
            UiManager.instance.p1BombText.color = UiManager.instance.p1Color;
            UiManager.instance.p1Input.SetActive(true);
        }
        else if (player.playerNumber == 2)
        {
            UiManager.instance.p2NameText.text = player.playerName;
            UiManager.instance.p2UpText.text = up;
            UiManager.instance.p2DownText.text = down;
            UiManager.instance.p2RightText.text = right;
            UiManager.instance.p2LeftText.text = left;
            UiManager.instance.p2BombText.text = bombKey.ToString();
            UiManager.instance.p2NameText.color = UiManager.instance.p2Color;
            UiManager.instance.p2UpText.color = UiManager.instance.p2Color;
            UiManager.instance.p2DownText.color = UiManager.instance.p2Color;
            UiManager.instance.p2RightText.color = UiManager.instance.p2Color;
            UiManager.instance.p2LeftText.color = UiManager.instance.p2Color;
            UiManager.instance.p2BombText.color = UiManager.instance.p2Color;
            UiManager.instance.p2Input.SetActive(true);
        }
        else if (player.playerNumber == 3)
        {
            UiManager.instance.p3NameText.text = player.playerName;
            UiManager.instance.p3UpText.text = up;
            UiManager.instance.p3DownText.text = down;
            UiManager.instance.p3RightText.text = right;
            UiManager.instance.p3LeftText.text = left;
            UiManager.instance.p3BombText.text = bombKey.ToString();
            UiManager.instance.p3NameText.color = UiManager.instance.p3Color;
            UiManager.instance.p3UpText.color = UiManager.instance.p3Color;
            UiManager.instance.p3DownText.color = UiManager.instance.p3Color;
            UiManager.instance.p3RightText.color = UiManager.instance.p3Color;
            UiManager.instance.p3LeftText.color = UiManager.instance.p3Color;
            UiManager.instance.p3BombText.color = UiManager.instance.p3Color;
            UiManager.instance.p3Input.SetActive(true);
        }
        else if (player.playerNumber == 4)
        {
            UiManager.instance.p4NameText.text = player.playerName;
            UiManager.instance.p4UpText.text = up;
            UiManager.instance.p4DownText.text = down;
            UiManager.instance.p4RightText.text = right;
            UiManager.instance.p4LeftText.text = left;
            UiManager.instance.p4BombText.text = bombKey.ToString();
            UiManager.instance.p4NameText.color = UiManager.instance.p4Color;
            UiManager.instance.p4UpText.color = UiManager.instance.p4Color;
            UiManager.instance.p4DownText.color = UiManager.instance.p4Color;
            UiManager.instance.p4RightText.color = UiManager.instance.p4Color;
            UiManager.instance.p4LeftText.color = UiManager.instance.p4Color;
            UiManager.instance.p4BombText.color = UiManager.instance.p4Color;
            UiManager.instance.p4Input.SetActive(true);
        }
    }
}
