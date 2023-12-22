using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] float _destructionTime = 1f;
    private void Start()
    {
        Destroy(gameObject, _destructionTime);
    }
}
