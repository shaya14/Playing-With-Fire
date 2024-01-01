using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum PlayerColor
{
    White,
    Blue,
    Red,
    Black
}
public class Player : MonoBehaviour
{
    [SerializeField] string _playerName;
    [SerializeField] PlayerColor _playerColor;
    [SerializeField] private PlayerMovement _ghostPrefab;
    [SerializeField] GameObject[] _colors;
    private PlayerMovement _playerMovement;
    private PlayerUiHandler _playerUiHandler;
    public PlayerMovement GhostPrefab => _ghostPrefab;
    public string PlayerName => _playerName;

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerUiHandler = GetComponent<PlayerUiHandler>();
        SetPlayerColor();
        _playerMovement.GetRenderers();
    }

    void Start()
    {
        _playerUiHandler.UpdateNameText(_playerName);
        GameManager.instance.AddPlayer(this);
    }

    void OnDisable()
    {
        GameManager.instance.RemovePlayer(this);
    }

    public void SetPlayerColor()
    {
        switch (_playerColor)
        {
            case PlayerColor.White:
                _colors[0].SetActive(true);
                _colors[1].SetActive(false);
                _colors[2].SetActive(false);
                _colors[3].SetActive(false);
                _ghostPrefab = Resources.Load<PlayerMovement>("White Died Player");
                break;
            case PlayerColor.Blue:
                _colors[1].SetActive(true);
                _colors[0].SetActive(false);
                _colors[2].SetActive(false);
                _colors[3].SetActive(false);
                _ghostPrefab = Resources.Load<PlayerMovement>("Blue Died Player");
                break;
            case PlayerColor.Red:
                _colors[2].SetActive(true);
                _colors[0].SetActive(false);
                _colors[1].SetActive(false);
                _colors[3].SetActive(false);
                _ghostPrefab = Resources.Load<PlayerMovement>("Red Died Player");
                break;
            case PlayerColor.Black:
                _colors[3].SetActive(true);
                _colors[0].SetActive(false);
                _colors[1].SetActive(false);
                _colors[2].SetActive(false);
                _ghostPrefab = Resources.Load<PlayerMovement>("Black");
                break;
            default:
                break;
        }
    }
}
