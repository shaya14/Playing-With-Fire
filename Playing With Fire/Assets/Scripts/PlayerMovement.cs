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

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rigidBody;
    private BombController _bombController;

    #region SpriteRenderers
    [HideInInspector] public bool showSpriteRenderers = true;

    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererUp;

    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererDown;

    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererLeft;

    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererRight;

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
        Vector3 direction = Vector3.zero;
        switch (_playerInput)
        {
            case PlayerInput.WASD:
                direction = GetInputDirection(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
                MoveCalculation(direction, GetActiveSpriteRenderer(direction));
                break;
            case PlayerInput.ArrowKeys:
                direction = GetInputDirection(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);
                MoveCalculation(direction, GetActiveSpriteRenderer(direction));
                break;
            case PlayerInput.Custom:
                direction = GetInputDirection(upKey, downKey, leftKey, rightKey);
                MoveCalculation(direction, GetActiveSpriteRenderer(direction));
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
    public void AddSpeed(float speed)
    {
        _moveSpeed += speed;
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

    AnimatedSpriteRenderer GetActiveSpriteRenderer(Vector3 direction)
    {
        if (direction == Vector3.up) return _spriteRendererUp;
        if (direction == Vector3.down) return _spriteRendererDown;
        if (direction == Vector3.left) return _spriteRendererLeft;
        if (direction == Vector3.right) return _spriteRendererRight;

        return _activeSpriteRenderer;
    }

    Vector3 GetInputDirection(KeyCode upKey, KeyCode downKey, KeyCode leftKey, KeyCode rightKey)
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(upKey))
            direction += Vector3.up;
        else if (Input.GetKey(downKey))
            direction += Vector3.down;

        if (Input.GetKey(rightKey))
            direction += Vector3.right;
        else if (Input.GetKey(leftKey))
            direction += Vector3.left;

        return direction;
    }
}

[CustomEditor(typeof(PlayerMovement))]
public class PlayerMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlayerMovement playerController = (PlayerMovement)target;

        EditorGUILayout.Space();
        playerController.showSpriteRenderers = EditorGUILayout.Toggle("Show Sprite Renderers", playerController.showSpriteRenderers);
        EditorGUILayout.Space();

        if (playerController.playerInput == PlayerInput.Custom)
        {
            EditorGUILayout.Space(); // Add some space to separate custom input fields from the rest

            EditorGUI.BeginChangeCheck();

            playerController.upKey = (KeyCode)EditorGUILayout.EnumPopup("Up Key", playerController.upKey);
            playerController.downKey = (KeyCode)EditorGUILayout.EnumPopup("Down Key", playerController.downKey);
            playerController.leftKey = (KeyCode)EditorGUILayout.EnumPopup("Left Key", playerController.leftKey);
            playerController.rightKey = (KeyCode)EditorGUILayout.EnumPopup("Right Key", playerController.rightKey);
            playerController.bombKey = (KeyCode)EditorGUILayout.EnumPopup("Bomb Key", playerController.bombKey);

            EditorGUILayout.Space();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(playerController);
            }
        }

        if (playerController.showSpriteRenderers)
        {
            // Draw the AnimatedSpriteRenderer fields
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spriteRendererUp"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spriteRendererDown"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spriteRendererLeft"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spriteRendererRight"));
        }

        // Apply any changes
        serializedObject.ApplyModifiedProperties();
    }


}
