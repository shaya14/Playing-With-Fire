using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private GameObject _ghostPrefab;
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
        _ghostPrefab = _player.ghostPrefab.gameObject;
    }

    public void TakeDamage(int damage)
    {
        if (!_isInvulnerable)
        {
            _currentHealth -= damage;

            InstantiateGhost();
            // CR: just 'GetComponent' immediately. (It's the same gameObject anyway).
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
        Vector2 ghostPosition = new Vector2(transform.position.x, transform.position.y + 0.4375f);
        var ghost = Instantiate(_ghostPrefab, ghostPosition, Quaternion.identity);
        GhostCoroutine(ghost);
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        _isInvulnerable = isInvulnerable;
    }

    public void GhostCoroutine(GameObject ghost)
    {
        StartCoroutine(GhostUpAndDisappear(ghost));
    }

    private IEnumerator GhostUpAndDisappear(GameObject ghost)
    {
        ghost.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);
        ghost.transform.position += Vector3.up * 0.25f;
        yield return new WaitForSeconds(0.25f);
        ghost.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
        ghost.transform.position += Vector3.up * 0.25f;
        yield return new WaitForSeconds(0.25f);
        ghost.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        ghost.transform.position += Vector3.up * 0.25f;
        yield return new WaitForSeconds(0.25f);
        ghost.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
        ghost.transform.position += Vector3.up * 0.25f;
        yield return new WaitForSeconds(0.25f);
        ghost.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
        ghost.transform.position += Vector3.up * 0.2f;
        yield return new WaitForSeconds(0.2f);

        Destroy(ghost.gameObject);
    }

}
