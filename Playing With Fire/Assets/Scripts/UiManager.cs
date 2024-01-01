using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private static UiManager _instance;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _controlsScreen;
    [SerializeField] private TextMeshProUGUI _winnerNameText;
    [SerializeField] private TextMeshProUGUI _secondPlaceNameText;
    [SerializeField] private TextMeshProUGUI _thirdPlaceNameText;
    [SerializeField] private TextMeshProUGUI _fourthPlaceNameText;
    [SerializeField] private bool _showControlsInputs;
    #region Players text to input mapping
    [Header("Player 1")]
    [HideInInspector][SerializeField] private GameObject _p1Input;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p1NameText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p1UpText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p1DownText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p1LeftText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p1RightText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p1BombText;
    [HideInInspector][SerializeField] private Color _p1Color;
    [Header("Player 2")]
    [HideInInspector][SerializeField] private GameObject _p2Input;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p2NameText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p2UpText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p2DownText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p2LeftText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p2RightText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p2BombText;
    [HideInInspector][SerializeField] private Color _p2Color;
    [Header("Player 3")]
    [HideInInspector][SerializeField] private GameObject _p3Input;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p3NameText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p3UpText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p3DownText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p3LeftText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p3RightText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p3BombText;
    [HideInInspector][SerializeField] private Color _p3Color;
    [Header("Player 4")]
    [HideInInspector][SerializeField] private GameObject _p4Input;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p4NameText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p4UpText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p4DownText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p4LeftText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p4RightText;
    [HideInInspector][SerializeField] private TextMeshProUGUI _p4BombText;
    [HideInInspector][SerializeField] private Color _p4Color;
    #endregion
    #region Players text Getters
    public GameObject p1Input => _p1Input;
    public TextMeshProUGUI p1NameText => _p1NameText;
    public TextMeshProUGUI p1UpText => _p1UpText;
    public TextMeshProUGUI p1DownText => _p1DownText;
    public TextMeshProUGUI p1LeftText => _p1LeftText;
    public TextMeshProUGUI p1RightText => _p1RightText;
    public TextMeshProUGUI p1BombText => _p1BombText;
    public Color p1Color => _p1Color;
    public GameObject p2Input => _p2Input;
    public TextMeshProUGUI p2NameText => _p2NameText;
    public TextMeshProUGUI p2UpText => _p2UpText;
    public TextMeshProUGUI p2DownText => _p2DownText;
    public TextMeshProUGUI p2LeftText => _p2LeftText;
    public TextMeshProUGUI p2RightText => _p2RightText;
    public TextMeshProUGUI p2BombText => _p2BombText;
    public Color p2Color => _p2Color;
    public GameObject p3Input => _p3Input;
    public TextMeshProUGUI p3NameText => _p3NameText;
    public TextMeshProUGUI p3UpText => _p3UpText;
    public TextMeshProUGUI p3DownText => _p3DownText;
    public TextMeshProUGUI p3LeftText => _p3LeftText;
    public TextMeshProUGUI p3RightText => _p3RightText;
    public TextMeshProUGUI p3BombText => _p3BombText;
    public Color p3Color => _p3Color;
    public GameObject p4Input => _p4Input;
    public TextMeshProUGUI p4NameText => _p4NameText;
    public TextMeshProUGUI p4UpText => _p4UpText;
    public TextMeshProUGUI p4DownText => _p4DownText;
    public TextMeshProUGUI p4LeftText => _p4LeftText;
    public TextMeshProUGUI p4RightText => _p4RightText;
    public TextMeshProUGUI p4BombText => _p4BombText;
    public Color p4Color => _p4Color;
    #endregion  

    public static UiManager instance => _instance;
    public GameObject winScreen => _winScreen;
    public GameObject pauseMenu => _pauseMenu;
    public GameObject controlsScreen => _controlsScreen;
    public TextMeshProUGUI winnerNameText => _winnerNameText;
    public TextMeshProUGUI secondPlaceNameText => _secondPlaceNameText;
    public TextMeshProUGUI thirdPlaceNameText => _thirdPlaceNameText;
    public TextMeshProUGUI fourthPlaceNameText => _fourthPlaceNameText;
    public bool showControlsInputs => _showControlsInputs;
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

[CustomEditor(typeof(UiManager))]
public class UiManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UiManager uiManager = (UiManager)target;
        if (uiManager.showControlsInputs)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1Input"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1NameText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1UpText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1DownText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1LeftText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1RightText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1BombText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1Color"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2Input"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2NameText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2UpText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2DownText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2LeftText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2RightText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2BombText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2Color"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3Input"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3NameText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3UpText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3DownText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3LeftText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3RightText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3BombText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3Color"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4Input"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4NameText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4UpText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4DownText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4LeftText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4RightText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4BombText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4Color"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
