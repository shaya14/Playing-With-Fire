using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _blastTime;

    [Header("Explosion")]
    [SerializeField] private Explosion _explosionPrefab;
    
    // CR: [discuss] can this be a property of 'Explosion' prefab instead?
    [SerializeField] float _explosionDuration;
    
    [SerializeField] int _explosionRadius;

    public LayerMask _explosionLayerMask;

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
        var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion._spriteRendererStart);
        explosion.DestroyAfter(_explosionDuration);

        Explode(transform.position, Vector2.up, _explosionRadius);
        Explode(transform.position, Vector2.down, _explosionRadius);
        Explode(transform.position, Vector2.left, _explosionRadius);
        Explode(transform.position, Vector2.right, _explosionRadius);
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

        if(Physics2D.OverlapBox(position, Vector2.one / 2f , 0 ,  _explosionLayerMask ))
        {
            return;
        }

        var explosion = Instantiate(_explosionPrefab, position, Quaternion.identity);
        // CR: [discuss] Init pattern.
        //               explosion.Init(activeRenderer, direction, duration);
        explosion.SetActiveRenderer(lenght > 1 ? explosion._spriteRendererMiddle : explosion._spriteRendererEnd);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(_explosionDuration);

        Explode(position, direction, lenght - 1);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _collider.isTrigger = false;
    }
}
