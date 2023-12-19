using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] Bomb _bombPrefab;
    [SerializeField] float _bombFuzeTime;
    [SerializeField] float _bombAmount;
    private Bomb _currentBomb;

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
                _bombAmount++;
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
        var bomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
        bomb._blastTime = _bombFuzeTime;
        _currentBomb = bomb;
    }

    public void PlaceBomb()
    {
        //Check if player has bombs left
        //If so, instantiate bomb
        if (_bombAmount > 0)
        {
            InstantiateBomb();
            _bombAmount--;
        }
    }
}
