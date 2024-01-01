using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private static UiManager _instance;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private TextMeshProUGUI _winnerNameText;
    [SerializeField] private TextMeshProUGUI _secondPlaceNameText;
    [SerializeField] private TextMeshProUGUI _thirdPlaceNameText;
    [SerializeField] private TextMeshProUGUI _fourthPlaceNameText;

    public static UiManager instance => _instance;
    public GameObject winScreen => _winScreen;
    public GameObject pauseMenu => _pauseMenu;
    public TextMeshProUGUI winnerNameText => _winnerNameText;
    public TextMeshProUGUI secondPlaceNameText => _secondPlaceNameText;
    public TextMeshProUGUI thirdPlaceNameText => _thirdPlaceNameText;
    public TextMeshProUGUI fourthPlaceNameText => _fourthPlaceNameText;
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
}
