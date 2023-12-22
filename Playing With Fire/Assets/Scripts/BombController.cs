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
    
    [SerializeField] Destructable _destructablePrefab;
    [SerializeField] private Tilemap _destructableTile;


    void Awake()
    {
        _bombRemaining = _maxBombAmount;
    }

    public void OnBombExploded() {
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
        if (_bombRemaining > 0)
        {
            InstantiateBomb();
            _bombRemaining--;
        }
    }
}
