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
    [SerializeField] private TextMeshProUGUI _winnerNameText;
    [SerializeField] private TextMeshProUGUI _secondPlaceNameText;
    [SerializeField] private TextMeshProUGUI _thirdPlaceNameText;
    [SerializeField] private TextMeshProUGUI _fourthPlaceNameText;

    public static UiManager Instance => _instance;
    public GameObject WinScreen => _winScreen;  
    public TextMeshProUGUI WinnerNameText => _winnerNameText;
    public TextMeshProUGUI SecondPlaceNameText => _secondPlaceNameText;
    public TextMeshProUGUI ThirdPlaceNameText => _thirdPlaceNameText;
    public TextMeshProUGUI FourthPlaceNameText => _fourthPlaceNameText;
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
