using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public enum PlayerInput
{
    WASD,
    ArrowKeys,
    Custom,
    None
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private int _moveSpeed;
    private Rigidbody2D _rigidBody;
    private BombController _bombController;
    private Damageable _damageable;
    private PlayerUiHandler _playerUiHandler;
    private float _timer;
    private bool _isBlinking = false;

    #region SpriteRenderers
    [HideInInspector] public bool showSpriteRenderers = true;

    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererUp;

    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererDown;

    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererLeft;

    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererRight;
    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererDie;

    private AnimatedSpriteRenderer _activeSpriteRenderer;
    #endregion

    #region Keys
    [HideInInspector] public KeyCode upKey;
    [HideInInspector] public KeyCode downKey;
    [HideInInspector] public KeyCode leftKey;
    [HideInInspector] public KeyCode rightKey;
    [HideInInspector] public KeyCode bombKey;
    #endregion
    private List<KeyCode> numpadKeys = new List<KeyCode> {
    KeyCode.Keypad8, KeyCode.Keypad5, KeyCode.Keypad4, KeyCode.Keypad6,
    KeyCode.Keypad7, KeyCode.Keypad2, KeyCode.Keypad1, KeyCode.Keypad3,
    KeyCode.Keypad9, KeyCode.Keypad3, KeyCode.Keypad7, KeyCode.Keypad1,
    KeyCode.KeypadPlus, KeyCode.KeypadMinus, KeyCode.KeypadMultiply, KeyCode.KeypadDivide,
    KeyCode.KeypadPeriod, KeyCode.KeypadEnter, KeyCode.KeypadEquals, KeyCode.Keypad0
};
    public PlayerInput playerInput => _playerInput;
    public AnimatedSpriteRenderer activeSpriteRenderer => _activeSpriteRenderer;
    public int numOfSpeedBoosts => _moveSpeed;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _bombController = GetComponent<BombController>();
        _damageable = GetComponent<Damageable>();
        _playerUiHandler = GetComponent<PlayerUiHandler>();
    }

    void Start()
    {
        if (_playerUiHandler != null)
        {
            _playerUiHandler.UpdateNumOfSpeedBoostsText(_moveSpeed);
            _playerUiHandler.UpdateLives(_damageable.maxHealth);
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Update()
    {
        SpawnBomb();
        if (_isBlinking)
        {
            _timer += Time.deltaTime;
        }
    }
    void Movement()
    {
        Vector3 direction = Vector3.zero;
        switch (_playerInput)
        {          
            case PlayerInput.WASD:
                direction = GetInputDirection(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
                MoveCalculation(direction, GetActiveSpriteRenderer(direction));
                GameManager.instance.UpdateInputMappingText(GetComponent<Player>(), "W", "A", "S", "D", KeyCode.Space);
                //_playerUiHandler.UpdateKeysText("W A S D");
                break;
            case PlayerInput.ArrowKeys:
                direction = GetInputDirection(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);
                MoveCalculation(direction, GetActiveSpriteRenderer(direction));
                GameManager.instance.UpdateInputMappingText(GetComponent<Player>(), "↑", "↓", "←", "→", KeyCode.Keypad0);
                //_playerUiHandler.UpdateKeysText("↑ ↓ ← →");
                //_playerUiHandler.keytext.fontSize = 42;
                break;
            case PlayerInput.Custom:
                direction = GetInputDirection(upKey, downKey, leftKey, rightKey);
                MoveCalculation(direction, GetActiveSpriteRenderer(direction));
                // _playerUiHandler.UpdateKeysText(upKey.ToString() + " " + downKey.ToString() + " " + leftKey.ToString() + " " + rightKey.ToString());
                // if (numpadKeys.Contains(upKey) || numpadKeys.Contains(downKey) || numpadKeys.Contains(leftKey) || numpadKeys.Contains(rightKey))
                // {
                //     _playerUiHandler.keytext.rectTransform.sizeDelta = new Vector2(200, 250);
                // }
                GameManager.instance.UpdateInputMappingText(GetComponent<Player>(), upKey.ToString(), downKey.ToString(), leftKey.ToString(), rightKey.ToString(), bombKey);
                break;
            case PlayerInput.None:
                direction = Vector3.zero;
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
    public void GetRenderers()
    {
        AnimatedSpriteRenderer[] spriteRenderers = GetComponentsInChildren<AnimatedSpriteRenderer>();
        foreach (AnimatedSpriteRenderer spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer == null) continue;
            if (spriteRenderer.name == "Up Sprite")
            {
                _spriteRendererUp = spriteRenderer;
            }
            else if (spriteRenderer.name == "Down Sprite")
            {
                _spriteRendererDown = spriteRenderer;
            }
            else if (spriteRenderer.name == "Left Sprite")
            {
                _spriteRendererLeft = spriteRenderer;
            }
            else if (spriteRenderer.name == "Right Sprite")
            {
                _spriteRendererRight = spriteRenderer;
            }
            else if (spriteRenderer.name == "Die Sprite")
            {
                _spriteRendererDie = spriteRenderer;
            }
        }
        SetActiveRendererByPosition(transform.position);
    }

    private void SetActiveRendererByPosition(Vector3 position)
    {
        if (position.y <= 0)
        {
            _activeSpriteRenderer = _spriteRendererUp;
        }
        else if (position.y >= 0)
        {
            _activeSpriteRenderer = _spriteRendererDown;
        }
    }
    public void AddSpeed(int speed)
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

        _activeSpriteRenderer.SetIdle(direction == Vector3.zero);


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

    public void RendererBlink()
    {
        StartCoroutine(Blink());
    }
    public void GhostCoroutine()
    {
        StartCoroutine(GhostUpAndDisappear());
    }
    private IEnumerator Blink()
    {
        _timer = 0;
        _isBlinking = true;
        _damageable.SetInvulnerable(true);

        while (_timer < 3)
        {
            _activeSpriteRenderer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);
            yield return new WaitForSeconds(0.1f);
            _activeSpriteRenderer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
            yield return new WaitForSeconds(0.1f);
            _activeSpriteRenderer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(0.1f);
            _activeSpriteRenderer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
            yield return new WaitForSeconds(0.1f);
        }

        _damageable.SetInvulnerable(false);
        _isBlinking = false;

        _spriteRendererUp.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        _spriteRendererDown.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        _spriteRendererLeft.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        _spriteRendererRight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }
    private IEnumerator GhostUpAndDisappear()
    {
        _activeSpriteRenderer = _spriteRendererDown;
        _activeSpriteRenderer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);
        transform.position += Vector3.up * 0.25f;
        yield return new WaitForSeconds(0.25f);
        _activeSpriteRenderer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
        transform.position += Vector3.up * 0.25f;
        yield return new WaitForSeconds(0.25f);
        _activeSpriteRenderer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        transform.position += Vector3.up * 0.25f;
        yield return new WaitForSeconds(0.25f);
        _activeSpriteRenderer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
        transform.position += Vector3.up * 0.25f;
        yield return new WaitForSeconds(0.25f);
        _activeSpriteRenderer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
        transform.position += Vector3.up * 0.2f;
        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }

    public void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        _spriteRendererUp.enabled = false;
        _spriteRendererDown.enabled = false;
        _spriteRendererLeft.enabled = false;
        _spriteRendererRight.enabled = false;

        _spriteRendererDie.enabled = true;

        Invoke(nameof(OnDeathSequenceFinished), 2f);
    }

    private void OnDeathSequenceFinished()
    {
        this.gameObject.SetActive(false);
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spriteRendererDie"));
        }

        // Apply any changes
        serializedObject.ApplyModifiedProperties();
    }


}
