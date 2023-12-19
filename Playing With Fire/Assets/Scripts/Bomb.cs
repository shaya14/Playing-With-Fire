using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [HideInInspector] public float _blastTime;
    public bool _isBlasted = false; 

    void Start()
    {
        StartCoroutine(BlastCoroutine());
    }

    void OnDisable()
    {
        _isBlasted = true;
        Debug.Log("Bomb is Disabled");
    }

    void Blast()
    {
        //Destroy all breakable walls in blast radius
        Debug.Log("BOOM!");
        this.gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }

    private IEnumerator BlastCoroutine()
    {
        Debug.Log("Waiting for " + _blastTime + " seconds");
        yield return new WaitForSeconds(_blastTime);
        Blast();
    }
}
