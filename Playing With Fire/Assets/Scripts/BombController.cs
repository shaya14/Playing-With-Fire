using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    [SerializeField] private Bomb _bombPrefab;

    [SerializeField] private int _maxBombAmount;

    private int _bombRemaining;

    // CR: [discuss] should probably be a list 
    private Bomb _currentBomb;
    void Awake()
    {
        _bombRemaining = _maxBombAmount;
    }

    void Update()
    {
        if (_currentBomb != null)
        {
            if (_currentBomb._isBlasted)
            {
                Debug.Log("Bomb is blasted");
                _bombRemaining++;
                _currentBomb = null;
            }
        }
    }

    private void InstantiateBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        var bomb = Instantiate(_bombPrefab, position, Quaternion.identity);
        _currentBomb = bomb;
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
