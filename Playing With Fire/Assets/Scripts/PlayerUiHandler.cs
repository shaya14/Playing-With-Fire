using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerUiHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerLives;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _numOfBombsText;
    [SerializeField] private TextMeshProUGUI _numOfSpeedBoostsText;
    [SerializeField] private TextMeshProUGUI _numOfRadiusBoostsText;
    [SerializeField] private TextMeshProUGUI _keysText;
    public TextMeshProUGUI keytext => _keysText;

    public void DecreceLives()
    {
        for (int i = 0; i < _playerLives.Length; i++)
        {
            if (_playerLives[i].gameObject.activeSelf)
            {
                _playerLives[i].gameObject.SetActive(false);
                break;
            }
        }
    }

    public void UpdateLives(int num)
    {
        for (int i = 0; i < _playerLives.Length; i++)
        {
            if (i < num)
            {
                _playerLives[i].gameObject.SetActive(true);
            }
            else
            {
                _playerLives[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateNameText(string name)
    {
        _nameText.text = name;
    }

    public void UpdateNumOfBombsText(int numOfBombs)
    {
        _numOfBombsText.text = numOfBombs.ToString();
    }

    public void UpdateNumOfSpeedBoostsText(int numOfSpeedBoosts)
    {
        _numOfSpeedBoostsText.text = numOfSpeedBoosts.ToString();
    }

    public void UpdateNumOfRadiusBoostsText(int numOfRadiusBoosts)
    {
        _numOfRadiusBoostsText.text = numOfRadiusBoosts.ToString();
    }

    public void UpdateKeysText(string keys)
    {
        _keysText.text = keys;
    }
}
