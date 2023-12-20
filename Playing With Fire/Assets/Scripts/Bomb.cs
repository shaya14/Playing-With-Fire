using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // CR: [SerializeField] private.
    //     Set it in the bomb-prefab.
    [HideInInspector] public float _blastTime;

    // CR: private..
    [Header("Explosion")]
    [SerializeField] Explosion _explosionPrefab;
    
    // CR: [discuss] can this be a property of 'Explosion' prefab instead?
    [SerializeField] float _explosionDuration;
    
    [SerializeField] int _explosionRadius;

    public LayerMask _explosionLayerMask;

    // CR: private
    BoxCollider2D _collider;
    // CR: [discuss] how to remove state.
    [HideInInspector] public bool _isBlasted = false;
    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        StartCoroutine(BlastCoroutine());
    }

    void OnDisable()
    {
        _isBlasted = true;
    }

    void Blast()
    {
        //Destroy all breakable walls in blast radius    
        this.gameObject.SetActive(false);
        Destroy(gameObject, 1f);
        var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion._spriteRendererStart);
        explosion.DestroyAfter(_explosionDuration);

        Explode(transform.position, Vector2.up, _explosionRadius);
        Explode(transform.position, Vector2.down, _explosionRadius);
        Explode(transform.position, Vector2.left, _explosionRadius);
        Explode(transform.position, Vector2.right, _explosionRadius);
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
