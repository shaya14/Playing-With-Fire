using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private PlayerMovement _ghostPrefab;
    private PlayerMovement _playerMovement;
    private Player _player;
    private int _currentHealth;
    private bool _isInvulnerable = false;
    public int maxHealth => _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _playerMovement = GetComponent<PlayerMovement>();
        _player = GetComponent<Player>();
    }

    void Start()
    {
        _ghostPrefab = _player.GhostPrefab;
    }

    public void TakeDamage(int damage)
    {
        if (!_isInvulnerable)
        {
            _currentHealth -= damage;

            InstantiateGhost();
            _playerMovement.GetComponent<PlayerUiHandler>().DecreceLives();
            _playerMovement.RendererBlink();

            if (_currentHealth <= 0)
            {
                _playerMovement.DeathSequence();
            }
        }
    }
    private void InstantiateGhost()
    {
        var ghost = Instantiate(_ghostPrefab, transform.position, Quaternion.identity);
        ghost.GhostCoroutine();
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        _isInvulnerable = isInvulnerable;
    }
}
