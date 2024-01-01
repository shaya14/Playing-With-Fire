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
    [SerializeField] private List<Player> _startingPlayers;
    private List<Player> _activePlayers;
    private Player _fourthPlace;
    private Player _thirdPlace;
    private Player _secondPlace;

    void Awake()
    {
        InitialGame();

        _activePlayers = new List<Player>();
        _startingPlayers = new List<Player>();
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
    }

    void Update()
    {
        SetLeaderBoardText();
        if (_activePlayers.Count <= 1)
        {
            foreach (var player in _activePlayers)
            {
                GameUI.instance.winnerNameText.text = player.playerName;
                player.GetComponent<PlayerMovement>().enabled = false;
            }

            GameUI.instance.winScreen.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }
    public void AddPlayer(Player player, int playerNumber)
    {
        _activePlayers.Add(player);
        player.SetPlayerNumber(playerNumber);
    }

    private void InitialGame()
    {

            for (int i = 0; i < MainMenuManager.instance.numberOfPlayers; i++)
            {
                _startingPlayers[i].gameObject.SetActive(true);
            }
        
    }
    public void RemovePlayer(Player player)
    {
        if (_activePlayers.Count == 4)
        {
            _fourthPlace = player;
        }
        else if (_activePlayers.Count == 3)
        {
            _thirdPlace = player;
        }
        else if (_activePlayers.Count == 2)
        {
            _secondPlace = player;
        }

        _activePlayers.Remove(player);
    }

    private void SetLeaderBoardText()
    {
        if (_fourthPlace != null)
            GameUI.instance.fourthPlaceNameText.text = _fourthPlace.playerName;

        if (_thirdPlace != null)
            GameUI.instance.thirdPlaceNameText.text = _thirdPlace.playerName;

        if (_secondPlace != null)
            GameUI.instance.secondPlaceNameText.text = _secondPlace.playerName;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        _fourthPlace = null;
        _thirdPlace = null;
        _secondPlace = null;
        GameUI.instance.winScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        GameUI.instance.pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToPauseMenu()
    {
        GameUI.instance.controlsScreen.SetActive(false);
        GameUI.instance.pauseMenu.SetActive(true);
    }

    public void ControlsMenu()
    {
        GameUI.instance.controlsScreen.SetActive(true);
        GameUI.instance.pauseMenu.SetActive(false);
    }

    private void PauseMenu()
    {
        GameUI.instance.pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void UpdateInputMappingText(Player player, string up, string down, string right, string left, KeyCode bombKey)
    {
        if (player.playerNumber == 1)
        {
            GameUI.instance.p1NameText.text = player.playerName;
            GameUI.instance.p1UpText.text = up;
            GameUI.instance.p1DownText.text = down;
            GameUI.instance.p1RightText.text = right;
            GameUI.instance.p1LeftText.text = left;
            GameUI.instance.p1BombText.text = bombKey.ToString();
            GameUI.instance.p1NameText.color = GameUI.instance.p1Color;
            GameUI.instance.p1UpText.color = GameUI.instance.p1Color;
            GameUI.instance.p1DownText.color = GameUI.instance.p1Color;
            GameUI.instance.p1RightText.color = GameUI.instance.p1Color;
            GameUI.instance.p1LeftText.color = GameUI.instance.p1Color;
            GameUI.instance.p1BombText.color = GameUI.instance.p1Color;
            GameUI.instance.p1Input.SetActive(true);
        }
        else if (player.playerNumber == 2)
        {
            GameUI.instance.p2NameText.text = player.playerName;
            GameUI.instance.p2UpText.text = up;
            GameUI.instance.p2DownText.text = down;
            GameUI.instance.p2RightText.text = right;
            GameUI.instance.p2LeftText.text = left;
            GameUI.instance.p2BombText.text = bombKey.ToString();
            GameUI.instance.p2NameText.color = GameUI.instance.p2Color;
            GameUI.instance.p2UpText.color = GameUI.instance.p2Color;
            GameUI.instance.p2DownText.color = GameUI.instance.p2Color;
            GameUI.instance.p2RightText.color = GameUI.instance.p2Color;
            GameUI.instance.p2LeftText.color = GameUI.instance.p2Color;
            GameUI.instance.p2BombText.color = GameUI.instance.p2Color;
            GameUI.instance.p2Input.SetActive(true);
        }
        else if (player.playerNumber == 3)
        {
            GameUI.instance.p3NameText.text = player.playerName;
            GameUI.instance.p3UpText.text = up;
            GameUI.instance.p3DownText.text = down;
            GameUI.instance.p3RightText.text = right;
            GameUI.instance.p3LeftText.text = left;
            GameUI.instance.p3BombText.text = bombKey.ToString();
            GameUI.instance.p3NameText.color = GameUI.instance.p3Color;
            GameUI.instance.p3UpText.color = GameUI.instance.p3Color;
            GameUI.instance.p3DownText.color = GameUI.instance.p3Color;
            GameUI.instance.p3RightText.color = GameUI.instance.p3Color;
            GameUI.instance.p3LeftText.color = GameUI.instance.p3Color;
            GameUI.instance.p3BombText.color = GameUI.instance.p3Color;
            GameUI.instance.p3Input.SetActive(true);
        }
        else if (player.playerNumber == 4)
        {
            GameUI.instance.p4NameText.text = player.playerName;
            GameUI.instance.p4UpText.text = up;
            GameUI.instance.p4DownText.text = down;
            GameUI.instance.p4RightText.text = right;
            GameUI.instance.p4LeftText.text = left;
            GameUI.instance.p4BombText.text = bombKey.ToString();
            GameUI.instance.p4NameText.color = GameUI.instance.p4Color;
            GameUI.instance.p4UpText.color = GameUI.instance.p4Color;
            GameUI.instance.p4DownText.color = GameUI.instance.p4Color;
            GameUI.instance.p4RightText.color = GameUI.instance.p4Color;
            GameUI.instance.p4LeftText.color = GameUI.instance.p4Color;
            GameUI.instance.p4BombText.color = GameUI.instance.p4Color;
            GameUI.instance.p4Input.SetActive(true);
        }
    }
}
