using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private int _maxBombAmount;
    private int _bombRemaining;

    [Header("Explosion")]
    [SerializeField] private Explosion _explosionPrefab;
    [SerializeField] private float _explosionDuration; // CR: move to 'Explosion' class. (Not used here, same for all players and bombs).
    [SerializeField] private int _explosionRadius;
    [SerializeField] LayerMask _explosionLayerMask; // CR: private. move to 'Bomb' class (not used here).

    [Header("Destructable")]
    [SerializeField] Destructable _destructablePrefab; // CR: private. Move to 'Bomb' class (not used here).
    private Tilemap _destructableTile; // CR: rename *destructableTiles*. move to 'Bomb' class (not used here).
    private PlayerUiHandler _playerUiHandler;

    public Explosion explosionPrefab => _explosionPrefab;
    public float explosionDuration => _explosionDuration;
    public int explosionRadius => _explosionRadius;
    public LayerMask explosionLayerMask => _explosionLayerMask;
    public Destructable destructablePrefab => _destructablePrefab;
    public Tilemap destructableTile => _destructableTile;
    public int numOfBombs => _maxBombAmount;

    void Awake()
    {
        _playerUiHandler = GetComponent<PlayerUiHandler>();
        _bombRemaining = _maxBombAmount;
        _destructableTile = GameObject.FindGameObjectWithTag("Destructables").GetComponent<Tilemap>();
    }

    void Start()
    {
        _playerUiHandler.UpdateNumOfBombsText(_maxBombAmount);
        _playerUiHandler.UpdateNumOfRadiusBoostsText(_explosionRadius);
    }

    public void OnBombExploded()
    {
        _bombRemaining++;
    }

    private void InstantiateBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        var bomb = Instantiate(_bombPrefab, position, Quaternion.identity);
        bomb.Init(this);
    }

    public void PlaceBomb()
    {
        if (_bombRemaining > 0 && !IsBombPlacedHere())
        {
            InstantiateBomb();
            _bombRemaining--;
        }
    }

    public void AddBomb()
    {
        _maxBombAmount++;
        // CR: '_bombRemaining++'. Otherwise, picking up the powerup when you have 2 deployed will "refill",
        //     allowing the player to place *5* bombs at the same time.
        _bombRemaining = _maxBombAmount; 
    }

    public void AddExplosionRadius()
    {
        _explosionRadius++;
    }

    private bool IsBombPlacedHere() {
        Vector2 tileCenter = new Vector2(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y)
        );

        RaycastHit2D[] hits = Physics2D.RaycastAll(
            origin: tileCenter,
            direction: Vector2.zero
        );

        foreach (RaycastHit2D hit in hits) {
            if (hit.collider != null && hit.collider.GetComponent<Bomb>() != null) {
                return true;
            }
        }
    
        return false;
    }
}
