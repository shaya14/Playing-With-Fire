using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CR: [discuss] remove this code
public class SpawnPointCheck : MonoBehaviour
{
    [SerializeField] private GameObject _canvasToggle;
    private bool _isSpawnPointFilled = false;

    void Update()
    {
        if(_isSpawnPointFilled)
        {
            this.gameObject.SetActive(false);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            _isSpawnPointFilled = true;
            _canvasToggle.SetActive(true);
        }
    }
}
