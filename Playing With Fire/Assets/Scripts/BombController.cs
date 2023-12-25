using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] float _explosionDuration;
    [SerializeField] int _explosionRadius;
    [SerializeField] LayerMask _explosionLayerMask;

    [Header("Destructable")]
    [SerializeField] Destructable _destructablePrefab;
    private Tilemap _destructableTile;
    private bool _isBombPlacedHere = false;
    private PlayerUiHandler _playerUiHandler;
    public Explosion ExplosionPrefab => _explosionPrefab;
    public float ExplosionDuration => _explosionDuration;
    public int ExplosionRadius => _explosionRadius;
    public LayerMask ExplosionLayerMask => _explosionLayerMask;
    public Destructable DestructablePrefab => _destructablePrefab;
    public Tilemap DestructableTile => _destructableTile;
    public int NumOfBombs => _maxBombAmount;

    

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
        if (_bombRemaining > 0 && !_isBombPlacedHere)
        {
            InstantiateBomb();
            _bombRemaining--;
        }
    }

    public void AddBomb()
    {
        _maxBombAmount++;
        _bombRemaining = _maxBombAmount;
    }

    public void AddExplosionRadius()
    {
        _explosionRadius++;
    }

    public void BombIsPlacedHere(bool isPlaced)
    {
        _isBombPlacedHere = isPlaced;
    }
}
