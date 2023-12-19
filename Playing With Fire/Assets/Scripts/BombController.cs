using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] Bomb _bombPrefab;
    [SerializeField] float _bombFuzeTime;
    [SerializeField] int _bombAmount;
    [SerializeField] int _bombRemaining;
    private Bomb _currentBomb;
    void Awake()
    {
        _bombRemaining = _bombAmount;
    }
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
        //Instantiate bomb at player position
        //Set blast time
        //Set blast radius
        //Set bomb amount
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        var bomb = Instantiate(_bombPrefab, position, Quaternion.identity);
        bomb._blastTime = _bombFuzeTime;
        _currentBomb = bomb;
    }

    public void PlaceBomb()
    {
        //Check if player has bombs left
        //If so, instantiate bomb
        if (_bombRemaining > 0)
        {
            InstantiateBomb();
            _bombRemaining--;
        }
    }
}
