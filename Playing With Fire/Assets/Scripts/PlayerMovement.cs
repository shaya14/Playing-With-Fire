using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _moveSpeed;
    private Rigidbody2D _rigidBody;
    private BombController _bombController;
    private Damageable _damageable;
    private PlayerUiHandler _playerUiHandler;
    private float _timer;
    private bool _isBlinking = false;

    #region SpriteRenderers
    [HideInInspector] public bool showSpriteRenderers;
    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererUp;

    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererDown;

    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererLeft;

    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererRight;
    [HideInInspector][SerializeField] private AnimatedSpriteRenderer _spriteRendererDie;

    private AnimatedSpriteRenderer _activeSpriteRenderer;
    #endregion

    #region Keys
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode bombKey;
    #endregion
    
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

        GameManager.instance.UpdateInputMappingText(GetComponent<Player>(), upKey.ToString(), downKey.ToString(), leftKey.ToString(), rightKey.ToString(), bombKey);
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Update()
    {
        UpdateBomb();
        if (_isBlinking)
        {
            _timer += Time.deltaTime;
        }
    }
    void Movement()
    {
        Vector3 direction = GetInputDirection(upKey, downKey, leftKey, rightKey);
        MoveCalculation(direction, GetActiveSpriteRenderer(direction));
    }

    void UpdateBomb()
    {
        if (Input.GetKeyDown(bombKey))
        {
            _bombController.PlaceBomb();
        }
    }
    
    // CR: [discuss] Remove // why? its essential for the playes animation to work
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

    private IEnumerator Blink()
    {
        _timer = 0;
        _isBlinking = true; // CR: [discuss] remove // how i should do it? the timer dont work if not in update
        _damageable.SetInvulnerable(true);

        while (_timer < 3) // CR: [discuss]
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

