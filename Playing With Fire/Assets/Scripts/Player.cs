using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum PlayerColor
{
    White,
    Blue,
    Red
}
public class Player : MonoBehaviour
{
    [SerializeField] string _playerName;
    [SerializeField] PlayerColor _playerColor;
    [SerializeField] GameObject[] _colors;
    private PlayerMovement _ghostPrefab;
    public PlayerMovement GhostPrefab => _ghostPrefab;
    private PlayerMovement _playerMovement;

    void OnEnable()
    {

    }
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        SetPlayerColor();
        _playerMovement.GetRenderers();
    }

    public void SetPlayerColor()
    {
        switch (_playerColor)
        {
            case PlayerColor.White:
                _colors[0].SetActive(true);
                _colors[1].SetActive(false);
                _colors[2].SetActive(false);
                _ghostPrefab = Resources.Load<PlayerMovement>("White Died Player");
                break;
            case PlayerColor.Blue:
                _colors[1].SetActive(true);
                _colors[0].SetActive(false);
                _colors[2].SetActive(false);
                _ghostPrefab = Resources.Load<PlayerMovement>("Blue Died Player");
                break;
            case PlayerColor.Red:
                _colors[2].SetActive(true);
                _colors[0].SetActive(false);
                _colors[1].SetActive(false);
                _ghostPrefab = Resources.Load<PlayerMovement>("Red Died Player");
                break;
            default:
                break;
        }
    }

}
