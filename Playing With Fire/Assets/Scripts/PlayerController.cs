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
    // CR: [SerializeField] private PlayerInput _playerInput;
    //     public PlayerInput playerInput => _playerInput;

    public PlayerInput _playerInput;
    // CR: [SerializeField] private float _moveSpeed;
    //     Same everywhere.
    [SerializeField] float _moveSpeed;
    
    // CR: No shortening! '_rigidBody', not '_rb'.
    // CR: What about 'private'? :)
    //       private RigidBody2D _rb;
    //     Same everywhere.
    Rigidbody2D _rb;
    BombController _bombController;

    #region SpriteRenderers
    // CR: [SerializeField] private ...
    [SerializeField] AnimatedSpriteRenderer _spriteRendererUp;
    [SerializeField] AnimatedSpriteRenderer _spriteRendererDown;
    [SerializeField] AnimatedSpriteRenderer _spriteRendererLeft;
    [SerializeField] AnimatedSpriteRenderer _spriteRendererRight;
    public AnimatedSpriteRenderer _activeSpriteRenderer;
    #endregion

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
                    // CR: remove these kinds of comments.
                    //     MoveCalculation(Vector3.up, ...) is clear enough.
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
                else if (Input.anyKey == false)
                {
                    MoveCalculation(Vector3.zero, _moveSpeed, transform.position, _rb, _activeSpriteRenderer);
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
                else if (Input.anyKey == false)
                {
                    MoveCalculation(Vector3.zero, _moveSpeed, transform.position, _rb, _activeSpriteRenderer);
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
                    // CR: remove these kind of comments - '_bombController.PlaceBomb()' is clear enough.
                    //Spawn bomb
                    _bombController.PlaceBomb();
                }
                break;
            case PlayerInput.ArrowKeys:
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    //Spawn bomb
                    _bombController.PlaceBomb();
                }
                break;
            case PlayerInput.Custom:
                if (Input.GetKeyDown(_bombKey))
                {
                    //Spawn bomb
                    _bombController.PlaceBomb();
                }
                break;
            default:
                break;
        }
        //Spawn bomb
    }
    
    // CR: moveSpeed doesn't need to be a parameter (since it doesn't change).
    //     Just access _moveSpeed directly.
    //     Same with rb (just access _rigidBody directly).

    private void MoveCalculation(Vector3 direction, float moveSpeed, Vector3 position, Rigidbody2D rb, AnimatedSpriteRenderer spriteRenderer)
    {
        Vector3 dir = direction * moveSpeed * Time.fixedDeltaTime;
        // CR: 'position' doesn't need to be a parameter to the function.
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
