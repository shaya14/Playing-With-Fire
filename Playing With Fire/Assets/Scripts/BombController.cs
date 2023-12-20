using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombController : MonoBehaviour
{
    // CR: private...
    [Header("Bomb")]
    [SerializeField] Bomb _bombPrefab;

    // CR: remove. This is something that should just be in the prefab.
    [SerializeField] float _bombFuzeTime;
    
    // CR: [nit] rename -> _maxBombAmount
    [SerializeField] int _bombAmount;

    // CR: doesn't need to be 'SerializeField' (you set it in 'Awake').
    [SerializeField] int _bombRemaining;

    // CR: [discuss] should probably be a list 
    private Bomb _currentBomb;
    void Awake()
    {
        _bombRemaining = _bombAmount;
    }

    // CR: delete
    void Start()
    {

    }

    // Update is called once per frame
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
        bomb._blastTime = _bombFuzeTime;
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
