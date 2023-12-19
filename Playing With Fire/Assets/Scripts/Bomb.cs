using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float _blastTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlastCoroutine());
    }

    void Blast()
    {
        //Destroy all breakable walls in blast radius
        Debug.Log("BOOM!");
        Destroy(gameObject);
    }

    private IEnumerator BlastCoroutine()
    {
        Debug.Log("Waiting for " + _blastTime + " seconds");
        yield return new WaitForSeconds(_blastTime);
        Blast();
    }
}
