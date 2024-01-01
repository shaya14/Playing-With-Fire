using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private static MainMenuUI _instance;
    public static MainMenuUI instance => _instance;

    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _newGamePanel;
    [SerializeField] private TMP_Dropdown _dropdown;

    public GameObject mainMenuPanel => _mainMenuPanel;
    public GameObject newGamePanel => _newGamePanel;
    public TMP_Dropdown dropdown => _dropdown;

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
