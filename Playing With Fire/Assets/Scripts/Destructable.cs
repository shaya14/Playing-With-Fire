using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private float _destructionTime = 1f; // CR: no defaults in the code
    [SerializeField] private float _dropChance;
    [SerializeField] private ItemObject[] _itemObjects;
    private void Start()
    {
        Destroy(gameObject, _destructionTime);
    }

    private void OnDestroy()
    {
        DropItem();
    }

    private void DropItem()
    {
        if (Random.value <= _dropChance)
        {
            int randomIndex = Random.Range(0, _itemObjects.Length);
            Instantiate(_itemObjects[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
