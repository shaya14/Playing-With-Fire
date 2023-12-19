using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum PlayerInput
{
    WASD,
    ArrowKeys,
    Custom
}
public class PlayerController : MonoBehaviour
{
    public PlayerInput _playerInput;
    [SerializeField] string _playerName;
    [SerializeField] float _moveSpeed;
    [SerializeField] Bomb _bombPrefab;
    Rigidbody2D _rb;
    [HideInInspector] public KeyCode _upKey;
    [HideInInspector]  public KeyCode _downKey;
    [HideInInspector]  public KeyCode _leftKey;
    [HideInInspector]  public KeyCode _rightKey;
    [HideInInspector]  public KeyCode _bombKey;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SpawnBomb();
    }

    void Movement()
    {
        switch (_playerInput)
        {
            case PlayerInput.WASD:
                if (Input.GetKey(KeyCode.W))
                {
                    //Move Up
                    Vector3 direction = Vector3.up * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    //Move Left
                    Vector3 direction = Vector3.left * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    //Move Down
                    Vector3 direction = Vector3.down * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    //Move Right
                    Vector3 direction = Vector3.right * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                break;
            case PlayerInput.ArrowKeys:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //Move Up
                    Vector3 direction = Vector3.up * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //Move Left
                    Vector3 direction = Vector3.left * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    //Move Down
                    Vector3 direction = Vector3.down * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    //Move Right
                    Vector3 direction = Vector3.right * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                break;
            case PlayerInput.Custom:
                if (Input.GetKey(_upKey))
                {
                    //Move Up
                    Vector3 direction = Vector3.up * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(_leftKey))
                {
                    //Move Left
                    Vector3 direction = Vector3.left * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(_downKey))
                {
                    //Move Down
                    Vector3 direction = Vector3.down * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(_rightKey))
                {
                    //Move Right
                    Vector3 direction = Vector3.right * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                break;
            default:
                break;
        }
    }

    void SpawnBomb()
    {
        switch (_playerInput)
        {
            case PlayerInput.WASD:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Spawn bomb
                    var bomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
                }
                break;
            case PlayerInput.ArrowKeys:
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    //Spawn bomb
                    var bomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
                }
                break;
            case PlayerInput.Custom:
                if (Input.GetKeyDown(_bombKey))
                {
                    //Spawn bomb
                    var bomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
                }
                break;
            default:
                break;
        }
        //Spawn bomb
    }
}

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlayerController playerController = (PlayerController)target;

        if (playerController._playerInput == PlayerInput.Custom)
        {
            EditorGUILayout.Space(); // Add some space to separate custom input fields from the rest

            EditorGUI.BeginChangeCheck();

            playerController._upKey = (KeyCode)EditorGUILayout.EnumPopup("Up Key", playerController._upKey);
            playerController._downKey = (KeyCode)EditorGUILayout.EnumPopup("Down Key", playerController._downKey);
            playerController._leftKey = (KeyCode)EditorGUILayout.EnumPopup("Left Key", playerController._leftKey);
            playerController._rightKey = (KeyCode)EditorGUILayout.EnumPopup("Right Key", playerController._rightKey);
            playerController._bombKey = (KeyCode)EditorGUILayout.EnumPopup("Bomb Key", playerController._bombKey);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(playerController);
            }
        }
    }
}
