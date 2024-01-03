using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
    private CircleCollider2D _collider;
    private BombController _bombContoller;
    private float _blastTime = 3;
    
    void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();       
    }

    public void Init(BombController bombController) {
        _bombContoller = bombController;
    }

    void Start()
    {
        StartCoroutine(BlastCoroutine());
    }

    void Blast()
    {
        var explosion = Instantiate(_bombContoller.explosionPrefab, transform.position, Quaternion.identity);
        explosion.SetRendererAndDuration(explosion.spriteRendererStart, _bombContoller.explosionDuration);

        Explode(transform.position, Vector2.up, _bombContoller.explosionRadius);
        Explode(transform.position, Vector2.down, _bombContoller.explosionRadius);
        Explode(transform.position, Vector2.left, _bombContoller.explosionRadius);
        Explode(transform.position, Vector2.right, _bombContoller.explosionRadius);
        //Destroy all breakable walls in blast radius    

        // In case the player died meanwhile.
        if (_bombContoller != null) {
            _bombContoller.OnBombExploded();
        }
        
        Destroy(gameObject);
    }

    private IEnumerator BlastCoroutine()
    {
        Debug.Log("Waiting for " + _blastTime + " seconds");
        yield return new WaitForSeconds(_blastTime);
        Blast();
    }

    private void Explode(Vector2 position , Vector2 direction , int lenght)
    {
        if(lenght <= 0){
            return;
        }

        position += direction;

        if(Physics2D.OverlapBox(position, Vector2.one / 2f , 0 ,  _bombContoller.explosionLayerMask ))
        {
            ClearDestructable(position);
            return;
        }

        var explosion = Instantiate(_bombContoller.explosionPrefab, position, Quaternion.identity);
        explosion.SetRendererDirectionAndDuration(lenght > 1 ? explosion.spriteRendererMiddle : explosion.spriteRendererEnd, direction, _bombContoller.explosionDuration);

        Explode(position, direction, lenght - 1);
    }

    private void ClearDestructable(Vector2 position)
    {
        Vector3Int cell = _bombContoller.destructableTile.WorldToCell(position);
        TileBase tile = _bombContoller.destructableTile.GetTile(cell);

        if(tile != null)
        {
            Instantiate(_bombContoller.destructablePrefab, position, Quaternion.identity);
            _bombContoller.destructableTile.SetTile(cell, null);
        }
    }

    // CR: make sure this is not a bug in case of collision with ghosts/fire/...
    void OnTriggerExit2D(Collider2D other)
    {
        _collider.isTrigger = false;
    }
}
