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

    [SerializeField] AnimatedSpriteRenderer _spriteRendererUp;
    [SerializeField] AnimatedSpriteRenderer _spriteRendererDown;
    [SerializeField] AnimatedSpriteRenderer _spriteRendererLeft;
    [SerializeField] AnimatedSpriteRenderer _spriteRendererRight;
    private AnimatedSpriteRenderer _activeSpriteRenderer;

    #region Keys
    [HideInInspector] public KeyCode _upKey;
    [HideInInspector] public KeyCode _downKey;
    [HideInInspector] public KeyCode _leftKey;
    [HideInInspector] public KeyCode _rightKey;
    [HideInInspector] public KeyCode _bombKey;
    #endregion

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _activeSpriteRenderer = _spriteRendererDown;
    }

    void FixedUpdate()
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
                    MoveCalculation(Vector3.up, _moveSpeed, transform.position, _rb, _spriteRendererUp);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    //Move Down
                    MoveCalculation(Vector3.down, _moveSpeed, transform.position, _rb, _spriteRendererDown);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    //Move Left
                    MoveCalculation(Vector3.left, _moveSpeed, transform.position, _rb, _spriteRendererLeft);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    //Move Right
                    MoveCalculation(Vector3.right, _moveSpeed, transform.position, _rb, _spriteRendererRight);
                }
                break;
            case PlayerInput.ArrowKeys:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //Move Up
                    MoveCalculation(Vector3.up, _moveSpeed, transform.position, _rb, _spriteRendererUp);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    //Move Down
                    MoveCalculation(Vector3.down, _moveSpeed, transform.position, _rb, _spriteRendererDown);
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //Move Left
                    MoveCalculation(Vector3.left, _moveSpeed, transform.position, _rb, _spriteRendererLeft);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    //Move Right
                    MoveCalculation(Vector3.right, _moveSpeed, transform.position, _rb, _spriteRendererRight);
                }
                break;
            case PlayerInput.Custom:
                if (Input.GetKey(_upKey))
                {
                    //Move Up
                    MoveCalculation(Vector3.up, _moveSpeed, transform.position, _rb, _spriteRendererUp);
                }
                else if (Input.GetKey(_downKey))
                {
                    //Move Down
                    MoveCalculation(Vector3.down, _moveSpeed, transform.position, _rb, _spriteRendererDown);
                }
                else if (Input.GetKey(_leftKey))
                {
                    //Move Left
                    MoveCalculation(Vector3.left, _moveSpeed, transform.position, _rb, _spriteRendererLeft);
                }
                else if (Input.GetKey(_rightKey))
                {
                    //Move Right
                    MoveCalculation(Vector3.right, _moveSpeed, transform.position, _rb, _spriteRendererRight);
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
    private void MoveCalculation(Vector3 direction, float moveSpeed, Vector3 position, Rigidbody2D rb, AnimatedSpriteRenderer spriteRenderer)
    {
        Vector3 dir = direction * moveSpeed * Time.fixedDeltaTime;
        position = transform.position;
        Vector3 movement = position + dir;
        rb.MovePosition(movement);

        _spriteRendererUp.enabled = spriteRenderer == _spriteRendererUp;
        _spriteRendererDown.enabled = spriteRenderer == _spriteRendererDown;
        _spriteRendererLeft.enabled = spriteRenderer == _spriteRendererLeft;
        _spriteRendererRight.enabled = spriteRenderer == _spriteRendererRight;

        _activeSpriteRenderer = spriteRenderer;
        _activeSpriteRenderer._idle = direction == Vector3.zero;
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
