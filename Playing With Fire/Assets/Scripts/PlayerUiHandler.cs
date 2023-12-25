using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using TMPro;

public class PlayerUiHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerLives;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _numOfBombsText;
    [SerializeField] private TextMeshProUGUI _numOfSpeedBoostsText;
    [SerializeField] private TextMeshProUGUI _numOfRadiusBoostsText;

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
}
