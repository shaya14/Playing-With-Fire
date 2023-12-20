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

    void Awake()
    {
        _bombRemaining = _maxBombAmount;
    }

    void Update()
    {
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
