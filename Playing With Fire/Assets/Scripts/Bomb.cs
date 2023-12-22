using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
    private float _blastTime = 3;
    private BoxCollider2D _collider;
    private BombController _bombContoller;
    
    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        
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
        var explosion = Instantiate(_bombContoller.ExplosionPrefab, transform.position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion._spriteRendererStart);
        explosion.DestroyAfter(_bombContoller.ExplosionDuration);

        Explode(transform.position, Vector2.up, _bombContoller.ExplosionRadius);
        Explode(transform.position, Vector2.down, _bombContoller.ExplosionRadius);
        Explode(transform.position, Vector2.left, _bombContoller.ExplosionRadius);
        Explode(transform.position, Vector2.right, _bombContoller.ExplosionRadius);
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

        if(Physics2D.OverlapBox(position, Vector2.one / 2f , 0 ,  _bombContoller.ExplosionLayerMask ))
        {
            ClearDestructable(position);
            return;
        }

        var explosion = Instantiate(_bombContoller.ExplosionPrefab, position, Quaternion.identity);
        // CR: [discuss] Init pattern.
        //               explosion.Init(activeRenderer, direction, duration);
        explosion.SetActiveRenderer(lenght > 1 ? explosion._spriteRendererMiddle : explosion._spriteRendererEnd);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(_bombContoller.ExplosionDuration);

        Explode(position, direction, lenght - 1);
    }

    private void ClearDestructable(Vector2 position)
    {
        Vector3Int cell = _bombContoller.DestructableTile.WorldToCell(position);
        TileBase tile = _bombContoller.DestructableTile.GetTile(cell);

        if(tile != null)
        {
            Instantiate(_bombContoller.DestructablePrefab, position, Quaternion.identity);
            _bombContoller.DestructableTile.SetTile(cell, null);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _collider.isTrigger = false;
    }
}
