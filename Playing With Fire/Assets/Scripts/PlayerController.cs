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

// CR: no need for 'filler' words like 'Controller'. Just name the class 'Player'.
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _moveSpeed;
    
    private Rigidbody2D _rigidBody;
    private BombController _bombController;

    #region SpriteRenderers
    [SerializeField] private AnimatedSpriteRenderer _spriteRendererUp;
    [SerializeField] private AnimatedSpriteRenderer _spriteRendererDown;
    [SerializeField] private AnimatedSpriteRenderer _spriteRendererLeft;
    [SerializeField] private AnimatedSpriteRenderer _spriteRendererRight;
    private AnimatedSpriteRenderer _activeSpriteRenderer;
    #endregion

    #region Keys
    [HideInInspector] public KeyCode upKey;
    [HideInInspector] public KeyCode downKey;
    [HideInInspector] public KeyCode leftKey;
    [HideInInspector] public KeyCode rightKey;
    [HideInInspector] public KeyCode bombKey;
    #endregion

    public PlayerInput playerInput => _playerInput;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _bombController = GetComponent<BombController>();
        _activeSpriteRenderer = _spriteRendererDown;
    }

    void FixedUpdate()
    {
        Movement();

    }

    void Update()
    {
        SpawnBomb();
    }

    void Movement()
    {
        switch (_playerInput)
        {
            case PlayerInput.WASD:
                if (Input.GetKey(KeyCode.W))
                {
                    MoveCalculation(Vector3.up, _spriteRendererUp);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    MoveCalculation(Vector3.down, _spriteRendererDown);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    MoveCalculation(Vector3.left, _spriteRendererLeft);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    MoveCalculation(Vector3.right, _spriteRendererRight);
                }
                else if (Input.anyKey == false)
                {
                    MoveCalculation(Vector3.zero, _moveSpeed, transform.position, _rb, _activeSpriteRenderer);
                }
                break;
            case PlayerInput.ArrowKeys:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    MoveCalculation(Vector3.up, _spriteRendererUp);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    MoveCalculation(Vector3.down, _spriteRendererDown);
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    MoveCalculation(Vector3.left, _spriteRendererLeft);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    MoveCalculation(Vector3.right, _spriteRendererRight);
                }
                else if (Input.anyKey == false)
                {
                    MoveCalculation(Vector3.zero, _moveSpeed, transform.position, _rb, _activeSpriteRenderer);
                }
                break;
            case PlayerInput.Custom:
                if (Input.GetKey(upKey))
                {
                    MoveCalculation(Vector3.up, _spriteRendererUp);
                }
                else if (Input.GetKey(downKey))
                {
                    MoveCalculation(Vector3.down, _spriteRendererDown);
                }
                else if (Input.GetKey(leftKey))
                {
                    MoveCalculation(Vector3.left, _spriteRendererLeft);
                }
                else if (Input.GetKey(rightKey))
                {
                    MoveCalculation(Vector3.right, _spriteRendererRight);
                }
                else if (Input.anyKey == false)
                {
                    MoveCalculation(Vector3.zero, _moveSpeed, transform.position, _rb, _activeSpriteRenderer);
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
                    _bombController.PlaceBomb();
                }
                break;
            case PlayerInput.ArrowKeys:
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    _bombController.PlaceBomb();
                }
                break;
            case PlayerInput.Custom:
                if (Input.GetKeyDown(bombKey))
                {
                    _bombController.PlaceBomb();
                }
                break;
            default:
                break;
        }
    }
    
    private void MoveCalculation(Vector3 direction, AnimatedSpriteRenderer spriteRenderer)
    {
        Vector3 dir = direction * _moveSpeed * Time.fixedDeltaTime;
        Vector3 movement = transform.position + dir;
        _rigidBody.MovePosition(movement);

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

        if (playerController.playerInput == PlayerInput.Custom)
        {
            EditorGUILayout.Space(); // Add some space to separate custom input fields from the rest

            EditorGUI.BeginChangeCheck();

            playerController.upKey = (KeyCode)EditorGUILayout.EnumPopup("Up Key", playerController.upKey);
            playerController.downKey = (KeyCode)EditorGUILayout.EnumPopup("Down Key", playerController.downKey);
            playerController.leftKey = (KeyCode)EditorGUILayout.EnumPopup("Left Key", playerController.leftKey);
            playerController.rightKey = (KeyCode)EditorGUILayout.EnumPopup("Right Key", playerController.rightKey);
            playerController.bombKey = (KeyCode)EditorGUILayout.EnumPopup("Bomb Key", playerController.bombKey);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(playerController);
            }
        }
    }
}
