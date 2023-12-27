using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public List<Player> _players;
    public static GameManager Instance => _instance;
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
                UiManager.Instance.WinnerNameText.text = player.PlayerName;
                player.GetComponent<PlayerMovement>().enabled = false;
            }

            UiManager.Instance.WinScreen.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }
    public void AddPlayer(Player player)
    {
        _players.Add(player);
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
            UiManager.Instance.FourthPlaceNameText.text = _fourthPlace.PlayerName;

        if (_thirdPlace != null)
            UiManager.Instance.ThirdPlaceNameText.text = _thirdPlace.PlayerName;

        if (_secondPlace != null)
            UiManager.Instance.SecondPlaceNameText.text = _secondPlace.PlayerName;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        _fourthPlace = null;
        _thirdPlace = null;
        _secondPlace = null;
        UiManager.Instance.WinScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        UiManager.Instance.PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void PauseMenu()
    {
        UiManager.Instance.PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
